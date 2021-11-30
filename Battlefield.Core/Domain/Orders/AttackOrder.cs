namespace Battlefield.Core.Domain.Orders;

public class AttackOrder : IOrder
{
    public BattleUnit Target { get; set; }

    public AttackOrder(BattleUnit target)
    {
        Target = target;
    }
}

