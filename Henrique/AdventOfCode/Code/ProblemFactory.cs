namespace AdventOfCode.Code
{
    internal static class ProblemFactory
    {
        internal static Problem? GetProblem(int year, int dayNumber)
        {
            switch (year)
            {
                case 2015:
                    switch (dayNumber)
                    {
                        case 1:
                            return new Problem_2015_1();
                        default: return null;
                    }
                default:
                    return null;
            }
        }

    }
}
