﻿using AdventOfCode.Helpers;

namespace AdventOfCode.Code
{
    public class Problem_2015_17 : Problem
    {
        private const int TotalEggnogLitres = 150;

        public Problem_2015_17() : base()
        { }

        public override string Solve()
        {
            List<int> ContainerSizes = [];
            try
            {
                ContainerSizes.AddRange(InputLines.Select(line => int.Parse(line)));
            }
            catch
            {
                throw new Exception("Invalid line in input.");
            }

            var allContainerPossibilities = SetsGenerator<int>.GenerateAllIntSetsWithLimit(InputLines.Count(), ContainerSizes, TotalEggnogLitres);

            string part1 = SolvePart1(allContainerPossibilities);
            string part2 = SolvePart2(allContainerPossibilities);

            return string.Format(SolutionFormat, part1, part2);

        }

        private static string SolvePart1(IEnumerable<IEnumerable<int>> allContainerPossibilities)
        {
            // Check which container sets can be completely filled with TotalEggnogLitres
            IEnumerable<IEnumerable<int>> realContainerPossibilities = allContainerPossibilities.Where(set => set.Sum() == TotalEggnogLitres);
            return realContainerPossibilities.Count().ToString();
        }

        private static string SolvePart2(IEnumerable<IEnumerable<int>> allContainerPossibilities)
        {
            // Check which container sets can be completely filled with TotalEggnogLitres
            IEnumerable<IEnumerable<int>> realContainerPossibilities = allContainerPossibilities.Where(set => set.Sum() == TotalEggnogLitres);
            // Group all possibilities by the set size, i.e. how many containers are used to fill TotalEggnogLitres 
            IEnumerable<IGrouping<int, IEnumerable<int>>> groupedContainerPossibilities = realContainerPossibilities.GroupBy(set => set.Count());
            // Select first group element, i.e. the grouping containing the set size and the associated sets. Then count all sets.
            return groupedContainerPossibilities.First().Count().ToString();
        }
    }
}