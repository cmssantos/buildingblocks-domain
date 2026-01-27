using Cms.BuildingBlocks.Domain.Abstractions;
using Cms.BuildingBlocks.Domain.Tests.Abstractions.Dummies;

using Shouldly;

using Xunit;

namespace Cms.BuildingBlocks.Domain.Tests.Abstractions;

public sealed class EntityTests
{
    [Fact]
    public void Constructor_ShouldSetId()
    {
        TestEntityId id = new TestEntityId(Guid.NewGuid());

        TestEntity entity = new TestEntity(id);

        entity.Id.ShouldBe(id);
    }

    [Fact]
    public void Raise_ShouldAddDomainEvent()
    {
        TestEntity entity = new TestEntity(new TestEntityId(Guid.NewGuid()));
        TestDomainEvent domainEvent = new TestDomainEvent();

        entity.RaiseTestEvent(domainEvent);

        entity.DomainEvents.ShouldHaveSingleItem();
        entity.DomainEvents.ShouldContain(domainEvent);
    }

    [Fact]
    public void ClearDomainEvents_ShouldRemoveAllEvents()
    {
        TestEntity entity = new TestEntity(new TestEntityId(Guid.NewGuid()));

        entity.RaiseTestEvent(new TestDomainEvent());
        entity.RaiseTestEvent(new TestDomainEvent());

        entity.DomainEvents.ShouldNotBeEmpty();

        entity.ClearDomainEvents();

        entity.DomainEvents.ShouldBeEmpty();
    }

    [Fact]
    public void DomainEvents_ShouldBeReadOnly()
    {
        TestEntity entity = new TestEntity(new TestEntityId(Guid.NewGuid()));

        entity.DomainEvents
            .ShouldBeAssignableTo<IReadOnlyCollection<IDomainEvent>>();
    }

    [Fact]
    public void Raise_ShouldAddMultipleDifferentDomainEvents()
    {
        TestEntity entity = new TestEntity(new TestEntityId(Guid.NewGuid()));
        TestDomainEvent event1 = new TestDomainEvent();
        AnotherTestDomainEvent event2 = new AnotherTestDomainEvent();

        entity.RaiseTestEvent(event1);
        entity.RaiseTestEvent(event2);

        entity.DomainEvents.Count.ShouldBe(2);
        entity.DomainEvents.ShouldContain(event1);
        entity.DomainEvents.ShouldContain(event2);
    }
}
