using Cms.BuildingBlocks.Domain.Errors;

namespace Cms.BuildingBlocks.Domain.Tests.Abstractions.Dummies;

public static class TestDomainError
{
    public static DomainError DummyError()
        => new("dummy");
}
