using Battlefield.Core.Domain;
using Battlefield.Infrastructure.EventHandlers.BattleUnit;

namespace Battlefield.Infrastructure.EventHandlers
{
    public class AddToBattlefieldCreatedUnit : IEventHandler<UnitCreated>
    {
        public async Task HandleAsync(UnitCreated @event)
        {
            if(@event.Unit == null || @event.Battlefield == null)
            {
                return;
            }
            var pos = @event.Unit.Position;
            if (pos == null)
            {
                return;
            }
            Tile? tile = @event.Battlefield.TileMap[pos.X,
                                                   pos.Y];
            if (tile == null)
            {
                return;
            }

            tile.Blocked = true;
            tile.Unit = @event.Unit;
            await Task.FromResult(Task.CompletedTask);
        }
    }
}
