using AdventOfCode.Helpers;

namespace AdventOfCode.Code
{
    internal class Problem_2015_24 : Problem
    {
       
        internal Problem_2015_24() : base()
        {
        }

        internal override string Solve()
        {
            IEnumerable<int> packagesWeights;
            try
            {
                packagesWeights = InputLines.Select(line => int.Parse(line));
            }
            catch
            {
                throw new Exception("Invalid line in input.");
            }

            string part1 = SolvePart1(packagesWeights);
            string part2 = SolvePart2();

            return $"Part 1 solution: " + part1 + "\n"
                + "Part 2 solution: " + part2;
        }

        private string SolvePart1(IEnumerable<int> packagesWeights)
        {
            int totalCargoWeight = packagesWeights.Sum();
            int groupWeight = totalCargoWeight / 3;

            var sets = SetsGenerator<int>.GenerateAllIntSetsWithLimit(8, packagesWeights, groupWeight);
            sets = sets.Where(set => set.Sum() == groupWeight);

            // Order by number of elements
            var orderedSetsByNElements = sets.GroupBy(set => set.Count()).First();

            // Select group with fewer elements and with smallets quantum entanglement
            var fewerElementsGroupWithSmallestQE = orderedSetsByNElements.OrderBy(set => ComputeQuantumEntanglement(set)).First();

            return ComputeQuantumEntanglement(fewerElementsGroupWithSmallestQE).ToString(); 
        }

        private string SolvePart2()
        {
            return "";
        }

        private double ComputeQuantumEntanglement(IEnumerable<int> itemsWeights)
        {
            double quantumEntanglement = 1;
            foreach(int weight in itemsWeights)
            {
                quantumEntanglement = quantumEntanglement * weight;
            }
            return quantumEntanglement;
        }
    }
}