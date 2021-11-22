using Battlefield.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlefield.Infrastructure.Commands.BattleUnit
{
    public class CreateUnit : ICommand
    {
        public Coordinates? Position { get; set; }
        public ICreature? Type { get; set; }
        public Player Owner { get; set; }

    }
}
