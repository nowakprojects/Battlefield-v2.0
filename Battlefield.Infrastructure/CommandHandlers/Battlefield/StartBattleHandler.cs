using Battlefield.Infrastructure.AI;
using Battlefield.Infrastructure.Commands.Battlefield;
using Battlefield.Infrastructure.EventHandlers;
using Battlefield.Infrastructure.Repositories;

namespace Battlefield.Infrastructure.CommandHandlers.Battlefield;
public class StartBattleHandler : ICommandHandler<StartBattle>
{
    private readonly IEventDispatcher _eventDispatcher;
    private readonly IBattlefieldRepository _battleRepo;
    private readonly ITimeTicker _ticker;
    private readonly IGameEnginesMemoryCache _gameEngines;

    public StartBattleHandler(
        IEventDispatcher eventDispatcher,
        IBattlefieldRepository battleRepo, 
        ITimeTicker ticker, 
        IGameEnginesMemoryCache gameEngines)
    {
        _eventDispatcher = eventDispatcher;
        _battleRepo = battleRepo;
        _ticker = ticker;
        _gameEngines = gameEngines;
    }

    public async Task HandleAsync(StartBattle command)
    {
        var battle = await _battleRepo.GetAsync(command.BattleId);
        var gameEngine = new GameEngine(_ticker, battle);
        var @event = battle.StartBattle(); 
        gameEngine.Enqueue(command);
        await _battleRepo.AddAsync(battle);
        await _gameEngines.AddAsync(gameEngine);
        await _eventDispatcher.PublishAsync(@event);
    }
    
}
