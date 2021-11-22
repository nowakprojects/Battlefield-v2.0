using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlefield.Core.Domain
{
    public class TileSize
    {
        public readonly int X;
        public readonly int Y;
        public TileSize(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
