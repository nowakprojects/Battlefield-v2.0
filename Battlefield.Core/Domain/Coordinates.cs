namespace Battlefield.Core.Domain;
public class Coordinates
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public Coordinates(int x, int y)
    {
        X = x;
        Y = y;
    }
    public bool Equals(Coordinates coor)
    {
        return X == coor.X && Y == coor.Y;
    }
}
