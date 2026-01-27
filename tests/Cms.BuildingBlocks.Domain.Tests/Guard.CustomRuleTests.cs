using Cms.BuildingBlocks.Domain.Errors;
using Cms.BuildingBlocks.Domain.Tests.Abstractions.Dummies;

using Shouldly;

using Xunit;

namespace Cms.BuildingBlocks.Domain.Tests;

public sealed class Guard_CustomRule_Tests
{
    [Fact]
    public void Against_WithCustomRule_ShouldThrow_WhenRuleIsTrue()
    {
        DomainError error = new DomainError("custom.rule.failed");

        Action act = () =>
            Guard.Against(
                0,
                v => v == 0,
                error,
                _: true);

        DomainException exception = act.ShouldThrow<DomainException>();
        exception.Error.ShouldBe(error);
    }

    [Fact]
    public void Against_WithCustomRule_ShouldReturnValue_WhenRuleIsFalse()
    {
        int result = Guard.Against(
            10,
            v => v < 0,
            TestDomainError.DummyError(),
            _: true);

        result.ShouldBe(10);
    }
}
