using Battlefield.Core.Events;

namespace Battlefield.Core.Domain.Orders;
public class MoveOrder : IOrder
{
    public Coordinates Destination { get; set; }
    public MoveOrder(Coordinates dest)
    {
        Destination = dest;
    }

    public IEnumerable<IEvent> Execute(Battle battle, BattleUnit unit)
    {
        var eventList = new List<IEvent>();
        if (unit.Position.Equals(Destination))
        {
            return unit.GiveOrder(new WaitOrder());
        }
            
        if(unit.CanMove())
        {
            var pathfinder = new PathFinder.PathFinder(battle);
            var path = pathfinder.GetPathFromTo(unit.Position,Destination);
            if(path.IsReachable)
            {
                var pos = path.GetNextStep();
                var events = battle.MakeMoveUnit(unit, unit.Position, pos);
                foreach (var @event in events)
                {
                    eventList.Add(@event);   
                }
            }
        }

        return eventList;
    }
}
