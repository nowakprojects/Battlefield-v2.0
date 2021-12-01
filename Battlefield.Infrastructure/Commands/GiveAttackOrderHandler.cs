using Battlefield.Core.Domain.Orders;
using Battlefield.Core.Events.BattleUnit;
using Battlefield.Infrastructure.Commands.BattleUnit;
using Battlefield.Infrastructure.EventHandlers;
using Battlefield.Infrastructure.Repositories;
using System.Linq;

namespace Battlefield.Infrastructure.Commands;
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
        var unit = battle.Units.FirstOrDefault(x => x.Id == command.UnitId);
        if (unit is null)
        {
            throw new Exception($"There is no Unit in '{command.BattleId}' in battle with '{command.BattleId}'.");
        }

        var target = battle.Units.FirstOrDefault(x => x.Id == command.TargetId);
        if (target is null)
        {
            throw new Exception($"There is no Unit in '{command.BattleId}' in battle with '{command.TargetId}'.");
        }

        unit.GiveOrder(new AttackOrder(target)); // maybe set Order should return event?
        var @event = new UnitOrderChanged(command.UnitId, new AttackOrder(target));
        await _eventDispatcher.PublishAsync(@event);
    }
}