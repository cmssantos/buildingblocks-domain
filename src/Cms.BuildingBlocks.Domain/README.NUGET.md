
# Cms.BuildingBlocks.Domain

Pure **Domain Building Blocks** for .NET applications using **DDD**, **Clean Architecture**, and **SOLID**.

![NuGet](https://img.shields.io/nuget/v/Cms.BuildingBlocks.Domain)
![Downloads](https://img.shields.io/nuget/dt/Cms.BuildingBlocks.Domain)
![License](https://img.shields.io/badge/license-MIT-blue)

## Features

- AggregateRoot & Entity base classes
- Strongly-typed Entity IDs
- Domain Events (no MediatR dependency)
- Value Objects
- Guard Clauses with Domain Errors
- Audit-friendly abstractions
- Zero infrastructure dependencies

## Installation

```bash
dotnet add package Cms.BuildingBlocks.Domain
```

## Example (Aggregate)

```csharp
public sealed class Category : AggregateRoot<CategoryId>
{
    public CategoryName Name { get; private set; }
    public bool IsArchived { get; private set; }

    private Category(CategoryId id, CategoryName name)
    {
        Id = id;
        Name = name;
    }

    public static Category Create(CategoryId id, CategoryName name)
    {
        Guard.AgainstNull(name, CategoryErrors.NameRequired);

        var category = new Category(id, name);
        category.Raise(new CategoryCreated(id, DateTime.UtcNow));

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
