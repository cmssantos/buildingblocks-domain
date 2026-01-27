using Cms.BuildingBlocks.Domain.Errors;
using Cms.BuildingBlocks.Domain.Tests.Abstractions.Dummies;

using Shouldly;

using Xunit;

namespace Cms.BuildingBlocks.Domain.Tests;

public sealed class Guard_String_Tests
{
    [Fact]
    public void AgainstNull_ShouldReturnValue_WhenNotNull()
    {
        string value = Guard.AgainstNull("ok", TestDomainError.DummyError());

        value.ShouldBe("ok");
    }


    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void AgainstNullOrEmpty_ShouldThrow_WhenInvalid(string? value)
    {
        DomainError error = new DomainError("string.empty");

        Action act = () =>
            Guard.AgainstNullOrEmpty(value, error);

        act.ShouldThrow<DomainException>();
    }

    [Theory]
    [InlineData("a")]
    [InlineData("valid")]
    public void AgainstNullOrEmpty_ShouldReturn_WhenValid(string value)
    {
        string result = Guard.AgainstNullOrEmpty(value, TestDomainError.DummyError());

        result.ShouldBe(value);
    }
}
