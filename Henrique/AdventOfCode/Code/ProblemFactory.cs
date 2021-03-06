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
                        case 2:
                            return new Problem_2015_2();
                        case 3:
                            return new Problem_2015_3();
                        default: return null;
                    }
                default:
                    return null;
            }
        }

    }
}
