namespace Cms.BuildingBlocks.Domain.Abstractions;

/// <summary>
/// Represents the base class for all value objects in the domain.
/// Value objects are immutable objects that are defined by their attributes rather than identity.
/// Derived classes should be records to leverage structural equality.
/// </summary>
public abstract record ValueObject;
