﻿namespace AdventOfCode.Code
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
                            return new Problem_2015_01();
                        case 2:
                            return new Problem_2015_02();
                        case 3:
                            return new Problem_2015_03();
                        case 4:
                            return new Problem_2015_04();
                        case 5:
                            return new Problem_2015_05();
                        case 6:
                            return new Problem_2015_06();
                        case 7:
                            return new Problem_2015_07();
                        case 8:
                            return new Problem_2015_08();
                        case 9:
                            return new Problem_2015_09();
                        case 10:
                            return new Problem_2015_10();
                        default: return null;
                    }
                default:
                    return null;
            }
        }

    }
}
