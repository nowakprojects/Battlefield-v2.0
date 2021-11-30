using Battlefield.Core.Domain;
using Battlefield.Core.Events.BattleUnit;
using Battlefield.Infrastructure.Repositories;

namespace Battlefield.Infrastructure.EventHandlers
{
    public class UpdateTileMapWhenUnitCreated : IEventHandler<UnitCreated>
    {
        private readonly IBattlefieldRepository _battleRepository;

        public UpdateTileMapWhenUnitCreated(IBattlefieldRepository battleRepos)
        {
            _battleRepository = battleRepos;
        }
        public async Task HandleAsync(UnitCreated @event)
        {
            var unit = @event.Unit;
            var pos = unit.Position;

            if (pos == null)
            {
                return;
            }

            if (unit.OccupiedTiles == null)
                unit.OccupiedTiles = new Tile[unit.Size.X, unit.Size.Y];
            var battle = await _battleRepository.GetAsync(@event.BattleId);
            for (int y = 0; y < unit.Size.Y; y++)
                for (int x = 0; x < unit.Size.X; x++)
                {
                    var tile = battle.TileMap[pos.X + x, pos.Y + y];
                    if (tile.Blocked)
                        throw new Exception($"Tile ({tile.Coordinates.X},{tile.Coordinates.Y}) is blocked.");
                    tile.Blocked = true;
                    tile.Unit = @event.Unit;
                    unit.OccupiedTiles[x, y] = tile;
                }
            await Task.FromResult(Task.CompletedTask);
        }
    }
}
