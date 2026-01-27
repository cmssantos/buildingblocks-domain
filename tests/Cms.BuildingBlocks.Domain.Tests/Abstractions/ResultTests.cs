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
        Result result = Result.Success();

        result.IsSuccess.ShouldBeTrue();
        result.IsFailure.ShouldBeFalse();
        result.Error.ShouldBe(DomainError.None);
    }

    [Fact]
    public void Failure_ShouldCreateFailureResult()
    {
        DomainError error = new DomainError("Test.Error");
        Result result = Result.Failure(error);

        result.IsSuccess.ShouldBeFalse();
        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(error);
    }

    [Fact]
    public void SuccessGeneric_ShouldCreateSuccessResultWithValue()
    {
        string value = "test";
        Result<string> result = Result.Success(value);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBe(value);
    }

    [Fact]
    public void FailureGeneric_ShouldCreateFailureResultWithError()
    {
        DomainError error = new DomainError("Test.Error");
        Result<string> result = Result.Failure<string>(error);

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
        DomainError error = new DomainError("Test.Error");
        Result<string> result = error;

        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldBe(error);
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenSuccessWithError()
    {
        DomainError error = new DomainError("Error");
        Should.Throw<InvalidOperationException>(() => new TestResult(true, error));
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenFailureWithNoError()
      => Should.Throw<InvalidOperationException>(()
        => new TestResult(false, DomainError.None));

    [Fact]
    public void ImplicitConversion_FromNullValue_ShouldCreateFailureWithNullValueError()
    {
        string? nullValue = null;
        Result<string> result = nullValue;

        result.IsSuccess.ShouldBeFalse();
        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(DomainError.NullValue);
    }

    [Fact]
    public void IsFailure_ShouldBeTrue_WhenResultIsFailure()
    {
        DomainError error = new DomainError("Test.Error");
        Result result = Result.Failure(error);

        result.IsFailure.ShouldBeTrue();
        result.IsSuccess.ShouldBeFalse();
    }

    [Fact]
    public void IsFailure_ShouldBeFalse_WhenResultIsSuccess()
    {
        Result result = Result.Success();

        result.IsFailure.ShouldBeFalse();
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Error_ShouldReturnCorrectError_OnGenericFailureResult()
    {
        DomainError error = new DomainError("Test.Error");
        Result<string> result = Result.Failure<string>(error);

        result.Error.ShouldBe(error);
        result.IsFailure.ShouldBeTrue();
    }

    [Fact]
    public void Value_ShouldReturnCorrectValue_OnSuccessResult()
    {
        string expectedValue = "test value";
        Result<string> result = Result.Success(expectedValue);

        result.Value.ShouldBe(expectedValue);
        result.IsSuccess.ShouldBeTrue();
    }

    private class TestResult : Result
    {
        public TestResult(bool isSuccess, DomainError error) : base(isSuccess, error) { }
    }
}
