namespace Cms.BuildingBlocks.Domain.Abstractions;

/// <summary>
/// Represents the base class for all entities in the domain.
/// Entities are objects that are defined by their identity rather than their attributes.
/// </summary>
/// <typeparam name="TId">The type of the entity's identifier.</typeparam>
#pragma warning disable S4035 // Entity is intentionally an abstract base class for inheritance, not sealed
public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : class, IEntityId
{
    /// <summary>
    /// Gets the unique identifier of this entity.
    /// </summary>
    public TId Id { get; protected init; } = default!;

    private readonly List<IDomainEvent> domainEvents = [];

    /// <summary>
    /// Gets the collection of domain events raised by this entity.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => domainEvents.AsReadOnly();

    /// <summary>
    /// Raises a new domain event.
    /// </summary>
    /// <param name="domainEvent">The domain event to raise.</param>
    protected void Raise(IDomainEvent domainEvent)
        => domainEvents.Add(domainEvent);

    /// <summary>
    /// Clears all domain events from the entity.
    /// </summary>
    public void ClearDomainEvents()
        => domainEvents.Clear();

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is Entity<TId> entity && Equals(entity);

    /// <summary>
    /// Indicates whether the current entity is equal to another entity of the same type.
    /// Equality is determined by comparing the entity IDs.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
    public bool Equals(Entity<TId>? other)
    {
        if (other is null) return false;

        if (ReferenceEquals(this, other)) return true;

        if (GetType() != other.GetType()) return false;

        return Id == other.Id || Id.Equals(other.Id);
    }

    /// <inheritdoc />
    public override int GetHashCode()
        => Id.GetHashCode() * 41;

    /// <summary>
    /// Determines whether two specified entity instances have the same value.
    /// </summary>
    /// <param name="left">The first entity to compare.</param>
    /// <param name="right">The second entity to compare.</param>
    /// <returns>true if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, false.</returns>
    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
        => Equals(left, right);

    /// <summary>
    /// Determines whether two specified entity instances have different values.
    /// </summary>
    /// <param name="left">The first entity to compare.</param>
    /// <param name="right">The second entity to compare.</param>
    /// <returns>true if the value of <paramref name="left"/> is different from the value of <paramref name="right"/>; otherwise, false.</returns>
    public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
        => !Equals(left, right);
}
#pragma warning restore S4035
