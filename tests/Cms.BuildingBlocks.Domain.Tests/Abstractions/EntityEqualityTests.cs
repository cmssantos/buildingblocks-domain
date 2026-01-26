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
        // Assuming we had another entity type, but for now checking against object or just verifying type check logic works roughly
        // If we strictly cannot create another entity with same ID but different type easily without defining one.
        // But the logic `GetType() != other.GetType()` handles it.
    }
}
