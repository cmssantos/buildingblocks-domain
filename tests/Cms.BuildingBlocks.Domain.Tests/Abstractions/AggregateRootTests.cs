using Cms.BuildingBlocks.Domain.Abstractions;
using Cms.BuildingBlocks.Domain.Tests.Abstractions.Dummies;

using Shouldly;

using Xunit;

namespace Cms.BuildingBlocks.Domain.Tests.Abstractions;

public sealed class AggregateRootTests
{
    [Fact]
    public void Constructor_ShouldInitializeVersionToZero()
    {
        TestEntityId id = new TestEntityId(Guid.NewGuid());

        TestAggregateRoot aggregate = new TestAggregateRoot(id);

        aggregate.Version.ShouldBe(0);
    }

    [Fact]
    public void IncrementVersion_ShouldIncreaseVersionByOne()
    {
        TestAggregateRoot aggregate = new TestAggregateRoot(new TestEntityId(Guid.NewGuid()));

        aggregate.IncrementVersionPublic();

        aggregate.Version.ShouldBe(1);
    }

    [Fact]
    public void IncrementVersion_ShouldIncreaseVersionMultipleTimes()
    {
        TestAggregateRoot aggregate = new TestAggregateRoot(new TestEntityId(Guid.NewGuid()));

        aggregate.IncrementVersionPublic();
        aggregate.IncrementVersionPublic();
        aggregate.IncrementVersionPublic();

        aggregate.Version.ShouldBe(3);
    }

    [Fact]
    public void Version_ShouldPersistAfterMultipleIncrements()
    {
        TestAggregateRoot aggregate = new TestAggregateRoot(new TestEntityId(Guid.NewGuid()));

        aggregate.IncrementVersionPublic();
        int firstVersion = aggregate.Version;
        aggregate.IncrementVersionPublic();
        int secondVersion = aggregate.Version;

        firstVersion.ShouldBe(1);
        secondVersion.ShouldBe(2);
        aggregate.Version.ShouldBe(2);
    }

    [Fact]
    public void AggregateRoot_ShouldInheritFromEntity()
    {
        TestAggregateRoot aggregate = new TestAggregateRoot(new TestEntityId(Guid.NewGuid()));

        aggregate.ShouldBeAssignableTo<Entity<TestEntityId>>();
    }

    [Fact]
    public void AggregateRoot_ShouldSupportDomainEvents()
    {
        TestAggregateRoot aggregate = new TestAggregateRoot(new TestEntityId(Guid.NewGuid()));
        TestDomainEvent domainEvent = new TestDomainEvent();

        aggregate.RaiseTestEvent(domainEvent);

        aggregate.DomainEvents.ShouldHaveSingleItem();
        aggregate.DomainEvents.ShouldContain(domainEvent);
    }
}
