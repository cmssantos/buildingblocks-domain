using Cms.BuildingBlocks.Domain.Abstractions;

namespace Cms.BuildingBlocks.Domain.Tests.Abstractions.Dummies;

public sealed class TestAggregateRoot : AggregateRoot<TestEntityId>
{
    public TestAggregateRoot(TestEntityId id)
    {
      Id = id;
    }

    public void IncrementVersionPublic()
        => IncrementVersion();

    public void RaiseTestEvent(IDomainEvent domainEvent)
        => Raise(domainEvent);
}
