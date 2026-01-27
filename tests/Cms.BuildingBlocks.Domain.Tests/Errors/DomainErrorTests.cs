using Cms.BuildingBlocks.Domain.Errors;

using Shouldly;

using Xunit;

namespace Cms.BuildingBlocks.Domain.Tests.Errors;

public sealed class DomainErrorTests
{
    [Fact]
    public void Constructor_ShouldInitializeEmptyMetadata_WhenNotProvided()
    {
        DomainError error = new DomainError("category.invalid");

        error.Code.ShouldBe("category.invalid");
        error.Metadata.ShouldNotBeNull();
        error.Metadata.ShouldBeEmpty();
    }

    [Fact]
    public void Constructor_ShouldSetMetadata_WhenProvided()
    {
        Dictionary<string, string?> metadata = new Dictionary<string, string?>
        {
            ["field"] = "name"
        };

        DomainError error = new DomainError("validation.failed", metadata);

        error.Metadata["field"].ShouldBe("name");
    }
}
