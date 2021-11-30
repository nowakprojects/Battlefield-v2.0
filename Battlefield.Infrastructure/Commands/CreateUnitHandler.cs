using Battlefield.Core.Domain;
using Battlefield.Core.Domain.Creatures;
using Battlefield.Core.Events;
using Battlefield.Core.Events.BattleUnit;
using Battlefield.Infrastructure.Commands.BattleUnit;
using Battlefield.Infrastructure.EventHandlers;
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
            var type = typeof(ICreature).Assembly
                        .GetTypes()
                        .FirstOrDefault(x => x.Name == command.Type);

            if (type is null || !typeof(ICreature).IsAssignableFrom(type))
            {
                throw new Exception($"invalid creatureType: '{command.Type}'");
            }
            var unitType = Activator.CreateInstance(type) as ICreature;
            var @event = battle.CreateUnit(new Coordinates(command.X,command.Y), command.Owner, unitType);
           
            await _eventDispatcher.PublishAsync(@event);
        }
    }
}
