using Battlefield.Infrastructure.EventHandlers.BattleUnit;
using Battlefield.Infrastructure.Repositories;

namespace Battlefield.Infrastructure.EventHandlers
{
    internal class AddToBattlefieldCreatedUnit : IEventHandler<UnitCreated>
    {
        private readonly IBattlefiedRepository _battleRepository;

        public AddToBattlefieldCreatedUnit(IBattlefiedRepository battleRepos)
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
