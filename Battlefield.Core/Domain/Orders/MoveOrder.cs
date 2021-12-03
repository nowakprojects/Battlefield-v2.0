namespace Battlefield.Core.Domain.Orders;
public class MoveOrder : IOrder
{
    public Coordinates Destination { get; set; }
    public MoveOrder(Coordinates dest)
    {
        Destination = dest;
    }

    public void ExecuteOrder(Battle battle, BattleUnit unit)
    {
        if(unit.CanMove())
        {
            var pathfinder = new PathFinder.PathFinder(battle);
            var path = pathfinder.GetPathFromTo(unit.Position,Destination);
            if(path.IsReachable)
            {
                var pos = path.GetNextStep();

            }
        }
    }
}
