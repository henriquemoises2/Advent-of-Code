namespace AdventOfCode.Helpers
{
    internal static partial class SetsGenerator<T>
    {

        /// <summary>
        /// Generate all the subsets of size subsetSize from the valuesList list. For example, for the set 1,2,3,4 the generated subsets
        /// with the subsetSize of 2 are [1,2][1,3][1,4][2,3][2,4][3,4].
        /// </summary>
        /// <param name="subsetSize"></param>
        /// <param name="valuesList"></param>
        /// <returns>A list with all subsets of size subsetSize of the valuesList set.</returns>
        internal static IEnumerable<IEnumerable<T>> GenerateSets(int subsetSize, List<T> valuesList)
        {
            List<List<T>> results = [];
            List<Tuple<List<T>, List<T>>> tempResults = [];

            if (subsetSize == 0)
            {
                return results;
            }

            for (int i = 0; i < valuesList.Count; i++)
            {
                tempResults.Add(Tuple.Create(new List<T> { valuesList[i] }, valuesList.Skip(i + 1).ToList()));
            }

            int iteration = 1;
            while (iteration < subsetSize)
            {
                List<Tuple<List<T>, List<T>>> iterationResults = [];
                foreach (var elem in tempResults)
                {
                    if (elem.Item2.Count != 0)
                    {
                        for (int i = 0; i < elem.Item2.Count; i++)
                        {
                            List<T> newList = [.. elem.Item1];
                            List<T> newRemainderList = [.. elem.Item2];

                            newList.Add(elem.Item2[i]);
                            newRemainderList.RemoveRange(0, i + 1);

                            var newTuple = Tuple.Create(newList, newRemainderList);

                            iterationResults.Add(newTuple);
                        }
                    }
                }
                iteration++;
                tempResults = iterationResults;
            }

            results = [.. tempResults.Select(r => r.Item1)];
            return results;
        }

        internal static IEnumerable<IEnumerable<T>> GenerateAllSets(int minSubsetSize, int maxSubsetSize, IEnumerable<T> valuesList)
        {
            List<IEnumerable<T>> sets = [];
            for (int i = minSubsetSize; i <= maxSubsetSize; i++)
            {
                sets.AddRange(SetsGenerator<T>.GenerateSets(i, [.. valuesList]));
            }
            return sets;
        }

        internal static IEnumerable<IEnumerable<int>> GenerateAllIntSetsWithLimit(int maxSubsetSize, IEnumerable<int> valuesList, int limit = 0)
        {
            List<IEnumerable<int>> sets = [];
            for (int i = 1; i <= maxSubsetSize; i++)
            {
                if (limit > 0 && valuesList.OrderBy(val => val).Take(i).Sum() > limit)
                {
                    return sets;
                }
                else if (limit > 0 && valuesList.OrderByDescending(val => val).Take(i).Sum() < limit)
                {
                    continue;
                }
                else
                {
                    sets.AddRange(SetsGenerator<int>.GenerateSets(i, [.. valuesList]));
                }

            }
            return sets;
        }

        internal static IEnumerable<IEnumerable<int>> GenerateIntSetsWithLimit(int subsetSize, IEnumerable<int> valuesList, int limit = 0)
        {
            List<IEnumerable<int>> sets = [.. SetsGenerator<int>.GenerateSets(subsetSize, [.. valuesList])];
            return sets.Where(set => set.Sum() <= limit);
        }

        /// <summary>
        /// Generate all permutations of valuesList with size combinationSize and repetitions, i.e. sets of elements where the order matters
        /// </summary>
        /// <param name="combinationSize"></param>
        /// <param name="valuesList"></param>
        /// <returns></returns>
        internal static IEnumerable<List<T>> GeneratePermutationsWithRepetition(int combinationSize, IEnumerable<T> valuesList)
        {
            Dictionary<string, T> codifiedValues = [];
            List<T> inputValues = [.. valuesList];
            for (int i = 1; i <= inputValues.Count; i++)
            {
                codifiedValues.Add(i.ToString(), inputValues.ElementAt(i - 1));
            }

            IEnumerable<String> combinations = PermutationsWithRepetitionRecursive(combinationSize, (IEnumerable<string>)codifiedValues.Keys.Select(k => k.ToString()));
            List<List<T>> results = [];

            foreach (string combination in combinations)
            {
                List<T> decodifiedResults = [];
                foreach (char code in combination)
                {
                    decodifiedResults.Add(codifiedValues[code.ToString()]);
                }
                results.Add(decodifiedResults);
            }
            return results;
        }

        /// <summary>
        /// Generate all permutations of valuesList with size combinationSize without repetitions, i.e. sets of elements where the order matters
        /// </summary>
        /// <param name="combinationSize"></param>
        /// <param name="valuesList"></param>
        /// <returns></returns>
        internal static IEnumerable<IEnumerable<T>> GeneratePermutationsWithoutRepetition(int combinationSize, IEnumerable<T> valuesList)
        {
            Dictionary<string, T> codifiedValues = [];
            List<T> inputValues = [.. valuesList];
            for (int i = 1; i <= inputValues.Count; i++)
            {
                codifiedValues.Add(((char)(64 + i)).ToString(), inputValues.ElementAt(i - 1));
            }

            IEnumerable<String> combinations = PermutationsWithoutRepetitionRecursive(combinationSize, (IEnumerable<string>)codifiedValues.Keys.Select(k => k.ToString()));
            List<List<T>> results = [];

            foreach (string combination in combinations)
            {
                List<T> decodifiedResults = [];
                foreach (char code in combination)
                {
                    decodifiedResults.Add(codifiedValues[code.ToString()]);
                }
                results.Add(decodifiedResults);
            }
            return results;
        }

        /// <summary>
        /// Generate all combinations of valuesList with size combinationSize, i.e. sets of elements where the order does not matter
        /// </summary>
        /// <param name="combinationSize"></param>
        /// <param name="valuesList"></param>
        /// <returns></returns>
        internal static List<List<T>> GenerateCombinationsWithRepetition(int combinationSize, IEnumerable<T> valuesList)
        {
            // Allocate memory
            int[] chosen = new int[combinationSize + 1];

            int n = valuesList.Count();
            List<List<T>> totalResults = [];

            // Call the recursive function
            CombinationsWithRepetitionRecursive(chosen, [.. valuesList], 0, combinationSize, 0, n - 1, totalResults);

            return totalResults;
        }


        #region Private Methods

        private static IEnumerable<string> PermutationsWithRepetitionRecursive(int length, IEnumerable<string> input)
        {
            if (length <= 0)
                yield return "";
            else
            {
                foreach (var i in input)
                    foreach (var c in PermutationsWithRepetitionRecursive(length - 1, input))
                        yield return i + c;
            }
        }

        private static IEnumerable<string> PermutationsWithoutRepetitionRecursive(int length, IEnumerable<string> input)
        {
            if (length <= 0)
                yield return "";
            else
            {
                foreach (var i in input)
                    foreach (var c in PermutationsWithoutRepetitionRecursive(length - 1, input.Except([i])))
                        yield return i + c;
            }
        }

        private static void CombinationsWithRepetitionRecursive(int[] chosen, T[] valuesList, int index, int combinationSize, int start, int end, List<List<T>> totalResults)
        {
            // Since index has become combinationSize, current combination is ready to be returned
            if (index == combinationSize)
            {
                List<T> result = [];
                for (int i = 0; i < combinationSize; i++)
                {
                    result.Add(valuesList[chosen[i]]);
                }
                totalResults.Add(result);
                return;
            }

            // One by one choose all elements (without considering if the element is already chosen or not) and recur
            for (int i = start; i <= end; i++)
            {
                chosen[index] = i;
                CombinationsWithRepetitionRecursive(chosen, valuesList, index + 1, combinationSize, i, end, totalResults);
            }
            return;
        }

        #endregion
    }
}
