namespace AdventOfCode.Code._2015.Entities._2015_22
{
    internal class SpellsLineup
    {
        internal SpellsLineup(List<Spell> spells)
        {
            Spells = spells;
        }

        internal List<Spell> Spells { get; set; }

        internal int TurnsSurvived { get; set; }
        internal int DamageDealtToBoss { get; set; }
        internal int ManaSpent { get; set; }

        internal bool IsSpellOrderValid()
        {
            for (int i = 0; i < Spells.Count; i++)
            {
                Spell spell = Spells[i];
                if (!spell.HasImmediateEffect())
                {
                    int duration = spell.TotalDuration - 2;
                    for (int j = i - 1; j >= 0 && duration > 0; j--)
                    {
                        if (Spells[j].GetType() == spell.GetType())
                        {
                            return false;
                        }
                        else
                        {
                            duration -= 2;
                        }
                    }
                }
            }
            return true;
        }

    }
}
