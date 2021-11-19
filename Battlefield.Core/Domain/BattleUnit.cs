using Battlefield.Core.Domain.Creatures;
using Battlefield.Core.Domain.Orders;

namespace Battlefield.Core.Domain
{
    public class BattleUnit
    {
        public Guid Id { get; set; }
        public UnitStatistic Statistic { get; set; }
        public ICreature Type { get; set; }
        public Coordinates Position { get; set; }
        public BattleUnit? Target { get; set; }
        public IOrder Order { get; set; }
        public Player Owner { get; set; }
        public bool IsBig { get; set; }

        public BattleUnit()
        {
            Id = Guid.NewGuid();
            Statistic = new UnitStatistic
            {
                AttackDmg = 30,
                MoveSpeed = 1.0f,
                AttackSeed = 0.5f,
                Hp = 100,
                CanShoot = false,
                Ammo = 0
            };
            Type = new Griffin();
            Position = new Coordinates(0,0);
            Target = null;
            Order = new WaitOrder();
            Owner = Player.BLUE;
            IsBig = true;

        }

    }
}
