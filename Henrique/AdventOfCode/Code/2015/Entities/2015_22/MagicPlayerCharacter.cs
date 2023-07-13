using AdventOfCode.Code._2015.Entities._2015_21;

namespace AdventOfCode.Code._2015.Entities._2015_22
{
    internal class MagicPlayerCharacter : PlayerCharacter
    {
        internal int Mana { get; set; }

        public MagicPlayerCharacter(int hitPoints, int mana) : base(hitPoints)
        {
            Mana = mana;
        }
        internal int GetMana()
        {
            return Mana;
        }

    }
}
