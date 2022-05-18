namespace AdventOfCode.Algorithms
{
    internal class HeldKarpAlgorithm
    {
        private int[,] _distanceMatrix;
        private int _startingNode;

        private Dictionary<string, int> _costLookup;

        internal HeldKarpAlgorithm(int[,] distanceMatrix, int startingNode)
        {
            _distanceMatrix = distanceMatrix;
            _startingNode = startingNode;
            _costLookup = new Dictionary<string, int>();
        }

        internal int GetShortestPathLenght()
        {
            int numberOfNodes = _distanceMatrix.GetLength(0) - 1;
            List<int> totalNodes = new List<int>();

            // K = 0
            for (int i = 1; i <= numberOfNodes + 1; i++)
            {
                if (i != _startingNode)
                {
                    // Insert all nodes except the starting one into a list
                    totalNodes.Add(i);

                    // Compute the cost of going from starting position into current position
                    _costLookup.Add($"g({i},{{O}})", _distanceMatrix[_startingNode - 1, i - 1]);
                }
            }

            // If there are only two or less cities, then the value was already calculated and it is the min value in the lookup
            if (numberOfNodes <= 1)
            {
                return _costLookup.Values.Min();
            }

            List<int> subsetsResult = new List<int>();
            List<int> currentSubsets = new List<int>();

            for (int subsetSize = 1; subsetSize < numberOfNodes; subsetSize++)
            {
                foreach (var node in totalNodes)
                {
                    IEnumerable<IEnumerable<int>> subsets = GenerateSubsets(subsetSize, totalNodes.Where(n => n != node).ToList());
                    foreach (var subset in subsets)
                    {
                        currentSubsets.Add(ComputeDistanceValue(node, subset.ToList()));
                    }
                }
                subsetsResult = new List<int>(currentSubsets);
                currentSubsets.Clear();
            }
            return ComputeDistanceValue(_startingNode, totalNodes);
        }

        private int ComputeDistanceValue(int x, List<int> set)
        {
            int minResult = int.MaxValue;
            int result = 0;
            if (set.Count == 1)
            {
                var lookupValue = _costLookup[$"g({set[0]},{{O}})"];

                // Handles infinity
                if (_distanceMatrix[set[0] - 1, x - 1] == int.MaxValue || lookupValue == int.MaxValue)
                {
                    result = int.MaxValue;
                }
                else
                {
                    result = _distanceMatrix[set[0] - 1, x - 1] + lookupValue;
                }

                _costLookup.Add($"g({x},{{{set[0]}}})", result);
                minResult = result;
            }
            else
            {
                foreach (int value in set)
                {
                    var lookupValue = _costLookup[$"g({value},{{{string.Join(",", set.Where(e => e != value))}}})"];

                    // Handles Infinity
                    if (_distanceMatrix[value - 1, x - 1] == int.MaxValue || lookupValue == int.MaxValue)
                    {
                        result = int.MaxValue;
                    }
                    else
                    {
                        result = _distanceMatrix[value - 1, x - 1] + lookupValue;

                    }
                    if (result < minResult)
                    {
                        minResult = result;
                    }
                }
                _costLookup.Add($"g({x},{{{string.Join(",", set)}}})", result);
            }
            return minResult;
        }


        /// <summary>
        /// Generate all the subsets of size subsetSize from the valuesList list.
        /// </summary>
        /// <param name="subsetSize"></param>
        /// <param name="valuesList"></param>
        /// <returns>A list with all subsets of size subsetSize of the valuesList set.</returns>
        private IEnumerable<IEnumerable<int>> GenerateSubsets(int subsetSize, List<int> valuesList)
        {

            List<IEnumerable<int>> results = new List<IEnumerable<int>>();
            if (subsetSize > valuesList.Count)
            {
                return results;
            }

            List<int> set = new List<int>();
            int idx = 0;
            int removed = 0;

            List<int> auxValuesList = new List<int>(valuesList);

            // Special case to when subset length is 1
            if (subsetSize == 1)
            {
                foreach (var elem in valuesList)
                {
                    results.Add(new List<int> { elem });
                }
                return results;
            }

            while (auxValuesList.Count >= subsetSize)
            {
                if (idx + subsetSize > auxValuesList.Count)
                {
                    auxValuesList.RemoveAt(0);
                    if (auxValuesList.Count < subsetSize)
                    {
                        break;
                    }
                    removed++;
                    idx = removed;
                }
                set = new List<int>();
                int currentNode = auxValuesList[0];
                set.Add(currentNode);

                int internalIterator = 1;

                while (internalIterator < subsetSize && internalIterator < auxValuesList.Count)
                {
                    set.Add(valuesList.Skip(idx).ToList()[internalIterator]);
                    internalIterator++;
                }
                idx++;

                results.Add(set);
            }
            return results.Distinct();
        }


    }
}
