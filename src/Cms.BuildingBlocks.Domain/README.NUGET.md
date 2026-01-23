# Cms.BuildingBlocks.Domain

Pure **Domain Building Blocks** for .NET applications using **DDD**, **Clean Architecture**, and **SOLID**.

![NuGet](https://img.shields.io/nuget/v/Cms.BuildingBlocks.Domain)
![Downloads](https://img.shields.io/nuget/dt/Cms.BuildingBlocks.Domain)
![License](https://img.shields.io/badge/license-MIT-blue)

## Features

- **AggregateRoot** base with versioning support
- **OwnedAggregateRoot** for user / tenant owned aggregates
- **Entity** base with built-in Domain Events
- **Strongly-typed Entity IDs**
- **Value Objects**
- **Domain Errors & Guard Clauses**
- **Audit-friendly abstractions**
- **Zero infrastructure dependencies**

## Installation

```bash
dotnet add package Cms.BuildingBlocks.Domain
```

## Example (User-Owned Aggregate)

```csharp
public sealed record CategoryId(Guid Value) : EntityId<Guid>(Value);
public sealed record UserId(Guid Value) : EntityId<Guid>(Value);

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
        Guard.AgainstNull(name, CategoryErrors.NameRequired);

        var category = new Category(id, ownerId, name);
        category.Raise(new CategoryCreated(id, ownerId, DateTime.UtcNow));

        return category;
    }
}
```

## Versioning

This package follows **Semantic Versioning (SemVer)**:
- **MAJOR** – breaking domain contracts
- **MINOR** – new abstractions, backward compatible
- **PATCH** – fixes and refactors

## License

MIT
