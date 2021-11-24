using Battlefield.Core.Domain;
using Battlefield.Core.Events.Battlefield;
using Battlefield.Infrastructure.Commands.Battlefield;
using Battlefield.Infrastructure.EventHandlers;
using Battlefield.Infrastructure.Repositories;

namespace Battlefield.Infrastructure.Commands;

public class CreateBattlefieldHandler : ICommandHandler<CreateBattlefiled>
{
    private readonly IBattlefieldRepository _battleRepo;
    private readonly IEventDispatcher _eventDispatcher;
    public CreateBattlefieldHandler(IBattlefieldRepository battleRepo, IEventDispatcher eventDispatcher)
    {
        _battleRepo = battleRepo;
        _eventDispatcher = eventDispatcher;
    }
    public async Task HandleAsync(CreateBattlefiled command)
    {
        var @event = new BattlefieldCreated(new Battle());
        await _eventDispatcher.PublishAsync(@event);
    }
}

