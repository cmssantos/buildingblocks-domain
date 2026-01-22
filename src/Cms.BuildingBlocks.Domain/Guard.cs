using Cms.BuildingBlocks.Domain.Errors;

namespace Cms.BuildingBlocks.Domain;

public static class Guard
{
    private static T Against<T>(
        T value,
        Func<T, bool> predicate,
        DomainError error)
    {
        if (predicate(value))
            throw new DomainException(error);

        return value;
    }

    // =============================
    // Null / Empty
    // =============================

    public static T AgainstNull<T>(
        T? value,
        DomainError error)
        where T : class
        => Against(value, v => v is null, error)!;

    public static string AgainstNullOrEmpty(
        string? value,
        DomainError error)
        => Against(value, string.IsNullOrWhiteSpace, error)!;

    public static Guid AgainstEmpty(
        Guid value,
        DomainError error)
        => Against(value, v => v == Guid.Empty, error);

    // =============================
    // Numeric
    // =============================

    public static int AgainstNegative(
        int value,
        DomainError error)
        => Against(value, v => v < 0, error);

    public static int AgainstNegativeOrZero(
        int value,
        DomainError error)
        => Against(value, v => v <= 0, error);

    public static decimal AgainstNegative(
        decimal value,
        DomainError error)
        => Against(value, v => v < 0, error);

    public static decimal AgainstNegativeOrZero(
        decimal value,
        DomainError error)
        => Against(value, v => v <= 0, error);

    // =============================
    // Collections
    // =============================

    public static IReadOnlyCollection<T> AgainstEmpty<T>(
        IReadOnlyCollection<T>? value,
        DomainError error)
        => Against(value, v => v is null || v.Count == 0, error)!;

    // =============================
    // Custom predicates (escape hatch)
    // =============================
    public static T Against<T>(
        T value,
        Func<T, bool> rule,
        DomainError error,
        bool _)
        => Against(value, rule, error);
}
