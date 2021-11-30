using Battlefield.Core.Domain;

namespace Battlefield.Core.Events.BattleUnit;
public record UnitMoved(
    Domain.BattleUnit Unit,
    Coordinates From,
    Coordinates To) : IEvent;
