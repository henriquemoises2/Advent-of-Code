using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    public class Problem_YEAR_DAY : Problem
    {

        private const string SomeRegexPattern = @"";

        public Problem_YEAR_DAY() : base()
        { }

        public override string Solve()
        {
            Regex pattern = new(SomeRegexPattern, RegexOptions.Compiled);
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

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";

        }

        private static string SolvePart1()
        {
            return "";
        }

        private static string SolvePart2()
        {
            return "";
        }
    }
}