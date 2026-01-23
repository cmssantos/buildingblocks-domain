namespace Cms.BuildingBlocks.Domain.Abstractions;

public abstract class OwnedAggregateRoot<TId, TOwnerId>
    : AggregateRoot<TId>
    where TId : IEntityId
    where TOwnerId : IEntityId
{
    public TOwnerId OwnerId { get; protected init; } = default!;
}
