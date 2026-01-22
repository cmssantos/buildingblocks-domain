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

    [Theory]
    [InlineData(-1)]
    [InlineData(-0.01)]
    public void AgainstNegativeDecimal_ShouldThrow_WhenNegative(decimal value)
    {
        Action act = () =>
            Guard.AgainstNegative(value, new DomainError("negative"));

        act.ShouldThrow<DomainException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(10.5)]
    public void AgainstNegativeDecimal_ShouldReturn_WhenZeroOrPositive(decimal value)
    {
        var result = Guard.AgainstNegative(value, TestDomainError.DummyError());

        result.ShouldBe(value);
    }


    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void AgainstNegativeOrZeroDecimal_ShouldThrow_WhenInvalid(decimal value)
    {
        Action act = () =>
            Guard.AgainstNegativeOrZero(value, new DomainError("invalid"));

        act.ShouldThrow<DomainException>();
    }

    [Theory]
    [InlineData(0.01)]
    [InlineData(10)]
    public void AgainstNegativeOrZeroDecimal_ShouldReturn_WhenPositive(decimal value)
    {
        var result = Guard.AgainstNegativeOrZero(value, TestDomainError.DummyError());

        result.ShouldBe(value);
    }
}
