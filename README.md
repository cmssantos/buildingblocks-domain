# Cms.BuildingBlocks.Domain

Reusable **Domain Building Blocks** for .NET applications following **Domain-Driven Design (DDD)**, **Clean Architecture**, and **SOLID** principles.

[![CI](https://github.com/cmssantos/buildingblocks-domain/actions/workflows/ci.yml/badge.svg)](https://github.com/cmssantos/buildingblocks-domain/actions/workflows/ci.yml)
[![Release](https://github.com/cmssantos/buildingblocks-domain/actions/workflows/release.yml/badge.svg)](https://github.com/cmssantos/buildingblocks-domain/actions/workflows/release.yml)
![NuGet](https://img.shields.io/nuget/v/Cms.BuildingBlocks.Domain)
![Downloads](https://img.shields.io/nuget/dt/Cms.BuildingBlocks.Domain)
![License](https://img.shields.io/badge/license-MIT-blue)

---

## 🎯 Purpose

This package provides **pure, infrastructure-agnostic domain abstractions** to help you build **rich domain models** with:

- Explicit business rules
- Strong invariants
- Clear aggregate boundaries
- First-class domain events
- Semantic domain errors

It is intentionally **minimal**, **opinionated**, and **framework-free**.

---

## 🧱 Architectural Positioning

This package is designed to be referenced **only by the Domain layer**.

```
src
├─ YourService.Domain          ← references Cms.BuildingBlocks.Domain
├─ YourService.Application
├─ YourService.Infrastructure
└─ YourService.API
```

### Important Rules

- ❌ Domain must NOT depend on Infrastructure
- ❌ Domain must NOT reference EF Core, ASP.NET, MediatR, Logging
- ✅ Application & Infrastructure depend on Domain
- ✅ Domain expresses business language, not technical concerns

---

## ✨ Key Features

- **AggregateRoot** base with versioning support
- **OwnedAggregateRoot** for user / tenant owned aggregates
- **Entity** base with built-in Domain Events and **Equality by ID**
- **Strongly-typed Entity IDs**
- **Value Object base**
- **Result Pattern** for explicit success/failure handling
- **Standard Repository & UnitOfWork interfaces**
- **Explicit Domain Errors**
- **Guard Clauses with business semantics**
- **Audit-friendly abstractions**
- **Zero infrastructure dependencies**

---

## 📦 Installation

```bash
dotnet add package Cms.BuildingBlocks.Domain
```

---

## 🧩 Core Concepts

### Aggregate Root

```csharp
public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : class, IEntityId
{
    public int Version { get; private set; }
}
```

---

### Owned Aggregate Root

```csharp
public abstract class OwnedAggregateRoot<TId, TOwnerId>
    : AggregateRoot<TId>
    where TId : class, IEntityId
    where TOwnerId : IEntityId
{
    public TOwnerId OwnerId { get; protected init; } = default!;
}
```

Use `OwnedAggregateRoot` when the aggregate lifecycle and visibility
are bound to a specific **user**, **account**, or **tenant**.

This is especially common in **financial systems**, **SaaS platforms**,
and **multi-tenant applications**.

---

### Entity Base & Domain Events

Entities now implement `IEquatable<Entity<TId>>` and are compared by **ID**, not reference.

```csharp
public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : class, IEntityId
{
    public TId Id { get; protected init; } = default!;

    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void Raise(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents()
        => _domainEvents.Clear();
}
```

---

### Result Pattern (No Exceptions)

Instead of throwing exceptions for validation errors, use the `Result` pattern.

```csharp
public class Result<TValue>
{
    public bool IsSuccess { get; }
    public bool IsFailure { get; }
    public TValue Value { get; }
    public DomainError Error { get; }
}
```

---

### Domain Events

```csharp
public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
```

---

### Strongly-Typed Entity IDs

```csharp
public abstract record EntityId<T>(T Value) : IEntityId
    where T : notnull;
```

```csharp
public interface IEntityId
{
    object Value { get; }
}
```

---

### Value Objects

```csharp
public abstract record ValueObject;
```

---

### Repositories & Unit of Work

Standard interfaces to be implemented in the Infrastructure layer.

```csharp
public interface IRepository<TEntity> where TEntity : class { }

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
```

---

## 🛡 Guard Clauses

```csharp
Guard.AgainstNull(name, CategoryErrors.NameRequired);
Guard.AgainstNegativeOrZero(amount, TransactionErrors.InvalidAmount);
```

---

## ❌ Domain Errors

```csharp
public record DomainError(
    string Code,
    IReadOnlyDictionary<string, string?>? Metadata = null)
{
    public static readonly DomainError None = new(string.Empty);
    public static readonly DomainError NullValue = new("Error.NullValue");
}
```

---

## 🧪 Complete Example — User-Owned Category Aggregate

```csharp
// 1. Define stronger IDs
public sealed record CategoryId(Guid Value) : EntityId<Guid>(Value);
public sealed record UserId(Guid Value) : EntityId<Guid>(Value);

// 2. Define Domain Events
public sealed record CategoryCreated(CategoryId Id, UserId OwnerId, DateTime OccurredOn) : IDomainEvent;
public sealed record CategoryArchived(CategoryId Id, UserId OwnerId, DateTime OccurredOn) : IDomainEvent;

// 3. Define Errors
public static class CategoryErrors
{
    public static readonly DomainError NameRequired = new("Category.NameRequired");
    public static readonly DomainError IdRequired = new("Category.IdRequired");
    public static readonly DomainError AlreadyArchived = new("Category.AlreadyArchived");
}

public sealed record CategoryName(string Value) : ValueObject
{
    public CategoryName(string value) : this(
        Guard.AgainstNullOrEmpty(value, CategoryErrors.NameRequired))
    { }
}

public sealed class Category
    : OwnedAggregateRoot<CategoryId, UserId>
{
    public CategoryName Name { get; private set; }
    public bool IsArchived { get; private set; }

    private Category(CategoryId id, UserId ownerId, CategoryName name)
    {
        Id = id;
        OwnerId = ownerId;
        Name = name;
    }

    public static Result<Category> Create(
        CategoryId id,
        UserId ownerId,
        CategoryName name)
    {
        // Example check returning Failure
        if (id == null)
            return Result.Failure<Category>(CategoryErrors.IdRequired);

        var category = new Category(id, ownerId, name);
        category.Raise(new CategoryCreated(id, ownerId, DateTime.UtcNow));

        return category; // Implicit conversion to Result<Category>
    }

    public Result Archive()
    {
        if (IsArchived)
            return Result.Failure(CategoryErrors.AlreadyArchived);

        IsArchived = true;
        Raise(new CategoryArchived(Id, OwnerId, DateTime.UtcNow));

        return Result.Success();
    }
}
```

---

## 🧭 Versioning Strategy

This project follows **Semantic Versioning (SemVer)**:

- **MAJOR** – Breaking changes in domain contracts
- **MINOR** – New abstractions (backward compatible)
- **PATCH** – Bug fixes, refactors

---

## 🚫 What This Package Does NOT Do

- Persistence
- ORM integration
- Logging
- Serialization
- Messaging
- Dependency Injection

---

## 📜 License

MIT
