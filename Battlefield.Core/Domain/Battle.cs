namespace Battlefield.Core.Domain
{
    public class Battle
    {
        private ISet<BattleUnit> _units = new HashSet<BattleUnit>();
        public Guid Id { get; protected set; }

        public Tile[,] TileMap { get; protected set; }
        public int Height { get; protected set; }
        public int Width { get; protected set; }
        public IEnumerable<BattleUnit> Units
        {
            get { return _units; }
            set { _units = new HashSet<BattleUnit>(value); }
        }
        public Battle()
        {
            Id = Guid.NewGuid();
            Height = 14;
            Width = 30;
            TileMap = new Tile[Height, Width];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    TileMap[y, x] = new Tile(x, y, false);
                }
            }
        }

        public void AddUnit(BattleUnit unit)
        {
            if (unit == null)
            {
                throw new ArgumentNullException();
            }
            var u = Units.SingleOrDefault(x => x.Id == unit.Id);
            if(u != null)
            {
                throw new Exception($"Unit with type: '{unit.Type.ToString()}' already exists.");
            }
            _units.Add(unit);
        }
        public void DeleteUnit(BattleUnit unit)
        {
            var u = Units.SingleOrDefault(x => x.Id == unit.Id);
            if(u == null)
            {
                throw new Exception($"Unit with type: {unit.Type} was not found.");
            }
            _units.Remove(unit);
        }
    }
}
