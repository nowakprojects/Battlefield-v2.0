using Battlefield.Core.Domain;
using Battlefield.Infrastructure.EventHandlers;

namespace Battlefield.Infrastructure.AI;
public class GameEngine
{
    private IEventDispatcher? _dispatcher { get; set; }
    private ITimeTicker _timeTicker;
    private readonly Battle _battlefield;

    public GameEngine(ITimeTicker timeTicker, IEventDispatcher? dispatcher, Battle battlefield)
    {
        _timeTicker = timeTicker;
        _battlefield = battlefield;
        _dispatcher = dispatcher;
        _timeTicker.OnTimeTick(RunBattleTick);
    }

    public Guid BattleGuid()
    {
        return _battlefield.Id;
    }
    
    public void RunBattleTick()
    {
        if (_battlefield.Started)
        {
            UnitUpdateAsync(100);
            Console.WriteLine("In battle tick");
        }
    }
    private void UnitUpdateAsync(float dt)
    {
        foreach (var unit in _battlefield.Units)
        {
            unit.UpdateCooldowns(dt);
            unit.Order.Execute(_battlefield, unit);
            //make order
            //move unit if
            //make decision
            // MoveToWorad
            //_dispatcher.PublishAsync()
        }
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
