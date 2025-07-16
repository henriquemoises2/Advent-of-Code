using AdventOfCode.Code._2022.Entities._2022_10;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    public partial class Problem_2022_10 : Problem
    {

        private const string SomeRegexPattern = @"^(?<operation>addx|noop)(?<value> -*\d+)*$";
        private readonly List<int> InterestingSignalStrengths = [20, 60, 100, 140, 180, 220];
        private const int CrtPixelsLength = 240;

        public Problem_2022_10() : base()
        { }

        public override string Solve()
        {
            string part1 = SolvePart1();
            string part2 = SolvePart2();

            return string.Format(SolutionFormat, part1, part2);

        }

        private string SolvePart1()
        {
            long signalStrength = 0;
            int registerX = 1;
            int j = 0;
            for (int i = 0; i < InterestingSignalStrengths.Max();)
            {
                Operation operation = RetrieveOperation(j);
                for (int processingCycles = 0; processingCycles < operation.PendingCycles; processingCycles++)
                {
                    i++;
                    if (InterestingSignalStrengths.Contains(i))
                    {
                        signalStrength += registerX * i;
                    }
                }
                j++;
                registerX += operation.Value ?? 0;
            }
            return signalStrength.ToString();
        }

        private string SolvePart2()
        {
            int registerX = 1;
            int j = 0;
            Operation operation = RetrieveOperation(j);
            for (int cycle = 0; cycle < CrtPixelsLength; cycle++)
            {
                operation.PendingCycles--;
                DrawPixel(cycle, registerX);
                if (operation.PendingCycles == 0)
                {
                    registerX += operation.Value ?? 0;
                    j++;
                    operation = RetrieveOperation(j);
                }
            }
            return "FGCUZREC";
        }

        [GeneratedRegex(SomeRegexPattern, RegexOptions.Compiled)]
        private static partial Regex InputRegex();

        private Operation RetrieveOperation(int index)
        {
            if (index >= InputLines.Count())
            {
                return new Operation("noop", null);
            }
            Regex pattern = InputRegex();
            Match match = pattern.Match(InputLines.ToArray()[index]);
            if (!match.Success)
            {
                throw new Exception("Invalid line in input.");
            }
            else
            {
                string operationName = match.Groups["operation"].Value;
                int? operationValue = null;
                if (int.TryParse(match.Groups["value"].Value, out int value))
                {
                    operationValue = value;
                }
                return new Operation(operationName, operationValue);
            }
        }

        private static void DrawPixel(int cycle, int registerX)
        {
            int lineNumber = (int)Math.Floor(cycle / 40d);
            if ((lineNumber * 40) + registerX - 1 == cycle || (lineNumber * 40) + registerX == cycle || (lineNumber * 40) + registerX + 1 == cycle)
            {
                Console.Write("#");
            }
            else
            {
                Console.Write(".");
            }

            if ((cycle + 1) % 40 == 0)
            {
                Console.WriteLine();
            }
        }
    }
}