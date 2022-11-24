using AdventOfCode.Helpers;

namespace AdventOfCode.Code
{
    internal class Problem_2015_17 : Problem
    {
        

        internal Problem_2015_17() : base()
        { }

        internal override string Solve()
        {
            List<int> ContainerSizes = new List<int>();
            try
            {
                ContainerSizes.AddRange(InputLines.Select(line => int.Parse(line)));
            }
            catch
            {
                throw new Exception("Invalid line in input.");
            }

            var sets = SetsGenerator.GenerateAllSets(InputLines.Count(), ContainerSizes);

            string part1 = SolvePart1();
            string part2 = SolvePart2();

            return $"Part 1 solution: " + part1 + "\n"
                + "Part 2 solution: " + part2;
        }

        private string SolvePart1()
        {
            return "";
        }

        private string SolvePart2()
        {
            return "";
        }
    }
}