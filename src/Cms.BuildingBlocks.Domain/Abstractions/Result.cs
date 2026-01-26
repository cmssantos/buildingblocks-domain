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
        {
            throw new InvalidOperationException("Success result cannot have an error");
        }

        if (!isSuccess && error == DomainError.None)
        {
            throw new InvalidOperationException("Failure result must have an error");
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    /// <summary>
    /// Gets a value indicating whether the result represents a success operation.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets a value indicating whether the result represents a failure operation.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Gets the error associated with the failure result.
    /// </summary>
    public DomainError Error { get; }

    /// <summary>
    /// Creates a success result.
    /// </summary>
    public static Result Success() => new(true, DomainError.None);

    /// <summary>
    /// Creates a failure result with the specified error.
    /// </summary>
    public static Result Failure(DomainError error) => new(false, error);

    /// <summary>
    /// Creates a generic success result with the specified value.
    /// </summary>
    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, DomainError.None);

    /// <summary>
    /// Creates a generic failure result with the specified error.
    /// </summary>
    public static Result<TValue> Failure<TValue>(DomainError error) => new(default, false, error);
}

/// <summary>
/// Represents the result of a domain operation that returns a value on success.
/// </summary>
/// <typeparam name="TValue">The type of the value.</typeparam>
public class Result<TValue> : Result
{
    private readonly TValue? value;

    protected internal Result(TValue? value, bool isSuccess, DomainError error)
        : base(isSuccess, error)
    {
        this.value = value;
    }

    /// <summary>
    /// Gets the value of the result.
    /// Throws InvalidOperationException if accessed on a failure result.
    /// </summary>
    [NotNull]
    public TValue Value => IsSuccess
        ? value!
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    /// <summary>
    /// Implicitly converts a value to a success result.
    /// </summary>
    public static implicit operator Result<TValue>(TValue? value)
        => value is not null ? Success(value) : Failure<TValue>(DomainError.NullValue); // You might want a specific error for null here, or just handle it.

    /// <summary>
    /// Implicitly converts a domain error to a failure result.
    /// </summary>
    public static implicit operator Result<TValue>(DomainError error)
        => Failure<TValue>(error);
}
