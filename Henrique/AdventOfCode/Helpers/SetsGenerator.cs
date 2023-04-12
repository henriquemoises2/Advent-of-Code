using AdventOfCode._2015_7;

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
            List<List<T>> results = new List<List<T>>();
            List<Tuple<List<T>, List<T>>> tempResults = new List<Tuple<List<T>, List<T>>>();

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
                List<Tuple<List<T>, List<T>>> iterationResults = new List<Tuple<List<T>, List<T>>>();
                foreach (var elem in tempResults)
                {
                    if (elem.Item2.Any())
                    {
                        for (int i = 0; i < elem.Item2.Count(); i++)
                        {
                            List<T> newList = new List<T>(elem.Item1);
                            List<T> newRemainderList = new List<T>(elem.Item2);

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

            results = tempResults.Select(r => r.Item1).ToList();
            return results;
        }

        internal static IEnumerable<IEnumerable<T>> GenerateAllSets(int minSubsetSize, int maxSubsetSize, IEnumerable<T> valuesList)
        {
            List<IEnumerable<T>> sets = new List<IEnumerable<T>>();
            for (int i = minSubsetSize; i <= maxSubsetSize; i++)
            {
                sets.AddRange(SetsGenerator<T>.GenerateSets(i, valuesList.ToList()));
            }
            return sets;
        }

        internal static IEnumerable<IEnumerable<int>> GenerateAllIntSetsWithLimit(int maxSubsetSize, IEnumerable<int> valuesList, int limit = 0)
        {
            List<IEnumerable<int>> sets = new List<IEnumerable<int>>();
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
                    sets.AddRange(SetsGenerator<int>.GenerateSets(i, valuesList.ToList()));
                }

            }
            return sets;
        }

        internal static IEnumerable<List<T>> GeneratePermutationsWithRepetition(int permutationSize, IEnumerable<T> valuesList)
        {
            List<List<T>> results = new List<List<T>>();
            if (permutationSize == 0)
            {
                return results;
            }

            foreach (T value in valuesList)
            {
                results.Add(new List<T>() { value });
            }
            permutationSize--;

            List<List<T>> tempResults = new List<List<T>>(results);
            while (permutationSize > 0)
            {
                results.Clear();
                foreach (List<T> result in tempResults)
                {
                    foreach (T value in valuesList)
                    {
                        List<T> newResult = new List<T>(result);
                        newResult.Add(value);
                        results.Add(newResult);
                    }
                }
                tempResults = new List<List<T>>(results);
                permutationSize--;
            }
            return results;
        }

        internal static IEnumerable<List<T>> GenerateCombinationsWithRepetition(int combinationSize, IEnumerable<T> valuesList)
        {
            Dictionary<string, T> codifiedValues = new Dictionary<string, T>();
            List<T> inputValues = valuesList.ToList();
            for (int i = 1; i <= inputValues.Count(); i++)
            {
                codifiedValues.Add(i.ToString(), inputValues.ElementAt(i - 1));
            }

            IEnumerable<String> combinations = CombinationsWithRepetition(combinationSize, (IEnumerable<string>)codifiedValues.Keys.Select(k => k.ToString()));
            List<List<T>> results = new List<List<T>>();

            foreach (string combination in combinations)
            {
                List<T> decodifiedResults = new List<T>();
                foreach(char code in combination)
                {
                    decodifiedResults.Add(codifiedValues[code.ToString()]);
                }
                results.Add(decodifiedResults);
            }
            return results;

        }


        private static IEnumerable<String> CombinationsWithRepetition(int length, IEnumerable<string> input)
        {
            if (length <= 0)
                yield return "";
            else
            {
                foreach (var i in input)
                    foreach (var c in CombinationsWithRepetition(length - 1, input))
                        yield return i + c;
            }
        }
    }
}
