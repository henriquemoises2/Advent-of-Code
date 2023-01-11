using AdventOfCode.Code._2015.Entities._2015_21;

namespace AdventOfCode.Code._2015.Entities._2015_22
{
    internal enum AvailableSpells
    {
        MagicMissile = 1,
        Drain = 2,
        Shield = 3,
        Poison = 4,
        Recharge = 5
    }

    internal static class SpellFactory
    {
        internal static Spell GetSpell(AvailableSpells spell)
        {
            switch(spell) {
                case AvailableSpells.MagicMissile:
                    return new MagicMissile();
                case AvailableSpells.Drain:
                    return new Drain();
                case AvailableSpells.Shield:
                    return new Shield();
                case AvailableSpells.Poison:
                    return new Poison();
                case AvailableSpells.Recharge:
                    return new Recharge();
                    default: throw new ArgumentOutOfRangeException(nameof(spell));
            }
        }
    }

    internal class MagicMissile : Spell
    {
        internal MagicMissile()
        {
            ManaCost = 53;
        }
        internal override void ApplyEffect(MagicPlayerCharacter pc, Boss boss)
        {
            boss.HitPoints -= 4;
        }
    }

    internal class Drain : Spell
    {
        internal Drain()
        {
            ManaCost = 73;
        }
        internal override void ApplyEffect(MagicPlayerCharacter pc, Boss boss)
        {
            pc.HitPoints += 2;
            boss.HitPoints -= 2;
        }
    }

    internal class Shield : Spell
    {
        internal Shield()
        {
            ManaCost = 113;
            TotalDuration = 6;
            Timer = TotalDuration;
        }
        internal override void ApplyEffect(MagicPlayerCharacter pc, Boss boss)
        {
            if (Timer > 0)
            {
                pc.Armor = 7;
                Timer--;
            }              
            
        }
    }

    internal class Poison : Spell
    {
        internal Poison()
        {
            ManaCost = 173;
            TotalDuration = 6;
            Timer = TotalDuration;
        }
        internal override void ApplyEffect(MagicPlayerCharacter pc, Boss boss)
        {
            if (Timer > 0)
            {
                boss.HitPoints -= 3;
                Timer--;
            }
        }
    }

    internal class Recharge : Spell
    {
        internal Recharge()
        {
            ManaCost = 229;
            TotalDuration = 5;
            Timer = TotalDuration;
        }
        internal override void ApplyEffect(MagicPlayerCharacter pc, Boss boss)
        {
            if (Timer > 0)
            {
                pc.Mana += 101;
                Timer--;
            }
        }
    }
}
