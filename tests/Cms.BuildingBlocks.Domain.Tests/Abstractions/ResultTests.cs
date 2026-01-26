using Cms.BuildingBlocks.Domain.Abstractions;
using Cms.BuildingBlocks.Domain.Errors;

using Shouldly;

using Xunit;

namespace Cms.BuildingBlocks.Domain.Tests.Abstractions;

public class ResultTests
{
    [Fact]
    public void Success_ShouldCreateSuccessResult()
    {
        var result = Result.Success();

        result.IsSuccess.ShouldBeTrue();
        result.IsFailure.ShouldBeFalse();
        result.Error.ShouldBe(DomainError.None);
    }

    [Fact]
    public void Failure_ShouldCreateFailureResult()
    {
        var error = new DomainError("Test.Error");
        var result = Result.Failure(error);

        result.IsSuccess.ShouldBeFalse();
        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(error);
    }

    [Fact]
    public void SuccessGeneric_ShouldCreateSuccessResultWithValue()
    {
        var value = "test";
        var result = Result.Success(value);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBe(value);
    }

    [Fact]
    public void FailureGeneric_ShouldCreateFailureResultWithError()
    {
        var error = new DomainError("Test.Error");
        var result = Result.Failure<string>(error);

        result.IsSuccess.ShouldBeFalse();
        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(error);
        Should.Throw<InvalidOperationException>(() => _ = result.Value);
    }

    [Fact]
    public void ImplicitConversion_FromValue_ShouldCreateSuccessResult()
    {
        string value = "test";
        Result<string> result = value;

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBe(value);
    }

    [Fact]
    public void ImplicitConversion_FromError_ShouldCreateFailureResult()
    {
        var error = new DomainError("Test.Error");
        Result<string> result = error;

        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldBe(error);
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenSuccessWithError()
    {
        var error = new DomainError("Error");
        Should.Throw<InvalidOperationException>(() => new TestResult(true, error));
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenFailureWithNoError()
    {
        Should.Throw<InvalidOperationException>(() => new TestResult(false, DomainError.None));
    }

    private class TestResult : Result
    {
        public TestResult(bool isSuccess, DomainError error) : base(isSuccess, error) { }
    }
}
