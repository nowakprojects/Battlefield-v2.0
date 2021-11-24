using Battlefield.Infrastructure.Commands.BattleUnit;
using Battlefield.Infrastructure.EventHandlers;
using Battlefield.Infrastructure.Repositories;

namespace Battlefield.Infrastructure.Commands
{
    public class CreateUnitHandler : ICommandHandler<CreateUnit>
    {
        private readonly IBattlefieldRepository _battleRepo;
        private readonly IEventDispatcher _eventDispatcher;
        public CreateUnitHandler(IBattlefieldRepository battleRepo, IEventDispatcher eventDispatcher)
        {
            _battleRepo = battleRepo;
            _eventDispatcher = eventDispatcher;
        }

        public async Task HandleAsync(CreateUnit command)
        {
            var battle = await _battleRepo.GetAsync(command.BattleId);

            var @event = battle.CreateUnit(command.Position, command.Owner, command.Type);
            await _eventDispatcher.PublishAsync(@event);
        }
    }
}
