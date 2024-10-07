using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    public partial class Problem_2015_06 : Problem
    {
        const string Pattern = @"^(.*) (\d+),(\d+) through (\d+),(\d+)$";
        const int GridSize = 1000;
        private readonly int[,] LightsGrid;
        private readonly int[,] BrightnessGrid;

        private enum Operation
        {
            TurnOn = 1,
            TurnOff = 2,
            Toggle = 3
        }

        public Problem_2015_06() : base()
        {
            LightsGrid = new int[GridSize, GridSize];
            BrightnessGrid = new int[GridSize, GridSize];
        }

        public override string Solve()
        {
            string part1 = SolvePart1();
            string part2 = SolvePart2();

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";

        }

        private string SolvePart1()
        {
            Regex regex = InputRegex();
            foreach (string line in InputLines)
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

                    UpdateLightsGrid(operationEnum, startPosition, endPosition);
                    UpdateBrightnessGrid(operationEnum, startPosition, endPosition);
                }
                catch (Exception)
                {
                    throw new Exception("Invalid line in input file.");
                }
            }

            return CountTotalTurnedOnLights().ToString();
        }

        private string SolvePart2()
        {
            return CountTotalBrightness().ToString(); ;
        }

        private void UpdateLightsGrid(Operation operation, Tuple<int, int> startPosition, Tuple<int, int> endPosition)
        {
            int xi = startPosition.Item1;
            int yi = startPosition.Item2;

            int xf = endPosition.Item1;
            int yf = endPosition.Item2;

            // Select all positions inside given area (from startPosition until endPosition)
            for (int y = yi; y <= yf; y++)
            {
                for (int x = xi; x <= xf; x++)
                {
                    if (operation == Operation.TurnOn)
                    {
                        //UpdateLightsCounter(LightsGrid[x, y], true);
                        LightsGrid[x, y] = 1;
                    }
                    else if (operation == Operation.TurnOff)
                    {
                        //UpdateLightsCounter(LightsGrid[x, y], false);
                        LightsGrid[x, y] = 0;
                    }
                    else
                    {
                        //UpdateLightsCounter(LightsGrid[x, y], !LightsGrid[x, y]);
                        if (LightsGrid[x, y] == 1)
                        {
                            LightsGrid[x, y] = 0;
                        }
                        else
                        {
                            LightsGrid[x, y] = 1;
                        }
                    }
                }
            }
        }

        private void UpdateBrightnessGrid(Operation operation, Tuple<int, int> startPosition, Tuple<int, int> endPosition)
        {
            int xi = startPosition.Item1;
            int yi = startPosition.Item2;

            int xf = endPosition.Item1;
            int yf = endPosition.Item2;

            // Select all positions inside given area (from startPosition until endPosition)
            for (int y = yi; y <= yf; y++)
            {
                for (int x = xi; x <= xf; x++)
                {
                    if (operation == Operation.TurnOn)
                    {
                        BrightnessGrid[x, y] += 1;
                    }
                    else if (operation == Operation.TurnOff)
                    {
                        BrightnessGrid[x, y] = Math.Max(BrightnessGrid[x, y] - 1, 0);
                    }
                    else
                    {
                        BrightnessGrid[x, y] += 2;
                    }
                }
            }
        }

        /// <summary>
        /// It is more efficient to count all the true values at the end instead of keeping record of the changes.
        /// </summary>
        /// <returns>Total number of lights that are turned on in the LightsGrid</returns>
        private int CountTotalTurnedOnLights()
        {
            int total = 0;
            foreach (var elem in LightsGrid)
            {
                if (elem == 1)
                {
                    total++;
                }
            }
            return total;
        }

        /// <summary>
        /// It is more efficient to count all the true values at the end instead of keeping record of the changes.
        /// </summary>
        /// <returns>Total brightness of BrightnessGrid</returns>
        private int CountTotalBrightness()
        {
            int total = 0;
            foreach (var elem in BrightnessGrid)
            {
                total += elem;
            }
            return total;
        }

        [GeneratedRegex(Pattern, RegexOptions.Compiled)]
        private static partial Regex InputRegex();
    }
}
