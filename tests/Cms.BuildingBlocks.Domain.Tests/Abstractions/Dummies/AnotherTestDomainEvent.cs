using Cms.BuildingBlocks.Domain.Abstractions;

namespace Cms.BuildingBlocks.Domain.Tests.Abstractions.Dummies;

public sealed record AnotherTestDomainEvent : IDomainEvent
{
    public DateTime OccurredOn => throw new NotImplementedException();
}
