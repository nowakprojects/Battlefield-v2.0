using Battlefield.Core.Events.Battlefield;
using Battlefield.Infrastructure.Repositories;

namespace Battlefield.Infrastructure.EventHandlers.Battlefield;

public class AddToRepoCreatedBattlefied : IEventHandler<BattlefieldCreated>
{
    private readonly IBattlefieldRepository _battleRepository;
    public AddToRepoCreatedBattlefied(IBattlefieldRepository battleRepo)
        => _battleRepository = battleRepo;
    public async Task HandleAsync(BattlefieldCreated @event)
    {
        await _battleRepository.AddAsync(@event.Battle);
    }
}

