using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    internal class Problem_YEAR_DAY : Problem
    {

        private const string SomeRegexPattern = @"";

        internal Problem_YEAR_DAY() : base()
        { }

        internal override string Solve()
        {
            Regex pattern = new Regex(SomeRegexPattern, RegexOptions.Compiled);
            foreach (string line in InputLines)
            {
                Match match = pattern.Match(line);
                if (!match.Success)
                {
                    throw new Exception("Invalid line in input.");
                }
                else
                {
                }
            }

            string part1 = SolvePart1();
            string part2 = SolvePart2();

            return $"Part 1 solution: " + part1 + "\n"
                + "Part 2 solution: " + part2;
        }

        private string SolvePart1()
        {
            return "";
        }

        private string SolvePart2()
        {
            return "";
        }
    }
}