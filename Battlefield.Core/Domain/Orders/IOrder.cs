namespace Battlefield.Core.Domain.Orders;
public interface IOrder
{
    void Execute(Battle battle, BattleUnit unit);
}
