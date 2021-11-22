using Battlefield.Core.Domain;
using Battlefield.Infrastructure.Events;
using System;

namespace Battlefield.Infrastructure.EventHandlers.BattleUnit
{
    public record UnitCreated(
        Core.Domain.BattleUnit Unit,
        Guid BattleId) : IEvent;
}
