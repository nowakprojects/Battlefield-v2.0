using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlefield.Core.Domain
{
    public class UnitStatistic
    {
        public int AttackDmg { get; set; }
        public float MoveSpeed { get; set; }
        public float AttackSeed { get; set; }
        public int Hp { get; set; }
        public bool CanShoot { get; set; }
        public int Ammo { get; set; }

        public UnitStatistic() { }

    }
}
