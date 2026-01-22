using Cms.BuildingBlocks.Domain.Errors;
using Cms.BuildingBlocks.Domain.Tests.Abstractions.Dummies;

using Shouldly;

using Xunit;

namespace Cms.BuildingBlocks.Domain.Tests;

public sealed class Guard_Numeric_Tests
{
    public static TheoryData<int> NegativeValues =>
        [-1, -10, int.MinValue];

    [Theory]
    [MemberData(nameof(NegativeValues))]
    public void AgainstNegative_ShouldThrow_WhenValueIsNegative(int value)
    {
        var error = new DomainError("value.negative");

        Action act = () =>
            Guard.AgainstNegative(value, error);

        act.ShouldThrow<DomainException>();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    public void AgainstNegative_ShouldReturnValue_WhenValueIsPositive(int value)
    {
        var result = Guard.AgainstNegative(value, TestDomainError.DummyError());

        result.ShouldBe(value);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void AgainstNegativeOrZero_ShouldThrow_WhenInvalid(int value)
    {
        var error = new DomainError("value.invalid");

        Action act = () =>
            Guard.AgainstNegativeOrZero(value, error);

        act.ShouldThrow<DomainException>();
    }
}
