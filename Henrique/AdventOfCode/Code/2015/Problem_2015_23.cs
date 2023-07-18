using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    internal class Problem_2015_23 : Problem
    {
        private const string InstructionPattern = @"^(?<instruction>\w)+ (?<register>\w)*(, (?<sign1>[+-])+(?<value1>\d)+)*((?<sign2>[+-])+(?<value2>\d)+)*$";

        internal Problem_2015_23() : base()
        {
        }

        internal override string Solve()
        {
            Regex pattern = new Regex(InstructionPattern, RegexOptions.Compiled);
            try
            {
                foreach(string line in InputLines)
                {
                    Match match = pattern.Match(line);
                    

                }
            }
            catch
            {
                throw new Exception("Invalid line in input.");
            }

            string part1 = SolvePart1();
            string part2 = SolvePart2();

            return $"Part 1 solution: " + part1 + "\n"
                + "Part 2 solution: " + part2;
        }

        private string SolvePart1()
        {
            throw new Exception("No solution found.");
        }

        private string SolvePart2()
        {
            throw new Exception("No solution found.");
        }
    }
}