using Cms.BuildingBlocks.Domain.Errors;

using Shouldly;

using Xunit;

namespace Cms.BuildingBlocks.Domain.Tests.Errors;

public sealed class DomainErrorTests
{
    [Fact]
    public void Constructor_ShouldInitializeEmptyMetadata_WhenNotProvided()
    {
        var error = new DomainError("category.invalid");

        error.Code.ShouldBe("category.invalid");
        error.Metadata.ShouldNotBeNull();
        error.Metadata.ShouldBeEmpty();
    }

    [Fact]
    public void Constructor_ShouldSetMetadata_WhenProvided()
    {
        var metadata = new Dictionary<string, string?>
        {
            ["field"] = "name"
        };

        var error = new DomainError("validation.failed", metadata);

        error.Metadata["field"].ShouldBe("name");
    }
}
