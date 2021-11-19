using Battlefield.Core.Domain;
using Battlefield.Infrastructure.Events;
using System;

namespace Battlefield.Infrastructure.EventHandlers.BattleUnit
{
    public class UnitCreated : BattleEventBase
    {
        public Core.Domain.BattleUnit? Unit { get; set; }
    }
}
