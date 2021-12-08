
using Battlefield.Core.Events.Battlefield;
using Battlefield.Infrastructure.AI;
using Battlefield.Infrastructure.Repositories;

namespace Battlefield.Infrastructure.EventHandlers.Battlefield;
public class RunStartedGame : IEventHandler<BattleStarted>
{

    private readonly IEventDispatcher _eventDispatcher;
    private readonly IBattlefieldRepository _battleRepository;
    private readonly ITimeTicker _ticker;
    private readonly IGameEnginesMemoryCache _gameEngines;

    public RunStartedGame(IBattlefieldRepository battleRepo, ITimeTicker ticker, IEventDispatcher eventDispatcher, IGameEnginesMemoryCache gameEngines)
    {
        _battleRepository = battleRepo;
        _ticker = ticker;
        _eventDispatcher = eventDispatcher;
        _gameEngines = gameEngines;
    }

    public async Task HandleAsync(BattleStarted @event)
    {
        var battle = await _battleRepository.GetAsync(@event.BattleId);
        var gameEngine = new GameEngine(_ticker, _eventDispatcher, battle);
        await _gameEngines.AddAsync(gameEngine);
    }

}
