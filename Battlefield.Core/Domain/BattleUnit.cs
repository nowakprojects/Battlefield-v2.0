using Battlefield.Core.Domain.Creatures;
using Battlefield.Core.Domain.Orders;

namespace Battlefield.Core.Domain
{
    public class BattleUnit
    {
        private float moveCooldown;
        private float actualMoveCooldown;
        private float attackCooldown;
        private float actualAttackCooldown;

        public Guid Id { get; protected set; }
        public UnitStatistic Statistic { get; set; }
        public ICreature Type { get; protected set; }
        public Coordinates Position { get; set; }
        public TileSize Size { get; protected set; }
        public Tile[,]? OccupiedTiles { get; set; }
        public BattleUnit? Target { get; set; }
        public IOrder Order { get; set; }
        public Player Owner { get; set; }
        public bool IsBig { get; set; }

        public BattleUnit()
        {
            Id = Guid.NewGuid();
            Statistic = new UnitStatistic();
            
            Type = new Griffin();
            Position = new Coordinates(0,0);
            Size = new TileSize(1, 1);
            Target = null;
            OccupiedTiles = null;
            Order = new WaitOrder();
            Owner = Player.BLUE;
        }
        public BattleUnit(ICreature type, Coordinates pos, Player owner)
        {
            Id = Guid.NewGuid();
            Statistic = new UnitStatistic();
            Type = type;
            Position = pos;
            Size = new TileSize(1, 1);
            Owner = owner;
            Order = new WaitOrder();
        }

        public void SetOrder(IOrder order)
        {
            Order = order;
        }

        public void ChangePos(Coordinates newPos)
        {
            Position = newPos;
        }

    }
}
