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
        internal int CalculatePotentialManaSpent()
        {
            return Spells.Sum(spell => spell.CalculatePotentialManaSpent());
        }

        internal int CalculatePotentialDamageToPlayer(int bossDamage)
        {
            int shieldSpells = Spells.Count(s => s.GetType() == typeof(Shield));
            Shield template = new Shield();
            int potentialDamageToPlayer = bossDamage * (Spells.Count - 1);
            int potentialDamageReduction = 0;
            
            if(template.Armor >= bossDamage)
            {
                potentialDamageReduction = ((bossDamage - 1) * template.TotalDuration) * Spells.Count(s => s.GetType() == typeof(Shield));
            }
            else
            {
                potentialDamageReduction = (template.Armor * template.TotalDuration) * Spells.Count(s => s.GetType() == typeof(Shield));
            }            

            int totalDamageToPlayer = potentialDamageToPlayer - potentialDamageReduction;
            return totalDamageToPlayer;
        }

        internal bool IsSpellOrderValid()
        {
            for (int i = 0; i < Spells.Count; i++)
            {
                Spell spell = Spells[i];
                if (!spell.HasImmediateEffect())
                {
                    List<Type> previousSpells = Spells.Where((value, index) => index >= Math.Max(0, i - spell.TotalDuration) && index < i).Select(spell => spell.GetType()).ToList();
                    if (previousSpells.Contains(spell.GetType()))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
