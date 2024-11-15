using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    public partial class Problem_2022_05 : Problem
    {

        private const string InstructionPattern = @"move (?<nCrates>\d+) from (?<from>\d+) to (?<to>\d+)";

        public Problem_2022_05() : base()
        {
        }

        public override string Solve()
        {
            Dictionary<int, IEnumerable<char>> stacks = [];
            List<Tuple<int, int, int>> instructions = [];

            try
            {
                string[] inputLinesAsArray = InputLines.ToArray();
                int stackNumberIndex = InputLines.ToList().FindIndex(line => string.IsNullOrWhiteSpace(line)) - 1;
                int maxColumnHeight = stackNumberIndex;
                string stackNumbers = inputLinesAsArray[stackNumberIndex];

                for (int i = 0; i < stackNumbers.Length; i++)
                {
                    if (stackNumbers[i] != ' ')
                    {
                        int stackNumber = stackNumbers[i] - '0';
                        List<char> crates = [];
                        for (int j = maxColumnHeight - 1; j >= 0; j--)
                        {
                            if (inputLinesAsArray[j][i] != ' ' && inputLinesAsArray[j][i] != '[' && inputLinesAsArray[j][i] != ']')
                            {
                                crates.Add(inputLinesAsArray[j][i]);
                            }
                        }
                        stacks.Add(stackNumber, crates);
                    }
                }

                Regex pattern = InputRegex();
                MatchCollection match = pattern.Matches(string.Join("/n", InputLines));

                for (int i = 0; i < match.Count; i++)
                {
                    Match instructionLineMatch = match[i];

                    int nCrates = int.Parse(instructionLineMatch.Groups["nCrates"].Value);
                    int from = int.Parse(instructionLineMatch.Groups["from"].Value);
                    int to = int.Parse(instructionLineMatch.Groups["to"].Value);

                    instructions.Add(new Tuple<int, int, int>(nCrates, from, to));
                }
            }
            catch
            {
                throw new Exception("Invalid line in input.");
            }

            string part1 = SolvePart1(stacks.ToDictionary(entry => entry.Key, entry => new Stack<char>(entry.Value)), instructions);
            string part2 = SolvePart2(stacks.ToDictionary(entry => entry.Key, entry => new List<char>(entry.Value)), instructions);

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";

        }

        private static string SolvePart1(Dictionary<int, Stack<char>> stacks, List<Tuple<int, int, int>> instructions)
        {
            try
            {
                foreach (Tuple<int, int, int> instruction in instructions)
                {
                    for (int i = 0; i < instruction.Item1; i++)
                    {
                        stacks[instruction.Item3].Push(stacks[instruction.Item2].Pop());
                    }
                }
                string result = "";
                foreach (IEnumerable<char> crates in stacks.Values)
                {
                    result += crates.First();
                }
                return result;
            }
            catch
            {
                throw new Exception("Invalid line in input.");
            }
        }

        private static string SolvePart2(Dictionary<int, List<char>> stacks, List<Tuple<int, int, int>> instructions)
        {
            try
            {
                foreach (Tuple<int, int, int> instruction in instructions)
                {
                    List<char> fromStack = stacks[instruction.Item2];
                    List<char> toStack = stacks[instruction.Item3];

                    toStack.AddRange(fromStack.TakeLast(instruction.Item1));
                    fromStack.RemoveRange(fromStack.Count - instruction.Item1, instruction.Item1);
                }
                string result = "";
                foreach (IEnumerable<char> crates in stacks.Values)
                {
                    result += crates.Last();
                }
                return result;
            }
            catch
            {
                throw new Exception("Invalid line in input.");
            }
        }

        [GeneratedRegex(InstructionPattern, RegexOptions.Compiled)]
        private static partial Regex InputRegex();
    }
}
