using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlefield.Core.Domain
{
    public class Coordinates
    {
        public readonly int X;
        public readonly int Y;
        public Coordinates (int x,int y)
        {
            X = x;
            Y = y;
        }
    }
}
