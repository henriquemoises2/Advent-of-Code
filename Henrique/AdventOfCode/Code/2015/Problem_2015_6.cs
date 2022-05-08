using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    internal class Problem_2015_6 : Problem
    {
        const string Pattern = "^(.*) (\\d+),(\\d+) through (\\d+),(\\d+)$";
        const int GridSize = 1000;
        bool[,] LightsGrid;

        private enum Operation
        {
            TurnOn = 1,
            TurnOff = 2,
            Toggle = 3
        }

        internal Problem_2015_6() : base()
        {
            LightsGrid = new bool[GridSize, GridSize];
        }

        internal override string Solve()
        {
            string part1 = SolvePart1();
            string part2 = SolvePart2();

            return $"Part 1 solution: " + part1 + "\n"
                + "Part 2 solution: " + part2;
        }

        private string SolvePart1()
        {
            Regex regex = new Regex(Pattern, RegexOptions.Compiled);
            foreach(string line in InputLines)
            {
                Match match = regex.Match(line);
                Operation operationEnum;
                Tuple<int, int> startPosition;
                Tuple<int, int> endPosition;

                try
                {
                    string operation = match.Groups[1].Value.Replace(" ", "");

                    operationEnum = (Operation)Enum.Parse(typeof(Operation), operation, true);

                    string initialPositionX = match.Groups[2].Value;
                    string initialPositionY = match.Groups[3].Value;
                    startPosition = Tuple.Create(int.Parse(initialPositionX), int.Parse(initialPositionY));

                    string finalPositionX = match.Groups[4].Value;
                    string finalPositionY = match.Groups[5].Value;
                    endPosition = Tuple.Create(int.Parse(finalPositionX), int.Parse(finalPositionY));

                    UpdateGrid(operationEnum, startPosition, endPosition);

                }
                catch (Exception)
                {
                    throw new Exception("Invalid line in input file.");
                }
                
            }
            return "";
        }

        private string SolvePart2()
        {
            return "";
        }

        private void UpdateGrid(Operation operation, Tuple<int, int> startPosition, Tuple<int, int> endPosition)
        {

        }
    }
}
