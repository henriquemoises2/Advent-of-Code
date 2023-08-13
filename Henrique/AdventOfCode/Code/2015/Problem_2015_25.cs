using AdventOfCode._2015_7;
using System.Data;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    internal class Problem_2015_25 : Problem
    {
        private const string ManualInstructionPattern = @"^To continue, please consult the code grid in the manual.  Enter the code at row (?<row>\d+), column (?<column>\d+)\.$";
        private const int MaxIterations = 10000000;


        internal Problem_2015_25() : base()
        {
        }

        internal override string Solve()
        {
            Regex pattern = new Regex(ManualInstructionPattern, RegexOptions.Compiled);
            int row, column;
            try
            {
                Match match = pattern.Match(InputFirstLine);
                row = int.Parse(match.Groups["row"].Value);
                column = int.Parse(match.Groups["column"].Value);

            }
            catch
            {
                throw new Exception("Invalid line in input.");
            }

            string part1 = SolvePart1(row, column);
            string part2 = SolvePart2();

            return $"Part 1 solution: " + part1 + "\n"
                + "Part 2 solution: " + part2;
        }

        private string SolvePart1(int row, int column)
        {
            double value = 20151125;
            Tuple<int, int> coordinates = new Tuple<int, int>(1, 1);

            do
            {
                value = ComputeNextValue(value);
                coordinates = ComputeNextCoordinates(coordinates);
            } 
            while (!(coordinates.Item1 == column && coordinates.Item2 == row));
            return value.ToString();
        }

        private string SolvePart2()
        {
            return "";
        }

        private double ComputeNextValue(double value)
        {
            return (value * 252533) % 33554393;
        }

        private Tuple<int, int> ComputeNextCoordinates(Tuple<int, int> coords)
        {
            int x, y;
            if (coords.Item2 == 1)
            {
                x = 1;
                y = coords.Item1 + 1;
            }
            else
            {
                x = coords.Item1 + 1;
                y = coords.Item2 - 1;
            }

            return new Tuple<int, int>(x, y);
        }
    }
}