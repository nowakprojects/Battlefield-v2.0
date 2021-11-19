using Battlefield.Infrastructure.Events;

namespace Battlefield.Infrastructure.EventHandlers
{
    public class BattleEventBase : IBattleEvent
    {
        public Core.Domain.Battlefield? Battlefield { get; set; }
    }
}
