﻿namespace AdventOfCode.Code
{
    internal class Problem_2022_01 : Problem
    {
        internal Problem_2022_01() : base()
        {
        }

        internal override string Solve()
        {
            List<int> elfsList = new List<int>();

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

            return $"Part 1 solution: " + part1 + "\n"
                + "Part 2 solution: " + part2;
        }

        private string SolvePart1(List<int> elfsList)
        {
            return elfsList.OrderByDescending(elf => elf).FirstOrDefault().ToString();
        }

        private string SolvePart2(List<int> elfsList)
        {
            return elfsList.OrderByDescending(elf => elf).Take(3).Sum().ToString();
        }

    }
}
