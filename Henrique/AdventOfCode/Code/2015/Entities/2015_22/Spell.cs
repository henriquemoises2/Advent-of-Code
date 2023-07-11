using AdventOfCode.Code._2015.Entities._2015_21;

namespace AdventOfCode.Code._2015.Entities._2015_22
{
    internal abstract class Spell
    {
        internal int ManaCost { get; set; }
        internal int TotalDuration { get; set; }
        internal int Timer { get; set; }
        internal SpellType Type { get;  set;}

        internal void Cast(MagicPlayerCharacter pc)
        {
            pc.Mana -= ManaCost;
        }

        internal abstract void ApplyEffect(MagicPlayerCharacter pc, Boss boss);
        internal virtual void RemoveEffect(MagicPlayerCharacter pc, Boss boss)
        {

        }


        internal abstract int CalculatePotentialDamage();
        internal abstract int CalculatePotentialManaSpent();

        internal bool HasImmediateEffect()
        {
            return TotalDuration == 0;
        }

    }
}
