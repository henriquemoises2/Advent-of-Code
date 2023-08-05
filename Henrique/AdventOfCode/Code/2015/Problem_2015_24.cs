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
            string part2 = SolvePart2(packagesWeights);

            return $"Part 1 solution: " + part1 + "\n"
                + "Part 2 solution: " + part2;
        }

        private string SolvePart1(IEnumerable<int> packagesWeights)
        {
            double? result = SolveProblem(3, packagesWeights);
            if (result != null)
            {
                return result.Value.ToString();
            }

            throw new Exception("No solution found");
        }

        private string SolvePart2(IEnumerable<int> packagesWeights)
        {

            double? result = SolveProblem(4, packagesWeights);
            if(result != null)
            {
                return result.Value.ToString();
            }

            throw new Exception("No solution found");

        }

        private double? SolveProblem(int nGroups, IEnumerable<int> packagesWeights)
        {
            int totalCargoWeight = packagesWeights.Sum();
            int groupWeight = totalCargoWeight / nGroups;

            for (int i = 1; i < packagesWeights.Count() / nGroups; i++)
            {
                var sets = SetsGenerator<int>.GenerateIntSetsWithLimit(i, packagesWeights, groupWeight);
                sets = sets.Where(set => set.Sum() == groupWeight);

                if (!sets.Any())
                {
                    continue;
                }

                // Order by number of elements
                var orderedSetsByNElements = sets.GroupBy(set => set.Count()).First();

                // Select group with fewer elements and with smallets quantum entanglement
                var fewerElementsGroupWithSmallestQE = orderedSetsByNElements.OrderBy(set => ComputeQuantumEntanglement(set)).First();

                if (fewerElementsGroupWithSmallestQE != null && fewerElementsGroupWithSmallestQE.Any())
                {
                    return ComputeQuantumEntanglement(fewerElementsGroupWithSmallestQE);
                }
            }

            return null;
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