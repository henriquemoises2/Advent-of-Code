namespace AdventOfCode.Code
{
    public class Problem_2022_05 : Problem
    {
        public Problem_2022_05() : base()
        {
        }

        public override string Solve()
        {
            try
            {
                Dictionary<int, Stack<char>> columns = new Dictionary<int, Stack<char>>();
                string[] inputLinesAsArray = InputLines.ToArray();
                int columnNumberIndex = InputLines.ToList().FindIndex(line => string.IsNullOrWhiteSpace(line)) - 1;
                int maxColumnHeight = columnNumberIndex;
                string columnNumbers = inputLinesAsArray[columnNumberIndex];

                for(int i = 0; i < columnNumbers.Length; i++) 
                {
                    if (columnNumbers[i] != ' ')
                    {
                        int columnNumber = columnNumbers[i] - '0';
                        Stack<char> crates = new Stack<char>();
                        for(int j = maxColumnHeight - 1; j >= 0; j--)
                        {
                            if (inputLinesAsArray[j][i] != ' ' && inputLinesAsArray[j][i] != '[' && inputLinesAsArray[j][i] != ']')
                            {
                                crates.Push(inputLinesAsArray[j][i]);
                            }
                        }
                        columns.Add(columnNumber, crates);
                    }
                }
            }
            catch
            {
                throw new Exception("Invalid line in input.");
            }

            string part1 = SolvePart1();
            string part2 = SolvePart2();

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";

        }

        private string SolvePart1()
        {
            try
            {
                return "";
            }
            catch
            {
                throw new Exception("Invalid line in input.");
            }
        }

        private string SolvePart2()
        {
            try
            {
                return "";
            }
            catch
            {
                throw new Exception("Invalid line in input.");
            }
        }

    }
}
