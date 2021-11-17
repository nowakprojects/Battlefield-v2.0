namespace Battlefield.Core.Domain
{
    public class BattleUnit
    {
        public Guid Id { get; set; }
        public UnitStatistic Statistic { get; set; }
        public MonsterType Type { get; set; }
        public Coordinates Position { get; set; }
        public BattleUnit? Target { get; set; }
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
            Type = MonsterType.GRIFFIN;
            Position = new Coordinates(0,0);
            Target = null;
            IsBig = true;

        }

    }
}
