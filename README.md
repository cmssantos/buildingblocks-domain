# Cms.BuildingBlocks.Domain

![Build](https://github.com/cmssantos/buildingblocks-domain/actions/workflows/ci.yml/badge.svg?branch=main)
![Coverage](https://codecov.io/gh/cmssantos/buildingblocks-domain/branch/main/graph/badge.svg)
![NuGet](https://img.shields.io/nuget/v/Cms.BuildingBlocks.Domain.svg)
![NuGet (beta)](https://img.shields.io/nuget/vpre/Cms.BuildingBlocks.Domain.svg)

Core **Domain Building Blocks** for .NET applications following **Clean Architecture**, **SOLID**, and **Domain-Driven Design (DDD)** principles.

This package provides reusable abstractions and primitives to accelerate the development of rich domain models while keeping the domain layer clean, expressive, and independent from infrastructure concerns.

---

## ✨ Features

- Base abstractions for **Entities**, **Value Objects**, and **Aggregates**
- **Domain Errors** and Result patterns
- Guard Clauses for invariant protection
- Strongly-typed IDs
- Fully compatible with **DDD Tactical Patterns**
- No external dependencies
- Designed for **high testability**
- **Multi-targeted**: `net8.0` and `net10.0`

---

## 📦 Target Frameworks

```text
net8.0
net10.0
```

Multi-targeting ensures forward compatibility while remaining stable on LTS runtimes.

---

## 🧱 Architecture Alignment

This package is intended to live in the **Domain layer**:

```text
Domain
 ├─ Aggregates
 ├─ Entities
 ├─ ValueObjects
 ├─ Errors
 ├─ Guards
 └─ Abstractions
```

It must **not** depend on:
- Infrastructure
- EF Core
- ASP.NET Core
- Serialization frameworks

---

## 🚀 Installation

```bash
dotnet add package Cms.BuildingBlocks.Domain
```

For private feeds:

```bash
dotnet nuget add source <URL> -n Cms --username <USER> --password <TOKEN>
```

---

## 🧪 Testing

- Tests are multi-targeted (`net8.0`, `net10.0`)
- Uses standard `.NET test` discovery
- Deterministic & CI-friendly builds

```bash
dotnet test
```

---

## 🔐 Versioning

- **Semantic Versioning**
- No breaking changes in PATCH/MINOR
- MAJOR only when domain contracts change

Example:

```xml
<Version>1.0.0</Version>
```

---

## 📄 License

MIT

---

## 🧠 Philosophy

> “The Domain Model is the heart of the software.”
> — Eric Evans

This package exists to keep that heart clean, expressive, and protected.
