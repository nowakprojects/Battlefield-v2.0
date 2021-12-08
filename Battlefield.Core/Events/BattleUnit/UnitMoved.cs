using Battlefield.Core.Domain;

namespace Battlefield.Core.Events.BattleUnit;
public record UnitMoved(
    Guid BattleId,
    Guid UnitId,
    Coordinates From,
    Coordinates To) : IEvent;
