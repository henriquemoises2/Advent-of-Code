namespace AdventOfCode.Code
{
    public class Problem_2022_03 : Problem
    {
        public Problem_2022_03() : base()
        {
        }

        public override string Solve()
        {
            string part1 = SolvePart1(InputLines);
            string part2 = SolvePart2(InputLines);

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";

        }

        private static string SolvePart1(IEnumerable<string> InputLines)
        {
            try
            {
                int totalValue = 0;
                foreach (string line in InputLines)
                {
                    Dictionary<char, int> itemsCountTracker = new();
                    char? commonItem = null;
                    foreach (char character in line.Take(line.Length / 2))
                    {
                        if (!itemsCountTracker.TryAdd(character, 1))
                        {
                            itemsCountTracker[character]++;
                        }
                    }
                    foreach (char character in line.Skip(line.Length / 2))
                    {
                        if (itemsCountTracker.ContainsKey(character))
                        {
                            commonItem = character;
                            break;
                        }
                    }

                    if (commonItem != null)
                    {
                        totalValue += ComputeItemValue(commonItem.Value);
                    }
                }
                return totalValue.ToString();
            }
            catch
            {
                throw new Exception("Invalid line in input.");
            }
        }

        private static string SolvePart2(IEnumerable<string> InputLines)
        {

            try
            {
                int totalValue = 0;
                int elfNumber = 1;
                Dictionary<char, int> itemsCountTracker = new();
                char? commonItem = null;
                foreach (string line in InputLines)
                {

                    if (elfNumber > 3)
                    {
                        itemsCountTracker = new Dictionary<char, int>();
                        commonItem = null;
                        elfNumber = 1;
                    }

                    foreach (char character in line)
                    {

                        if (elfNumber == 1)
                        {
                            itemsCountTracker.TryAdd(character, 1);
                        }
                        else if (elfNumber == 2)
                        {
                            if (itemsCountTracker.ContainsKey(character))
                            {
                                itemsCountTracker[character] = 2;
                            }
                        }
                        else
                        {
                            if (itemsCountTracker.TryGetValue(character, out int value) && value == 2)
                            {
                                commonItem = character;
                                break;
                            }
                        }
                    }
                    if (commonItem != null)
                    {
                        totalValue += ComputeItemValue(commonItem.Value);
                    }

                    elfNumber++;

                }
                return totalValue.ToString();
            }
            catch
            {
                throw new Exception("Invalid line in input.");
            }
        }

        private static int ComputeItemValue(char item)
        {
            if (Char.IsLower(item))
            {
                return item - 96;
            }
            else
            {
                return item - 38;
            }
        }

    }
}
