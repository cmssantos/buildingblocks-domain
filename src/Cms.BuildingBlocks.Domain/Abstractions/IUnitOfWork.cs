namespace Cms.BuildingBlocks.Domain.Abstractions;

/// <summary>
/// Defines the contract for the Unit of Work pattern, representing a business transaction.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Saves all changes made in this unit of work to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
