namespace Battlefield.Core.Domain;

public class TileSize
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public TileSize(int x, int y)
    {
        X = x;
        Y = y;
    }
}

