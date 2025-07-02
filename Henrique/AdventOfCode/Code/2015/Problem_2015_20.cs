using AdventOfCode.Helpers;

namespace AdventOfCode.Code
{
    public class Problem_2015_20 : Problem
    {
        private const int MaxHousesNumber = 1000000;
        private const int MaxVisitedHouses = 50;


        public Problem_2015_20() : base()
        {
        }

        public override string Solve()
        {
            int minimumGifts;
            try
            {
                minimumGifts = int.Parse(InputLastLine);
            }
            catch
            {
                throw new Exception("Invalid line in input.");
            }

            Tuple<int, int> combinedSolutions = SolveBothParts(minimumGifts);

            string part1 = SolvePart1(combinedSolutions);
            string part2 = SolvePart2(combinedSolutions);

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";

        }

        private static Tuple<int, int> SolveBothParts(int minimumGifts)
        {
            Dictionary<int, int> numberHousesVisitedByEachElf = [];
            Tuple<int, int> combinedSolutions = new(0, 0);
            long totalGiftsFirstRun = 0, totalGiftsSecondRun = 0;
            for (int i = 1; i < MaxHousesNumber; i++)
            {
                IEnumerable<int> divisors = MathOperations.GetDivisors(i);

                if (combinedSolutions.Item1 == 0)
                {
                    totalGiftsFirstRun = MathOperations.SumOfMultiplications(divisors, 10);
                }

                if (combinedSolutions.Item2 == 0)
                {
                    List<int> filteredDivisors = [.. divisors];
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
                    totalGiftsSecondRun = MathOperations.SumOfMultiplications(filteredDivisors, 11);
                }

                if (totalGiftsFirstRun >= minimumGifts)
                {
                    if (combinedSolutions.Item1 == 0)
                    {
                        int item2 = combinedSolutions.Item2;
                        combinedSolutions = new Tuple<int, int>(i, item2);
                    }
                }

                if (totalGiftsSecondRun >= minimumGifts)
                {
                    if (combinedSolutions.Item2 == 0)
                    {
                        int item1 = combinedSolutions.Item1;
                        combinedSolutions = new Tuple<int, int>(item1, i);
                    }
                }

                if (combinedSolutions.Item1 != 0 && combinedSolutions.Item2 != 0)
                {
                    return combinedSolutions;
                }
            }

            throw new Exception("No solution found.");
        }

        private static string SolvePart1(Tuple<int, int> combinedResult)
        {
            return combinedResult.Item1.ToString();
        }

        private static string SolvePart2(Tuple<int, int> combinedResult)
        {
            return combinedResult.Item2.ToString();
        }
    }
}