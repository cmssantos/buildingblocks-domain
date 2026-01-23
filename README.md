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
- **Entity** base with built-in Domain Events
- **Strongly-typed Entity IDs**
- **Value Object base**
- **Explicit Domain Errors & Exceptions**
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
    where TId : IEntityId
{
    public int Version { get; private set; }
}
```

---

### Owned Aggregate Root

```csharp
public abstract class OwnedAggregateRoot<TId, TOwnerId>
    : AggregateRoot<TId>
    where TId : IEntityId
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

```csharp
public abstract class Entity<TId>
    where TId : IEntityId
{
    public TId Id { get; protected init; } = default!;

    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

    protected void Raise(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents()
        => _domainEvents.Clear();
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

## 🛡 Guard Clauses

```csharp
Guard.AgainstNull(name, CategoryErrors.NameRequired);
Guard.AgainstNegativeOrZero(amount, TransactionErrors.InvalidAmount);
```

---

## ❌ Domain Errors & Exceptions

```csharp
public sealed class DomainError(
    string code,
    IReadOnlyDictionary<string, string?>? metadata = null);
```

```csharp
public sealed class DomainException(DomainError error)
    : Exception(error.Code);
```

---

## 🧪 Complete Example — User-Owned Category Aggregate

```csharp
public sealed record CategoryId(Guid Value) : EntityId<Guid>(Value);
public sealed record UserId(Guid Value) : EntityId<Guid>(Value);

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

    public static Category Create(
        CategoryId id,
        UserId ownerId,
        CategoryName name)
    {
        var category = new Category(id, ownerId, name);
        category.Raise(new CategoryCreated(id, ownerId, DateTime.UtcNow));
        return category;
    }

    public void Archive()
    {
        if (IsArchived) return;

        IsArchived = true;
        Raise(new CategoryArchived(Id, OwnerId, DateTime.UtcNow));
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
