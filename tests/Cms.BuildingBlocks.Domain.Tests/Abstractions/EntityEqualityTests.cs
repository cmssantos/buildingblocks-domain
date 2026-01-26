using Cms.BuildingBlocks.Domain.Abstractions;
using Cms.BuildingBlocks.Domain.Tests.Abstractions.Dummies;

using Shouldly;

using Xunit;

namespace Cms.BuildingBlocks.Domain.Tests.Abstractions;

public class EntityEqualityTests
{
    [Fact]
    public void Equals_ShouldReturnTrue_WhenIdsAreEqual()
    {
        var id = new TestEntityId(Guid.NewGuid());
        var entity1 = new TestEntity(id);
        var entity2 = new TestEntity(id);

        (entity1 == entity2).ShouldBeTrue();
        entity1.Equals(entity2).ShouldBeTrue();
        entity1.Equals((object)entity2).ShouldBeTrue();
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenIdsAreDifferent()
    {
        var entity1 = new TestEntity(new TestEntityId(Guid.NewGuid()));
        var entity2 = new TestEntity(new TestEntityId(Guid.NewGuid()));

        (entity1 != entity2).ShouldBeTrue();
        entity1.Equals(entity2).ShouldBeFalse();
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenOtherIsNull()
    {
        var entity1 = new TestEntity(new TestEntityId(Guid.NewGuid()));

        entity1.Equals(null).ShouldBeFalse();
        (entity1 == null).ShouldBeFalse();
        (null == entity1).ShouldBeFalse();
    }

    [Fact]
    public void GetHashCode_ShouldReturnSameValue_WhenIdsAreEqual()
    {
        var id = new TestEntityId(Guid.NewGuid());
        var entity1 = new TestEntity(id);
        var entity2 = new TestEntity(id);

        entity1.GetHashCode().ShouldBe(entity2.GetHashCode());
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenTypesAreDifferent()
    {
        var id = new TestEntityId(Guid.NewGuid());
        var entity1 = new TestEntity(id);
        var entityDifferentType = new AnotherTestEntity(id);

        entity1.Equals(entityDifferentType).ShouldBeFalse();
    }

    [Fact]
    public void Equals_ShouldReturnTrue_WhenReferenceIsSame()
    {
        var entity1 = new TestEntity(new TestEntityId(Guid.NewGuid()));
        var entity2 = entity1;

        entity1.Equals(entity2).ShouldBeTrue();
    }

    private class AnotherTestEntity : Entity<TestEntityId>
    {
        public AnotherTestEntity(TestEntityId id)
        {
            Id = id;
        }
    }
}
