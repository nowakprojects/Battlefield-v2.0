using Battlefield.Core.Domain;
using Battlefield.Core.Events;
using Battlefield.Infrastructure.Commands;
using Battlefield.Infrastructure.Commands.BattleUnit;
using Battlefield.Infrastructure.EventHandlers;

namespace Battlefield.Infrastructure.AI;
public class GameEngine
{
    private readonly Battle _battlefield;
    public int TickCount = 0;
    // kolejka command graczy, musi byc tez synchornizowana, bo wiele watkow sie dostanie do tej kolejki
    private readonly Queue<ICommand> _queue = new();
    
    public GameEngine(ITimeTicker timeTicker, Battle battlefield)
    {
        _battlefield = battlefield;
        timeTicker.OnTimeTick(RunBattleTick);
    }

    public Guid BattleGuid()
    {
        return _battlefield.Id;
    }

    // przyjmuje komende gracza
    public void Enqueue(ICommand command)
    {
        _queue.Enqueue(command);
    }
    
    private IEnumerable<IEvent> RunBattleTick()
    {
        TickCount++;
        List<IEvent> events = new List<IEvent>();
        if (_battlefield.Started)
        {
            var command = _queue.Dequeue();
            if (command is GiveAttackOrder)
            {
                // attack 
            }
            
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
