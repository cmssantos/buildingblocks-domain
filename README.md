# Cms.BuildingBlocks.Domain

## Badges

### Main (Release)
[![CI/CD](https://github.com/cmssantos/buildingblocks-domain/actions/workflows/dotnet-ci.yml/badge.svg)](https://github.com/cmssantos/buildingblocks-domain/actions/workflows/dotnet-ci.yml)
![Coverage Main](https://codecov.io/gh/cmssantos/buildingblocks-domain/branch/main/graph/badge.svg)
![NuGet](https://img.shields.io/nuget/v/Cms.BuildingBlocks.Domain.svg)

---

## Overview

Core **Domain Building Blocks** for .NET applications following **Clean Architecture**, **SOLID**, and **Domain-Driven Design (DDD)** principles.

Provides reusable abstractions to accelerate rich domain model development while keeping the domain layer **clean, expressive, and independent** from infrastructure concerns.

---

## ✨ Features

- Base abstractions for **Entities**, **Value Objects**, and **Aggregates**
- **Domain Errors** and Result pattern
- Guard Clauses for invariant protection
- Strongly-typed identifiers
- Fully compatible with **DDD Tactical Patterns**
- No external dependencies
- Designed for **high testability**
- **Multi-targeted**: `net8.0` and `net10.0`

---

## 🧱 Architecture Alignment

This package is intended to be used **only in the Domain layer**:

```
Domain
 ├─ Aggregates
 ├─ Entities
 ├─ ValueObjects
 ├─ Errors
 ├─ Guards
 └─ Abstractions
```

Must **not** depend on:
- Infrastructure
- EF Core
- ASP.NET Core
- Serialization frameworks

---

## 🚀 Installation

```bash
dotnet add package Cms.BuildingBlocks.Domain
```

For private NuGet feeds:

```bash
dotnet nuget add source <URL> -n Cms --username <USER> --password <TOKEN>
```

---

## 🧪 Testing

- Multi-targeted (`net8.0`, `net10.0`)
- Deterministic & CI-friendly
- Run tests:

```bash
dotnet test
```

- Coverage threshold enforced (80% minimum)
- Coverage uploaded to **Codecov**

---

## 🔖 Versioning

- **Semantic Versioning** with automated versioning:
  - `main` branch → releases (patch increment automatically)
  - `develop` branch → pre-releases `-beta.N` (continuous)
  - Git tags → synchronized with NuGet
- **PATCH** auto-incremented based on last release tag
- Pre-releases automatically numbered by commits since last tag

Example:

```xml
<Version>1.0.3</Version>          <!-- main/release -->
<Version>1.0.4-beta.2</Version>   <!-- develop/pre-release -->
```

---

## 📄 License

MIT

---

## 🧠 Philosophy

> “The Domain Model is the heart of the software.”
> — Eric Evans

This package exists to keep that heart **clean, expressive, and protected**.
