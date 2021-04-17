using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterModify
{
    public class Monster
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public string HpMax { get; set; }
        public string PhysicalAttack { get; set; }
        public string PhysicalDefense { get; set; }
        public string MagicalAttack { get; set; }
        public string MagicalDefense { get; set; }



        public string Level { get; set; }

        public string DarkResistance { get; set; }
        public string LightResistance { get; set; }
        public string PoisonResistance { get; set; }
        public string CurseResistance { get; set; }
        public string FireResistance { get; set; }
        public string WaterResistance { get; set; }
        public string SlowResistance { get; set; }
        public string FreezeResistance { get; set; }
        public string StunResistance { get; set; }
        public string BlindResistance { get; set; }
        public string LightningResistance { get; set; }
        public string StoneResistance { get; set; }
        public string BleedingResistance { get; set; }
        public string ConfuseResistance { get; set; }
        public string HoldResistance { get; set; }
        public string BurnResistance { get; set; }
        public string SleepResistance { get; set; }
        public string PiercingResistance { get; set; }
        public string StuckResistance { get; set; }
        public string WeaponBreakResistance { get; set; }

        public string HpRegenSpeed { get; set; }
        public string MpRegenSpeed { get; set; }
        public string MoveSpeed { get; set; }
        public string AttackSpeed { get; set; }
        public string CastSpeed { get; set; }
        public string JumpSpeed { get; set; }
        public string FirstSpeed { get; set; }
        public string LastSpeed { get; set; }
        public string NormalDashSpeed { get; set; }
        public string SlantDashSpeed { get; set; }
        public string ATTACK_SPEED { get; set; }

        public string HitRecovery { get; set; }
        public string JumpPower { get; set; }
        public string TargetingTimeTerm { get; set; }
        public string Sight { get; set; }
        public string AttackDelay { get; set; }
        public string Warlike { get; set; }

        public string Cooltime { get; set; }
        public string DashTime { get; set; }
        public string DashReadyTime { get; set; }



        public Monster()
        {
        }

        public Monster(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}