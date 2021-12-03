using Battlefield.Infrastructure.Commands.Battlefield;
using Battlefield.Infrastructure.EventHandlers;
using Battlefield.Infrastructure.Repositories;

namespace Battlefield.Infrastructure.CommandHandlers.Battlefield;
public class StartBattleHandler : ICommandHandler<StartBattle>
{
    private readonly IEventDispatcher _eventDispatcher;
    private readonly IBattlefieldRepository _battleRepo;

    public StartBattleHandler(IEventDispatcher eventDispatcher,
        IBattlefieldRepository battleRepo)
    {
        _eventDispatcher = eventDispatcher;
        _battleRepo = battleRepo;
    }

    public async Task HandleAsync(StartBattle command)
    {
        var battle = await _battleRepo.GetAsync(command.BattleId);
        var @event = battle.StartBattle();
        await _eventDispatcher.PublishAsync(@event);
    }
    
}
