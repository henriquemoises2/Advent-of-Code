namespace AdventOfCode.Code
{
    internal static class ProblemFactory
    {
        internal static Problem? GetProblem(int year, int dayNumber)
        {
            return year switch
            {
                2015 => dayNumber switch
                {
                    1 => new Problem_2015_01(),
                    2 => new Problem_2015_02(),
                    3 => new Problem_2015_03(),
                    4 => new Problem_2015_04(),
                    5 => new Problem_2015_05(),
                    6 => new Problem_2015_06(),
                    7 => new Problem_2015_07(),
                    8 => new Problem_2015_08(),
                    9 => new Problem_2015_09(),
                    10 => new Problem_2015_10(),
                    11 => new Problem_2015_11(),
                    12 => new Problem_2015_12(),
                    13 => new Problem_2015_13(),
                    14 => new Problem_2015_14(),
                    15 => new Problem_2015_15(),
                    16 => new Problem_2015_16(),
                    17 => new Problem_2015_17(),
                    18 => new Problem_2015_18(),
                    19 => new Problem_2015_19(),
                    20 => new Problem_2015_20(),
                    21 => new Problem_2015_21(),
                    22 => new Problem_2015_22(),
                    23 => new Problem_2015_23(),
                    24 => new Problem_2015_24(),
                    25 => new Problem_2015_25(),
                    _ => null,
                },
                2022 => dayNumber switch
                {
                    1 => new Problem_2022_01(),
                    2 => new Problem_2022_02(),
                    3 => new Problem_2022_03(),
                    4 => new Problem_2022_04(),
                    5 => new Problem_2022_05(),
                    6 => new Problem_2022_06(),
                    7 => new Problem_2022_07(),
                    8 => new Problem_2022_08(),
                    9 => new Problem_2022_09(),
                    10 => new Problem_2022_10(),
                    _ => null,
                },
                _ => null,
            };
        }

    }
}
