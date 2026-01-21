using Cms.BuildingBlocks.Domain.Errors;

namespace Cms.BuildingBlocks.Domain;

public static class Guard
{
    public static T Against<T>(
        T value,
        Func<T, bool> predicate,
        DomainError error)
    {
        if (predicate(value))
            throw new DomainException(error);

        return value;
    }
}
