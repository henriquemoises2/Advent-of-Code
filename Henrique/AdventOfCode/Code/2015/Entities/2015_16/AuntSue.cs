namespace AdventOfCode._2015_16
{
    internal class AuntSue
    {
        internal readonly int Number;
        internal readonly IEnumerable<Compound> Compounds;

        internal AuntSue(int number, IEnumerable<Compound> compounds)
        {
            Number = number;
            Compounds = compounds;
        }
    }
}
