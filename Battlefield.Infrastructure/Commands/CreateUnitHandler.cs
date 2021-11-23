using Battlefield.Infrastructure.Commands.BattleUnit;
using Battlefield.Infrastructure.EventHandlers;
using Battlefield.Infrastructure.EventHandlers.BattleUnit;
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
            var battle = await _battleRepo.GetAsync(command.battleId);
            if(!battle.ContiansPos(command.Position))
            {
                throw new Exception("Invalid position");
            }
            if(!battle.ContainsPlayer(command.Owner))
            {
                throw new Exception($"Player {command.Owner.ToString()} not found.");
            }
            var unit = new Core.Domain.BattleUnit();
            unit.Position = command.Position;
            unit.Owner = command.Owner;
            unit.Type = command.Type;

            var @event = new UnitCreated(unit, battle.Id);
            await _eventDispatcher.PublishAsync(@event);
        }
    }
}
