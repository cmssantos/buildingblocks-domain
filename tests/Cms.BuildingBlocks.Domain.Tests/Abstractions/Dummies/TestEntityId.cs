using Cms.BuildingBlocks.Domain.Abstractions;

namespace Cms.BuildingBlocks.Domain.Tests.Abstractions.Dummies;

public sealed record TestEntityId(Guid Value) : EntityId<Guid>(Value);
