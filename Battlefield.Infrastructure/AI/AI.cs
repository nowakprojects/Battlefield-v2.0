using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battlefield.Core.Domain;

namespace Battlefield.Infrastructure.AI
{
    public static class AI
    {
        public async static Task UnitUpdateAsync(float dt, Battle battlefield)
        {
            foreach (var unit in battlefield.Units)
            {
                ;
                //make order
                //move unit if
                //make decision
                // MoveToWorad
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
}
