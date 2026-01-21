namespace Cms.BuildingBlocks.Domain.Errors;

public sealed class DomainError(
    string code,
    IReadOnlyDictionary<string, string?>? metadata = null)
{
    public string Code { get; } = code;
    public IReadOnlyDictionary<string, string?> Metadata { get; } = metadata
        ?? new Dictionary<string, string?>();
}
