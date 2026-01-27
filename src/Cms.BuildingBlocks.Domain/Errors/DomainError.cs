namespace Cms.BuildingBlocks.Domain.Errors;

public record DomainError
{
    public string Code { get; init; }
    public IReadOnlyDictionary<string, string?> Metadata { get; init; }

    public DomainError(
        string code,
        IReadOnlyDictionary<string, string?>? metadata = null)
    {
        Code = code;
        Metadata = metadata ?? new Dictionary<string, string?>();
    }

    public static readonly DomainError None = new(string.Empty);
    public static readonly DomainError NullValue = new("Error.NullValue");
}
