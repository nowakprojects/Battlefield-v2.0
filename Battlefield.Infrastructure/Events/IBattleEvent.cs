using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlefield.Infrastructure.Events
{
    public interface IBattleEvent : IEvent
    {
        Core.Domain.Battlefield? Battlefield { get; set; }
    }
}
