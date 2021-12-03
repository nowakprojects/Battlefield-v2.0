
using Battlefield.Core.Events.Battlefield;
using Battlefield.Infrastructure.Repositories;

namespace Battlefield.Infrastructure.EventHandlers.Battlefield;
public class RunStartedGame : IEventHandler<BattleStarted>
{

    private readonly IBattlefieldRepository _battleRepository;
    public RunStartedGame(IBattlefieldRepository battleRepo)
        => _battleRepository = battleRepo;
    public async Task HandleAsync(BattleStarted @event)
    {
        var battle = await _battleRepository.GetAsync(@event.BattleId);
        AI.AI.RunBattleAsync(battle);
    }

}
