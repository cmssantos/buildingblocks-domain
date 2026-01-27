namespace Cms.BuildingBlocks.Domain.Errors;

public sealed class DomainException(DomainError error)
  : Exception(error.Code)
{
    public DomainError Error { get; } = error;
}
