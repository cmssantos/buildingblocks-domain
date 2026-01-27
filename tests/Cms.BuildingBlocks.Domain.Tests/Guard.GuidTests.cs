using Cms.BuildingBlocks.Domain.Errors;
using Cms.BuildingBlocks.Domain.Tests.Abstractions.Dummies;

using Shouldly;

using Xunit;

namespace Cms.BuildingBlocks.Domain.Tests;

public sealed class Guard_Guid_Tests
{
    [Fact]
    public void AgainstEmpty_ShouldThrow_WhenGuidIsEmpty()
    {
        DomainError error = new DomainError("guid.empty");

        Action act = () =>
            Guard.AgainstEmpty(Guid.Empty, error);

        act.ShouldThrow<DomainException>();
    }

    [Fact]
    public void AgainstEmpty_ShouldReturn_WhenGuidIsValid()
    {
        Guid guid = Guid.NewGuid();

        Guid result = Guard.AgainstEmpty(guid, TestDomainError.DummyError());

        result.ShouldBe(guid);
    }
}
