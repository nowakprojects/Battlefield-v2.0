using Battlefield.Core.Domain;
using Battlefield.Infrastructure.EventHandlers;

namespace Battlefield.Infrastructure.AI;
public static class AI
{
    public static IEventDispatcher? _dispatcher { get; set; }
    public static void RunBattle(Battle battlefield)
    {
        var timeAfter = DateTime.Now;
        while (battlefield.Started)
        {
            var dt = DateTime.Now - timeAfter;

            UnitUpdateAsync(((float)dt.TotalSeconds), battlefield).Wait();
            timeAfter = DateTime.Now;
        }
    }
    public async static Task UnitUpdateAsync(float dt, Battle battlefield)
    {
        foreach (var unit in battlefield.Units)
        {
            unit.UpdateCooldowns(dt);
            unit.Order.Execute(battlefield, unit);
            //make order
            //move unit if
            //make decision
            // MoveToWorad
            //_dispatcher.PublishAsync()
        }
        await Task.FromResult(Task.CompletedTask);
    }

    private static BattleUnit? CalculateBestTargetFor(BattleUnit unit, Battle battlefield)
    {
        double min = 9999;
        BattleUnit? result = null;
        foreach (var u in battlefield.Units)
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
