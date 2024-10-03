namespace AdventOfCode.Code
{
    public class Problem_2022_01 : Problem
    {
        public Problem_2022_01() : base()
        {
        }

        public override string Solve()
        {
            List<int> elfsList = [];

            int totalCalories = 0;

            try
            {
                foreach (string line in InputLines)
                {
                    // New elf inventory
                    if (string.IsNullOrEmpty(line))
                    {
                        elfsList.Add(totalCalories);
                        totalCalories = 0;
                    }
                    else
                    {
                        totalCalories += int.Parse(line);
                    }
                }
            }
            catch
            {
                throw new Exception("Invalid line in input.");
            }

            string part1 = SolvePart1(elfsList);
            string part2 = SolvePart2(elfsList);

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";

        }

        private static string SolvePart1(List<int> elfsList)
        {
            return elfsList.OrderByDescending(elf => elf).FirstOrDefault().ToString();
        }

        private static string SolvePart2(List<int> elfsList)
        {
            return elfsList.OrderByDescending(elf => elf).Take(3).Sum().ToString();
        }

    }
}
