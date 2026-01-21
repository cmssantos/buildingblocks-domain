using Cms.BuildingBlocks.Domain.Abstractions;

namespace Cms.BuildingBlocks.Domain.Tests.Abstractions.Dummies;

public sealed record TestDomainEvent : IDomainEvent
{
    public DateTime OccurredOn => throw new NotImplementedException();
}
