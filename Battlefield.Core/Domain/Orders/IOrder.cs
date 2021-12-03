namespace Battlefield.Core.Domain.Orders;
public interface IOrder
{
    void ExecuteOrder(Battle battle, BattleUnit unit);
}
