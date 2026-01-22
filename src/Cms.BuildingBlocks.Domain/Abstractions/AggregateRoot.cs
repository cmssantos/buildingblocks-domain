namespace Cms.BuildingBlocks.Domain.Abstractions;

public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : IEntityId
{
    public int Version { get; private set; }
}
