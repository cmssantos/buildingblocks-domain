using Cms.BuildingBlocks.Domain.Abstractions;
using Cms.BuildingBlocks.Domain.Tests.Abstractions.Dummies;
using Shouldly;
using Xunit;

namespace Cms.BuildingBlocks.Domain.Tests.Abstractions;
public sealed class EntityIdTests
{
    [Fact]
    public void ShouldExposeValue()
    {
        var guid = Guid.NewGuid();
        var id = new TestEntityId(guid);

        id.Value.ShouldBe(guid);
    }

    [Fact]
    public void ShouldExposeValueViaIEntityId()
    {
        var guid = Guid.NewGuid();
        IEntityId id = new TestEntityId(guid);

        id.Value.ShouldBe(guid);
    }

    [Fact]
    public void ShouldSupportValueEquality()
    {
        var guid = Guid.NewGuid();

        var id1 = new TestEntityId(guid);
        var id2 = new TestEntityId(guid);

        id1.ShouldBe(id2);
    }
}
