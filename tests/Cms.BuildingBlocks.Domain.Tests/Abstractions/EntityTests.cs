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
        var id = new TestEntityId(Guid.NewGuid());

        var entity = new TestEntity(id);

        entity.Id.ShouldBe(id);
    }

    [Fact]
    public void Raise_ShouldAddDomainEvent()
    {
        var entity = new TestEntity(new TestEntityId(Guid.NewGuid()));
        var domainEvent = new TestDomainEvent();

        entity.RaiseTestEvent(domainEvent);

        entity.DomainEvents.ShouldHaveSingleItem();
        entity.DomainEvents.ShouldContain(domainEvent);
    }

    [Fact]
    public void ClearDomainEvents_ShouldRemoveAllEvents()
    {
        var entity = new TestEntity(new TestEntityId(Guid.NewGuid()));

        entity.RaiseTestEvent(new TestDomainEvent());
        entity.RaiseTestEvent(new TestDomainEvent());

        entity.DomainEvents.ShouldNotBeEmpty();

        entity.ClearDomainEvents();

        entity.DomainEvents.ShouldBeEmpty();
    }

    [Fact]
    public void DomainEvents_ShouldBeReadOnly()
    {
        var entity = new TestEntity(new TestEntityId(Guid.NewGuid()));

        entity.DomainEvents
            .ShouldBeAssignableTo<IReadOnlyCollection<IDomainEvent>>();
    }
}
