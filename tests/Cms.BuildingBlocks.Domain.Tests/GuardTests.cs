using Cms.BuildingBlocks.Domain.Errors;

using Shouldly;

using Xunit;

namespace Cms.BuildingBlocks.Domain.Tests;

public sealed class GuardTests
{
    [Fact]
    public void Against_ShouldThrowDomainException_WhenPredicateIsTrue()
    {
        var error = new DomainError("value.invalid");

        Action act = () =>
            Guard.Against(0, v => v == 0, error);

        DomainException exception = act.ShouldThrow<DomainException>();
        exception.Error.ShouldBe(error);
    }

    [Fact]
    public void Against_ShouldReturnValue_WhenPredicateIsFalse()
    {
        var error = new DomainError("value.invalid");

        var result = Guard.Against(1, v => v == 0, error);

        result.ShouldBe(1);
    }
}
