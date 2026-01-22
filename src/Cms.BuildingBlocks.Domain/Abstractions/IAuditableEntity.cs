namespace Cms.BuildingBlocks.Domain.Abstractions;

public interface IAuditableEntity
{
    DateTime CreatedAtUtc { get; }
    DateTime? UpdatedAtUtc { get; }
}
