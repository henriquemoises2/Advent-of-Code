namespace AdventOfCode.Helpers
{
    internal static class SetsGenerator
    {

        /// <summary>
        /// Generate all the subsets of size subsetSize from the valuesList list.
        /// </summary>
        /// <param name="subsetSize"></param>
        /// <param name="valuesList"></param>
        /// <returns>A list with all subsets of size subsetSize of the valuesList set.</returns>
        internal static IEnumerable<IEnumerable<int>> GenerateSets(int subsetSize, List<int> valuesList)
        {
            List<List<int>> results = new List<List<int>>();
            List<Tuple<List<int>, List<int>>> tempResults = new List<Tuple<List<int>, List<int>>>();

            for (int i = 0; i < valuesList.Count; i++)
            {
                tempResults.Add(Tuple.Create(new List<int> { valuesList[i] }, valuesList.Skip(i + 1).ToList()));
            }

            int iteration = 1;
            while (iteration < subsetSize)
            {
                List<Tuple<List<int>, List<int>>> iterationResults = new List<Tuple<List<int>, List<int>>>();
                foreach (var elem in tempResults)
                {
                    if (elem.Item2.Any())
                    {
                        for (int i = 0; i < elem.Item2.Count(); i++)
                        {
                            List<int> newList = new List<int>(elem.Item1);
                            List<int> newRemainderList = new List<int>(elem.Item2);

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

        internal static IEnumerable<IEnumerable<int>> GenerateAllSets(int maxSubsetSize, List<int> valuesList)
        {
            List<IEnumerable<int>> sets = new List<IEnumerable<int>>();
            for(int i = 1; i <= maxSubsetSize; i++)
            {
                sets.AddRange(GenerateSets(i, valuesList));
            }
            return sets;
        }
    }
}
