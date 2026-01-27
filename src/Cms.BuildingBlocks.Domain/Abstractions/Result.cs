using System.Diagnostics.CodeAnalysis;

using Cms.BuildingBlocks.Domain.Errors;

namespace Cms.BuildingBlocks.Domain.Abstractions;

/// <summary>
/// Represents the result of a domain operation, indicating success or failure.
/// </summary>
public class Result
{
    protected Result(bool isSuccess, DomainError error)
    {
        if (isSuccess && error != DomainError.None)
            throw new InvalidOperationException("Success result cannot have an error");

        if (!isSuccess && error == DomainError.None)
            throw new InvalidOperationException("Failure result must have an error");

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public DomainError Error { get; }

    // =============================
    // Success
    // =============================

    public static Result Success()
        => new(true, DomainError.None);

    public static Result<TValue> Success<TValue>(TValue value)
        => new(value, true, DomainError.None);

    // =============================
    // Failure
    // =============================

    public static Result Failure(DomainError error)
        => new(false, error);

    public static Result<TValue> Failure<TValue>(DomainError error)
        => new(default, false, error);
}

/// <summary>
/// Represents the result of a domain operation that returns a value on success.
/// </summary>
public class Result<TValue> : Result
{
    private readonly TValue? value;

    protected internal Result(TValue? value, bool isSuccess, DomainError error)
        : base(isSuccess, error)
    {
        this.value = value;
    }

    [NotNull]
    public TValue Value => IsSuccess
        ? value!
        : throw new InvalidOperationException(
            "The value of a failure result can not be accessed.");

    public static implicit operator Result<TValue>(TValue? value)
        => value is not null
            ? Success(value)
            : Failure<TValue>(DomainError.NullValue);

    public static implicit operator Result<TValue>(DomainError error)
        => Failure<TValue>(error);
}
