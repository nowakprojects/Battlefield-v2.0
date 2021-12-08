using Battlefield.Core.Domain;
using Battlefield.Core.Events;
using Battlefield.Infrastructure.EventHandlers;

namespace Battlefield.Infrastructure.AI;
public class GameEngine
{
    private readonly Battle _battlefield;
    public int TickCount = 0;

    public GameEngine(ITimeTicker timeTicker, Battle battlefield)
    {
        _battlefield = battlefield;
        timeTicker.OnTimeTick(RunBattleTick);
    }

    public Guid BattleGuid()
    {
        return _battlefield.Id;
    }
    
    private IEnumerable<IEvent> RunBattleTick()
    {
        TickCount++;
        List<IEvent> events = new List<IEvent>();
        if (_battlefield.Started)
        {
            var unitUpdateEvents = UnitUpdateAsync(100);
            foreach (var unitUpdateEvent in unitUpdateEvents)
            {
                    events.Add(unitUpdateEvent);
            }
            Console.WriteLine("In battle tick");
        }

        return events;
    }
    private IEnumerable<IEvent> UnitUpdateAsync(float dt)
    {
        return _battlefield.Units.Select(unit =>
        {
            unit.UpdateCooldowns(dt);
            var events = unit.Order.Execute(_battlefield, unit);
            return events;
            //make order
            //move unit if
            //make decision
            // MoveToWorad
            //_dispatcher.PublishAsync()
        }).Aggregate(new List<IEvent>(), (list, events) => list.Union(events).ToList());
    }

    private BattleUnit? CalculateBestTargetFor(BattleUnit unit)
    {
        double min = 9999;
        BattleUnit? result = null;
        foreach (var u in _battlefield.Units)
        {
            if (u == unit || unit.Owner == u.Owner) continue;

            var distanceX = u.Position.X - unit.Position.X;
            var distanceY = u.Position.Y - unit.Position.Y;
            var distance = Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
            if (min > distance)
                min = distance;
        }
        return result;
    }
}
