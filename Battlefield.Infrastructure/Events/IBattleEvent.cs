using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlefield.Infrastructure.Events
{
    interface IBattleEvents : IEvent
    {
        Core.Domain.Battle Battlefield { get; set; }
    }
}
