using Battlefield.Core.Domain;

namespace Battlefield.Infrastructure.Events.BattleUnit
{
    public record UnitMoved(
        Core.Domain.BattleUnit Unit,
        Coordinates From,
        Coordinates To) : IEvent;
}
