using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterModify
{
    public class Monster
    {
        public string Path { get; set; }
        public MonsterAttribute<string> Name { get; set; } = new("名称", @"name].*\n.*`(.+)`", 1, "name");

        public MonsterAttribute<string> Level { get; set; } = new("等级", "level");

        public MonsterAttribute<int> HpMax { get; set; } = new("血量", @"(.*`\[HP MAX\]`.*`\*`\t*)(.+?)([\n|\r\n|\t]+)", 2, "HP MAX");

        public MonsterAttribute<string> PhysicalAttack { get; set; } =
            new("物理攻击", @"(.*`\[EQUIPMENT_PHYSICAL_ATTACK\]`.*`\*`,*)(\d?)(.*)", 2, "EQUIPMENT_PHYSICAL_ATTACK");

        public MonsterAttribute<string> PhysicalDefense { get; set; } =
            new("物理防御", @"(.*`\[EQUIPMENT_PHYSICAL_DEFENSE\]`.*`\*`,*)(\d?)(.*)", 2, "EQUIPMENT_PHYSICAL_DEFENSE");

        public MonsterAttribute<string> MagicalAttack { get; set; } =
            new("魔法攻击", @"(.*`\[EQUIPMENT_MAGICAL_ATTACK\]`.*`\*`,*)(\d?)(.*)", 2, "EQUIPMENT_MAGICAL_ATTACK");

        public MonsterAttribute<string> MagicalDefense { get; set; } =
            new("魔法防御", @"(.*`\[EQUIPMENT_MAGICAL_DEFENSE\]`.*`\*`,*)(\d?)(.*)", 2, "EQUIPMENT_MAGICAL_DEFENSE");


        public MonsterAttribute<string> DarkResistance { get; set; } = new("暗属性抗性", "dark resistance");

        public MonsterAttribute<string> LightResistance { get; set; } = new("光属性抗性", "light resistance");

        public MonsterAttribute<string> PoisonResistance { get; set; } = new("中毒抗性", "poison resistance");

        public MonsterAttribute<string> CurseResistance { get; set; } = new("诅咒抗性", "curse resistance");

        public MonsterAttribute<string> FireResistance { get; set; } = new("火属性抗性", "fire resistance");

        public MonsterAttribute<string> WaterResistance { get; set; } = new("水属性抗性", "water resistance");

        public MonsterAttribute<string> SlowResistance { get; set; } = new("减速抗性", "slow resistance");

        public MonsterAttribute<string> FreezeResistance { get; set; } = new("冰属性抗性", "freeze resistance");

        public MonsterAttribute<string> StunResistance { get; set; } = new("眩晕抗性", "stun resistance");

        public MonsterAttribute<string> BlindResistance { get; set; } = new("失明抗性", "blind resistance");

        public MonsterAttribute<string> LightningResistance { get; set; } = new("感电抗性", "lightning resistance");

        public MonsterAttribute<string> StoneResistance { get; set; } = new("石化抗性", "stone resistance");

        public MonsterAttribute<string> BleedingResistance { get; set; } = new("出血抗性", "bleeding resistance");

        public MonsterAttribute<string> ConfuseResistance { get; set; } = new("混乱抗性", "confuse resistance");

        public MonsterAttribute<string> HoldResistance { get; set; } = new("持有?抗性", "hold resistance");

        public MonsterAttribute<string> BurnResistance { get; set; } = new("燃烧抗性", "burn resistance");

        public MonsterAttribute<string> SleepResistance { get; set; } = new("睡眠抗性", "sleep resistance");

        public MonsterAttribute<string> PiercingResistance { get; set; } = new("穿刺?抗性", "piercing resistance");

        public MonsterAttribute<string> StuckResistance { get; set; } = new("困住?抗性", "stuck resistance");

        public MonsterAttribute<string> WeaponBreakResistance { get; set; } = new("武器打破?抗性", "weapon break resistance");


        public MonsterAttribute<string> HpRegenSpeed { get; set; } = new("血恢复速度", "HP regen speed");
        public MonsterAttribute<string> MpRegenSpeed { get; set; } = new("蓝恢复速度", "MP regen speed");
        public MonsterAttribute<string> MoveSpeed { get; set; } = new("移动速度", "move speed");
        public MonsterAttribute<string> AttackSpeed { get; set; } = new("攻击速度", "attack speed");
        public MonsterAttribute<string> CastSpeed { get; set; } = new("施法速度", "cast speed");
        public MonsterAttribute<string> JumpSpeed { get; set; } = new("跳跃速度", "jump speed");


        public MonsterAttribute<string> HitRecovery { get; set; } = new("打击恢复?", "hit recovery");
        public MonsterAttribute<string> JumpPower { get; set; } = new("跳跃高度", "jump power");
        public MonsterAttribute<string> TargetingTimeTerm { get; set; } = new("目标期限?", "targeting time term");
        public MonsterAttribute<string> TargetingNearest { get; set; } = new("目标最近?", "targeting nearest");
        public MonsterAttribute<string> Sight { get; set; } = new("视力", "sight");
        public MonsterAttribute<string> AttackDelay { get; set; } = new("攻击延迟", "attack delay");
        public MonsterAttribute<string> Warlike { get; set; } = new("好战度", "warlike");
        public MonsterAttribute<string> StuckBonusOnDamage { get; set; } = new("伤害加成", "stuckbonus on damage");
        public MonsterAttribute<string> Vision { get; set; } = new("视野", "vision");
        public MonsterAttribute<string> SuperarmorOnAttack { get; set; } = new("攻击时霸体", "superarmor on attack");


        //  public MonsterAttribute Cooltime { get; set; }


        public Monster()
        {
        }

        public Monster(string name, string path)
        {
            Name.Value = name;
            Path = path;
        }

        public Monster(string path)
        {
            Path = path;
        }
    }
}