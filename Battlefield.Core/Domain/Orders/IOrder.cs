using Battlefield.Core.Events;

namespace Battlefield.Core.Domain.Orders;
public interface IOrder
{
    IEnumerable<IEvent> Execute(Battle battle, BattleUnit unit);
}
