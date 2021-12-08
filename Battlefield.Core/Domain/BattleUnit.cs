using Battlefield.Core.Domain.Creatures;
using Battlefield.Core.Domain.Orders;
using Battlefield.Core.Events;
using Battlefield.Core.Events.BattleUnit;

namespace Battlefield.Core.Domain
{
    public class BattleUnit
    {
        private float moveCooldown;
        private float actualMoveCooldown;

        public bool CanMove()
        {
            return (actualMoveCooldown <= 0);
        }

        private float attackCooldown;
        private float actualAttackCooldown;

        public Guid Id { get; protected set; }
        public UnitStatistic Statistic { get; set; }
        public ICreature Type { get; protected set; }
        public Coordinates Position { get; protected set; }
        public TileSize Size { get; protected set; }
        public Tile[,]? OccupiedTiles { get; set; }

        public void UpdateCooldowns(float dt)
        {
            if (actualMoveCooldown > 0)
                actualMoveCooldown -= dt;
            if (actualAttackCooldown > 0)
                actualAttackCooldown -= dt;
        }

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

        public IEnumerable<IEvent> GiveOrder(IOrder order)
        {
            Order = order;
            var events = new List<IEvent>();
            events.Add(new UnitOrderChanged(Id, order));
            return events;
        }
        public void resetMoveCooldown()
        {
            actualMoveCooldown = moveCooldown;
        }


        public void ChangePos(Coordinates newPos)
        {
            Position = newPos;
        }

    }
}
