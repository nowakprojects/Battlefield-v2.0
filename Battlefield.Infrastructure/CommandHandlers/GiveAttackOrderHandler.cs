using Battlefield.Core.Domain.Orders;
using Battlefield.Infrastructure.Commands.BattleUnit;
using Battlefield.Infrastructure.EventHandlers;
using Battlefield.Infrastructure.Repositories;

namespace Battlefield.Infrastructure.CommandHandlers;
public class GiveAttackOrderHandler : ICommandHandler<GiveAttackOrder>
{
    private readonly IBattlefieldRepository _battleRepo;
    private readonly IEventDispatcher _eventDispatcher;

    public GiveAttackOrderHandler(IBattlefieldRepository battleRepo, IEventDispatcher eventDispatcher)
    {
        _battleRepo = battleRepo;
        _eventDispatcher = eventDispatcher;
    }
    public async Task HandleAsync(GiveAttackOrder command)
    {
        var battle = await _battleRepo.GetAsync(command.BattleId);
        var unit = battle.GetUnitOrThrow(command.UnitId);
        var target = battle.GetUnitOrThrow(command.TargetId);

        var events = unit.GiveOrder(new AttackOrder(target));
        foreach (var @event in events)
            await _eventDispatcher.PublishAsync(@event);
    }
}