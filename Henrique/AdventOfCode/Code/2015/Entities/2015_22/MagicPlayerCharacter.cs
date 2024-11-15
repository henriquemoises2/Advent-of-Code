using AdventOfCode._2015_21;

namespace AdventOfCode._2015_22
{
    internal class MagicPlayerCharacter(int hitPoints, int mana) : PlayerCharacter(hitPoints)
    {
        internal int Mana { get; set; } = mana;

        internal int GetMana()
        {
            return Mana;
        }

    }
}
