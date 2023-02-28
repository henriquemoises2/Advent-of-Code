using AdventOfCode.Code._2015.Entities._2015_21;

namespace AdventOfCode.Code._2015.Entities._2015_22
{
    internal class SpellsLineup
    {
        internal SpellsLineup(List<Spell> spells)
        {
            Spells = spells;
        }

        internal List<Spell> Spells { get; set; }

        internal int CalculatePotentialDamage()
        {
            return Spells.Sum(spell => spell.CalculatePotentialDamage());
        }
        internal int CalculatePotentialManaLost()
        {
            int totalManaLost = 0;
            for (int i = 0; i < Spells.Count; i++)
            {
                if (Spells[i].GetType() == typeof(Recharge))
                {
                    totalManaLost -= (((Recharge)Spells[i]).ManaGain * Math.Min(Spells.Count - i, Spells[i].TotalDuration)) - Spells[i].ManaCost;
                }
                else
                {
                    totalManaLost += Spells[i].ManaCost;
                }
            }

            return totalManaLost;
        }


        internal int CalculatePotentialDamageToPlayer(int bossDamage)
        {
            int totalDamageToPlayer = 0;
            for (int i = 0; i < Spells.Count; i++)
            {
                if (Spells[i].GetType() == typeof(Shield))
                {
                    totalDamageToPlayer += Math.Max(1, bossDamage - ((Shield)Spells[i]).Armor) * Math.Min(Spells.Count - i, Spells[i].TotalDuration);
                }
                else
                {
                    totalDamageToPlayer += bossDamage;
                }
            }

            return totalDamageToPlayer;
        }

    }
}
