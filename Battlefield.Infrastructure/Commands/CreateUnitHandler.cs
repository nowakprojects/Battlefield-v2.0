using Battlefield.Core.Domain;
using Battlefield.Core.Domain.Creatures;
using Battlefield.Core.Events;
using Battlefield.Core.Events.BattleUnit;
using Battlefield.Infrastructure.Commands.BattleUnit;
using Battlefield.Infrastructure.EventHandlers;
using Battlefield.Infrastructure.Extensions;
using Battlefield.Infrastructure.Repositories;
using System.Reflection;

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
            
            ICreature unitType = command.Type.ConvertStringToCreature();

            var @event = battle.CreateUnit(new Coordinates(command.X,command.Y), command.Owner, unitType);
            

            await _eventDispatcher.PublishAsync(@event);
        }
    }
}
