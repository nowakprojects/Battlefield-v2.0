using Battlefield.Core.Domain.Creatures;
using Battlefield.Core.Events;
using Battlefield.Core.Events.Battlefield;
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
        public bool Started { get; private set; }
        public IEnumerable<BattleUnit> Units
        {
            get { return _units; }
            set { _units = new HashSet<BattleUnit>(value); }
        }
        public Battle(string name)
        {
            Name = name;
            Started = false;
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
        public BattleStarted StartBattle()
        {
            if (Started)
                throw new Exception("Battle is allready started.");
            Started = true;
            return new BattleStarted(Id);
        }
        public bool ContainsPlayer(Player owner)
        {
            return (owner.Equals(Player.GREEN) ||
               owner.Equals(Player.BLUE) ||
               owner.Equals(Player.RED));
        }
        public BattleUnit GetUnitOrThrow(Guid unitId)
        {
            var unit = Units.FirstOrDefault(x => x.Id == unitId);
            if (unit is null)
            {
                throw new Exception($"There is no Unit with '{unitId}' in battle with '{Id}'.");
            }
            return unit;
        }
        public bool ContiansPos(Coordinates position)
        {
            return position.X < Width && position.Y < Height && 
                position.X >= 0 && position.Y >= 0;
        }

        private void AddUnit(BattleUnit unit)
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
        private void RemoveUnit(BattleUnit unit)
        {
            var u = Units.SingleOrDefault(x => x.Id == unit.Id);
            if(u is null)
            {
                throw new Exception($"Unit with type: {unit.Type} was not found.");
            }
            _units.Remove(unit);
        }
        private void SetTileMapOnUnitCreating(BattleUnit unit)
        {
            if (unit.OccupiedTiles is null)
                unit.OccupiedTiles = new Tile[unit.Size.X, unit.Size.Y];
            for (int y = 0; y < unit.Size.Y; y++)
                for (int x = 0; x < unit.Size.X; x++)
                {
                    var tile = TileMap[unit.Position.X + x, unit.Position.Y + y];
                    if (tile.Blocked)
                        throw new Exception($"Tile ({tile.Coordinates.X},{tile.Coordinates.Y}) is blocked.");
                    tile.Blocked = true;
                    tile.Unit = unit;
                    unit.OccupiedTiles[x, y] = tile;
                }
        }
        private void SetTileMapOnUnitDeleting(BattleUnit unit)
        {
            if (unit.OccupiedTiles is null)
                return;
            foreach (var tile in unit.OccupiedTiles)
            {
                tile.Blocked = false;
                tile.Unit = null;
            }
        }
        public BattleUnit? UnitOnTile(Coordinates pos)
        {
            return UnitOnTile(pos.X, pos.Y);
        }
        public BattleUnit? UnitOnTile(int x, int y)
        {
            if(!ContiansPos(new Coordinates(x,y)))
                throw new Exception("Invalid position");
            return TileMap[x, y].Unit;
        }
        public UnitDeleted DeleteUnit(Coordinates pos)
        {
            var unit = UnitOnTile(pos);
            if (unit is null)
                throw new Exception($"There is no Unit on tile ({pos.X},{pos.Y}).");
            return DeleteUnit(unit);
        }
        public UnitDeleted DeleteUnit(BattleUnit unit)
        {
            SetTileMapOnUnitDeleting(unit);
            RemoveUnit(unit);
            return new UnitDeleted(unit);
        }
        public UnitCreated CreateUnit(Coordinates pos, Player owner, ICreature type)
        {
            if (!ContiansPos(pos))
            {
                throw new Exception("Invalid position");
            }
            if (!ContainsPlayer(owner))
            {
                throw new Exception($"Player {owner.ToString()} not found.");
            }
            
            var unit = new BattleUnit(type, pos, owner);
            AddUnit(unit);
            SetTileMapOnUnitCreating(unit);
            return new UnitCreated(unit.Id, Id);
        }
        public IEnumerable<IEvent> MakeMoveUnit(BattleUnit unit, Coordinates from, Coordinates to)
        {
            var eventsList = new List<IEvent>();
            SetTileMapOnUnitDeleting(unit);
            unit.ChangePos(to);
            unit.resetMoveCooldown();
            eventsList.Add(new UnitMoved(Id, unit.Id, from, to));
            SetTileMapOnUnitCreating(unit);
            return eventsList;
        }
    }
}
