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

            
            await Task.FromResult(Task.CompletedTask);
        }
    }
}
