using Cms.BuildingBlocks.Domain.Errors;

using Shouldly;

using Xunit;

namespace Cms.BuildingBlocks.Domain.Tests.Errors;

public sealed class DomainExceptionTests
{
    [Fact]
    public void Constructor_ShouldExposeDomainError()
    {
        DomainError error = new DomainError("business.rule");

        DomainException exception = new DomainException(error);

        exception.Error.ShouldBe(error);
        exception.Message.ShouldBe("business.rule");
    }
}
