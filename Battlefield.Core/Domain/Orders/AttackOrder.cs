using Battlefield.Core.Events;

namespace Battlefield.Core.Domain.Orders;

public class AttackOrder : IOrder
{
    public BattleUnit Target { get; set; }

    public AttackOrder(BattleUnit target)
    {
        Target = target;
    }

    public IEnumerable<IEvent> Execute(Battle battle, BattleUnit unit)
    {
        throw new NotImplementedException();
    }
}

