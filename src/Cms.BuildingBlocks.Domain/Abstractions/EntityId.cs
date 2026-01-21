namespace Cms.BuildingBlocks.Domain.Abstractions;

public abstract record EntityId<T>(T Value) : IEntityId
    where T : notnull
{
    object IEntityId.Value => Value!;
}
