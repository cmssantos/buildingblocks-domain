namespace Cms.BuildingBlocks.Domain.Abstractions;

public abstract class Entity<TId>
    where TId : IEntityId
{
    public TId Id { get; protected init; } = default!;

    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

    protected void Raise(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents()
        => _domainEvents.Clear();
}
