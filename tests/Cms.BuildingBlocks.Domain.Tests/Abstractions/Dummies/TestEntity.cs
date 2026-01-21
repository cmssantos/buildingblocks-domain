using Cms.BuildingBlocks.Domain.Abstractions;

namespace Cms.BuildingBlocks.Domain.Tests.Abstractions.Dummies;

public sealed class TestEntity : Entity<TestEntityId>
{
    public TestEntity(TestEntityId id) => Id = id;

    public void RaiseTestEvent(IDomainEvent domainEvent)
        => Raise(domainEvent);
}
