using Battlefield.Core.Events.BattleUnit;
using Battlefield.Infrastructure.Repositories;

namespace Battlefield.Infrastructure.EventHandlers
{
    public class AddToBattlefieldCreatedUnit : IEventHandler<UnitCreated>
    {
        private readonly IBattlefieldRepository _battleRepository;

        public AddToBattlefieldCreatedUnit(IBattlefieldRepository battleRepos)
        {
            _battleRepository = battleRepos;
        }
        public async Task HandleAsync(UnitCreated @event)
        {

            var battlefield = await _battleRepository.GetAsync(@event.BattleId);
            battlefield.AddUnit(@event.Unit);
            await Task.FromResult(Task.CompletedTask);
        }
    }
}
