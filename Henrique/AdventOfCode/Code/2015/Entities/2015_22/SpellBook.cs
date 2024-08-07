﻿using AdventOfCode._2015_21;

namespace AdventOfCode._2015_22
{
    internal enum SpellType
    {
        MagicMissile = 1,
        Drain = 2,
        Shield = 3,
        Poison = 4,
        Recharge = 5
    }

    internal static class SpellFactory
    {
        internal static Spell GetSpell(SpellType spell)
        {
            return spell switch
            {
                SpellType.MagicMissile => new MagicMissile(),
                SpellType.Drain => new Drain(),
                SpellType.Shield => new Shield(),
                SpellType.Poison => new Poison(),
                SpellType.Recharge => new Recharge(),
                _ => throw new ArgumentOutOfRangeException(nameof(spell)),
            };
        }
    }

    internal class MagicMissile : Spell
    {
        private readonly int Damage = 4;

        internal MagicMissile()
        {
            ManaCost = 53;
            Type = SpellType.MagicMissile;
        }
        internal override void ApplyEffect(MagicPlayerCharacter pc, Boss boss)
        {
            boss.HitPoints -= Damage;
        }

        internal override int CalculatePotentialDamage()
        {
            return Damage;
        }

        internal override int CalculatePotentialManaSpent()
        {
            return ManaCost;
        }
    }

    internal class Drain : Spell
    {
        private readonly int Damage = 2;
        internal Drain()
        {
            ManaCost = 73;
            Type = SpellType.Drain;

        }
        internal override void ApplyEffect(MagicPlayerCharacter pc, Boss boss)
        {
            pc.HitPoints += 2;
            boss.HitPoints -= 2;
        }

        internal override int CalculatePotentialDamage()
        {
            return Damage;
        }

        internal override int CalculatePotentialManaSpent()
        {
            return ManaCost;
        }
    }

    internal class Shield : Spell
    {
        internal int Armor = 7;

        internal Shield()
        {
            ManaCost = 113;
            TotalDuration = 6;
            Timer = TotalDuration;
            Type = SpellType.Shield;
        }
        internal override void ApplyEffect(MagicPlayerCharacter pc, Boss boss)
        {
            if (Timer > 0)
            {
                pc.Armor = Armor;
                Timer--;
            }
        }

        internal override void RemoveEffect(MagicPlayerCharacter pc, Boss boss)
        {
            pc.Armor = 0;
        }

        internal override int CalculatePotentialDamage()
        {
            return 0;
        }

        internal override int CalculatePotentialManaSpent()
        {
            return ManaCost;
        }
    }

    internal class Poison : Spell
    {
        private readonly int Damage = 3;

        internal Poison()
        {
            ManaCost = 173;
            TotalDuration = 6;
            Timer = TotalDuration;
            Type = SpellType.Poison;
        }
        internal override void ApplyEffect(MagicPlayerCharacter pc, Boss boss)
        {
            if (Timer > 0)
            {
                boss.HitPoints -= Damage;
                Timer--;
            }
        }

        internal override int CalculatePotentialDamage()
        {
            return Damage * TotalDuration;
        }

        internal override int CalculatePotentialManaSpent()
        {
            return ManaCost;
        }

    }

    internal class Recharge : Spell
    {

        internal int ManaGain = 101;

        internal Recharge()
        {
            ManaCost = 229;
            TotalDuration = 5;
            Timer = TotalDuration;
            Type = SpellType.Recharge;
        }
        internal override void ApplyEffect(MagicPlayerCharacter pc, Boss boss)
        {
            if (Timer > 0)
            {
                pc.Mana += ManaGain;
                Timer--;
            }
        }

        internal override int CalculatePotentialDamage()
        {
            return 0;
        }

        internal override int CalculatePotentialManaSpent()
        {
            return ManaCost - (ManaGain * TotalDuration);
        }

    }
}
