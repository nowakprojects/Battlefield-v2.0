namespace Battlefield.Core.Domain
{
    public class Tile
    {
        public BattleUnit? Unit { get; set; }
        public bool Blocked { get; set; }
        public Coordinates Coordinates { get; set; }

        public Tile(Coordinates coor, bool block)
        {
            Coordinates = coor;
            Blocked = block;
        }
        public Tile(int x, int y, bool block) 
        {
            Coordinates = new Coordinates(x, y);
            Blocked = block;
        }
    }
}
