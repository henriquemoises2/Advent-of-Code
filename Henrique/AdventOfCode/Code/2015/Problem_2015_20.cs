using AdventOfCode.Helpers;

namespace AdventOfCode.Code
{
    internal class Problem_2015_20 : Problem
    {
        private const int MaxHousesNumber = 1000000;
        private const int MaxVisitedHouses = 50;


        internal Problem_2015_20() : base()
        {
        }

        internal override string Solve()
        {
            int minimunGifts = 0;
            try
            {
                minimunGifts = int.Parse(InputLastLine);
            }
            catch
            {
                throw new Exception("Invalid line in input.");
            }

            IEnumerable<int> combinedSolutions = SolveBothParts(minimunGifts);

            string part1 = SolvePart1(combinedSolutions);
            string part2 = SolvePart2(combinedSolutions);

            return $"Part 1 solution: " + part1 + "\n"
                + "Part 2 solution: " + part2;
        }

        private IEnumerable<int> SolveBothParts(int minimumGifts)
        {
            Dictionary<int, int> numberHousesVisitedByEachElf = new Dictionary<int, int>();
            List<int> combinedSolutions = new List<int>();

            for (int i = 1; i < MaxHousesNumber; i++)
            {
                IEnumerable<int> divisors = MathOperations.GetDivisors(i);

                long totalGiftsFirstRun = MathOperations.SumOfMultiplications(divisors, 10);

                List<int> filteredDivisors = divisors.ToList();
                foreach (int divisor in divisors)
                {
                    if (!numberHousesVisitedByEachElf.TryAdd(divisor, 1))
                    {
                        if (numberHousesVisitedByEachElf[divisor] == MaxVisitedHouses)
                        {
                            filteredDivisors.Remove(divisor);
                        }
                        else
                        {
                            numberHousesVisitedByEachElf[divisor]++;
                        }
                    }
                }

                long totalGiftsSecondRun = MathOperations.SumOfMultiplications(filteredDivisors, 11);

                if (totalGiftsFirstRun >= minimumGifts)
                {
                    if (combinedSolutions.Count < 2)
                    {
                        combinedSolutions.Add(i);
                    }
                }

                if (totalGiftsSecondRun >= minimumGifts)
                {
                    if (combinedSolutions.Count < 2)
                    {
                        combinedSolutions.Add(i);
                    }
                }

                if (combinedSolutions.Count == 2)
                {
                    return combinedSolutions;
                }
            }


            throw new Exception("No solution found.");
        }

        private string SolvePart1(IEnumerable<int> combinedResult)
        {
            return combinedResult.First().ToString();
        }

        private string SolvePart2(IEnumerable<int> combinedResult)
        {
            return combinedResult.Last().ToString();

        }
    }
}