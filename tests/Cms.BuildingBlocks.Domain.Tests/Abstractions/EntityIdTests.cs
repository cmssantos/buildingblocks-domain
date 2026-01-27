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
        Guid guid = Guid.NewGuid();
        TestEntityId id = new TestEntityId(guid);

        id.Value.ShouldBe(guid);
    }

    [Fact]
    public void ShouldExposeValueViaIEntityId()
    {
        Guid guid = Guid.NewGuid();
        IEntityId id = new TestEntityId(guid);

        id.Value.ShouldBe(guid);
    }

    [Fact]
    public void ShouldSupportValueEquality()
    {
        Guid guid = Guid.NewGuid();

        TestEntityId id1 = new TestEntityId(guid);
        TestEntityId id2 = new TestEntityId(guid);

        id1.ShouldBe(id2);
    }
}
