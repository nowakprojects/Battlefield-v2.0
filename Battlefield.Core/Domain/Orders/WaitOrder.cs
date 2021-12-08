using Battlefield.Core.Events;

namespace Battlefield.Core.Domain.Orders;
public class WaitOrder : IOrder
{
    public IEnumerable<IEvent> Execute(Battle battle, BattleUnit unit)
    {
        var eventList = new List<IEvent>();
        return eventList;
    }
}
