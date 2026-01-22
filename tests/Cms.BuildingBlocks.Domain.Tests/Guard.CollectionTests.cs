using Cms.BuildingBlocks.Domain.Errors;
using Cms.BuildingBlocks.Domain.Tests.Abstractions.Dummies;

using Shouldly;

using Xunit;

namespace Cms.BuildingBlocks.Domain.Tests;

public sealed class Guard_Collection_Tests
{
    [Fact]
    public void AgainstEmpty_ShouldThrow_WhenCollectionIsEmpty()
    {
        var error = new DomainError("collection.empty");

        Action act = () =>
            Guard.AgainstEmpty(Array.Empty<int>(), error);

        act.ShouldThrow<DomainException>();
    }

    [Fact]
    public void AgainstEmpty_ShouldReturn_WhenCollectionHasItems()
    {
        var items = new[] { 1 };

        IReadOnlyCollection<int> result = Guard.AgainstEmpty(items, TestDomainError.DummyError());

        result.ShouldBe(items);
    }
}
