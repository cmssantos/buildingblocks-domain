namespace Cms.BuildingBlocks.Domain.Abstractions;

/// <summary>
/// Represents the base class for aggregate roots in the domain.
/// Aggregate roots are entities that bind together a cluster of associated objects.
/// </summary>
/// <typeparam name="TId">The type of the aggregate root's identifier.</typeparam>
public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : class, IEntityId
{
    /// <summary>
    /// Gets the version of the aggregate. Used for optimistic concurrency control.
    /// </summary>
    public int Version { get; private set; }

    /// <summary>
    /// Increments the version of the aggregate.
    /// Typically called after persisting changes to support optimistic concurrency.
    /// </summary>
    protected void IncrementVersion()
        => Version++;
}
