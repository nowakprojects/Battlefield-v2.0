using Battlefield.Core.Events;
using Battlefield.Core.Events.BattleUnit;

namespace Battlefield.Core.Domain
{
    public class Battle
    {
        private ISet<BattleUnit> _units = new HashSet<BattleUnit>();

        private TileSize _tileSize;
        public string Name { get; set; }
        public Guid Id { get; protected set; }
        public Tile[,] TileMap { get; protected set; }
        public int Height => _tileSize.Y;
        public int Width => _tileSize.X;
        public IEnumerable<BattleUnit> Units
        {
            get { return _units; }
            set { _units = new HashSet<BattleUnit>(value); }
        }
        public Battle()
        {
            Name = "";
            Id = Guid.NewGuid();
            _tileSize = new TileSize(23,14);
            TileMap = new Tile[Height, Width];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    TileMap[y, x] = new Tile(x, y, false);
                }
            }
        }

        public bool ContainsPlayer(Player owner)
        {
            return (owner.Equals(Player.GREEN) ||
               owner.Equals(Player.BLUE) ||
               owner.Equals(Player.RED));
        }

        public bool ContiansPos(Coordinates position)
        {
            return position.X < Width && position.Y < Height && 
                position.X >= 0 && position.Y >= 0;
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
        
        public IEvent CreateUnit(Coordinates pos, Player owner, ICreature type)
        {
            if (ContiansPos(pos))
            {
                throw new Exception("Invalid position");
            }
            if (ContainsPlayer(owner))
            {
                throw new Exception($"Player {owner.ToString()} not found.");
            }
            var unit = new BattleUnit(type, pos, owner);
            return new UnitCreated(unit, Id);
        }
    }
}
