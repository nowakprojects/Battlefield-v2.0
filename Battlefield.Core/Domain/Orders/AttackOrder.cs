namespace Battlefield.Core.Domain.Orders;

public class AttackOrder : IOrder
{
    public BattleUnit Target { get; set; }

    public AttackOrder(BattleUnit target)
    {
        Target = target;
    }

    public void ExecuteOrder(Battle battle, BattleUnit unit)
    {
        throw new NotImplementedException();
    }
}

