﻿namespace AdventOfCode.Helpers
{
    internal static partial class SetsGenerator<T>
    {

        /// <summary>
        /// Generate all the subsets of size subsetSize from the valuesList list.
        /// </summary>
        /// <param name="subsetSize"></param>
        /// <param name="valuesList"></param>
        /// <returns>A list with all subsets of size subsetSize of the valuesList set.</returns>
        internal static IEnumerable<IEnumerable<T>> GenerateSets(int subsetSize, List<T> valuesList)
        {
            List<List<T>> results = new List<List<T>>();
            List<Tuple<List<T>, List<T>>> tempResults = new List<Tuple<List<T>, List<T>>>();

            if(subsetSize == 0)
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
    }

    internal static partial class SetsGenerator
    {
        internal static IEnumerable<IEnumerable<int>> GenerateAllIntSets(int maxSubsetSize, IEnumerable<int> valuesList, int limit = 0)
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
    }
}
