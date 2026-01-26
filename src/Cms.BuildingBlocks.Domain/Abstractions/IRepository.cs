namespace Cms.BuildingBlocks.Domain.Abstractions;

/// <summary>
/// Defines the contract for a repository pattern.
/// Can be used as a marker interface or to define common repository operations.
/// </summary>
/// <typeparam name="TEntity">The type of the entity managed by the repository.</typeparam>
public interface IRepository<TEntity>
    where TEntity : class
{
    // Marker interface or add common methods like GetByIdAsync if desired.
    // Often strictly typed repositories are preferred, but having a common base is good.
    // For now keeping it simple as a marker or place for common logic.
}
