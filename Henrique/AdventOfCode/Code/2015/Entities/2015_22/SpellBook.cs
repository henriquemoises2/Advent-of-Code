using AdventOfCode.Code._2015.Entities._2015_21;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode.Code._2015.Entities._2015_22
{
    internal enum AvailableSpell
    {
        MagicMissile = 1,
        Drain = 2,
        Shield = 3,
        Poison = 4,
        Recharge = 5
    }

    internal static class SpellFactory
    {
        internal static Spell GetSpell(AvailableSpell spell)
        {
            switch (spell)
            {
                case AvailableSpell.MagicMissile:
                    return new MagicMissile();
                case AvailableSpell.Drain:
                    return new Drain();
                case AvailableSpell.Shield:
                    return new Shield();
                case AvailableSpell.Poison:
                    return new Poison();
                case AvailableSpell.Recharge:
                    return new Recharge();
                default: throw new ArgumentOutOfRangeException(nameof(spell));
            }
        }
    }

    internal class MagicMissile : Spell
    {
        private int Damage = 4;

        internal MagicMissile()
        {
            ManaCost = 53;
        }
        internal override void ApplyEffect(MagicPlayerCharacter pc, Boss boss)
        {
            boss.HitPoints -= Damage;
        }

        internal override int CalculatePotentialDamage()
        {
            return Damage;
        }

        internal override int CalculatePotentialManaLost()
        {
            return ManaCost;
        }
    }

    internal class Drain : Spell
    {
        private int Damage = 2;
        internal Drain()
        {
            ManaCost = 73;
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

        internal override int CalculatePotentialManaLost()
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

        internal override int CalculatePotentialManaLost()
        {
            return ManaCost;
        }
    }

    internal class Poison : Spell
    {
        private int Damage = 3;

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
                boss.HitPoints -= Damage;
                Timer--;
            }
        }

        internal override int CalculatePotentialDamage()
        {
            return Damage * TotalDuration;
        }

        internal override int CalculatePotentialManaLost()
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

        internal override int CalculatePotentialManaLost()
        {
            return -ManaCost * TotalDuration;
        }
    }
}
