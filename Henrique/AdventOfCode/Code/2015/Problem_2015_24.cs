using AdventOfCode.Helpers;

namespace AdventOfCode.Code
{
    public class Problem_2015_24 : Problem
    {

        public Problem_2015_24() : base()
        {
        }

        public override string Solve()
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

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";

        }

        private static string SolvePart1(IEnumerable<int> packagesWeights)
        {
            double? result = SolveProblem(3, packagesWeights);
            if (result != null)
            {
                return result.Value.ToString();
            }

            throw new Exception("No solution found");
        }

        private static string SolvePart2(IEnumerable<int> packagesWeights)
        {

            double? result = SolveProblem(4, packagesWeights);
            if (result != null)
            {
                return result.Value.ToString();
            }

            throw new Exception("No solution found");

        }

        private static double? SolveProblem(int nCargoCompartments, IEnumerable<int> packagesWeights)
        {
            int totalCargoWeight = packagesWeights.Sum();
            // The exact weight for each cargo compartment is given by the total weight divided by the number of cargo compartments
            int groupWeight = totalCargoWeight / nCargoCompartments;

            for (int i = 1; i < packagesWeights.Count() / nCargoCompartments; i++)
            {
                // Generates all possible sets of length i, with a top limit of groupWeight
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

        private static long ComputeQuantumEntanglement(IEnumerable<int> itemsWeights)
        {
            long quantumEntanglement = MathOperations.MultiplyElements(itemsWeights);
            return quantumEntanglement;
        }
    }
}