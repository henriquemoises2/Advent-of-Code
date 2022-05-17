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
            int numberOfCities = _distanceMatrix.GetLength(0);
            List<int> totalNodes = new List<int>();

            // K = 0
            for (int i = 0; i < numberOfCities; i++)
            {
                if (i != _startingNode)
                {
                    // Insert all nodes except the starting one into a list
                    totalNodes.Add(i);

                    // Compute the cost of going from starting position into current position
                    _costLookup.Add($"g({i},{{O}})", _distanceMatrix[_startingNode, i]);
                }
            }

            // If there are only two or less cities, then the value was already calculated and it is the min value in the lookup
            if (numberOfCities <= 2)
            {
                return _costLookup.Values.Min();
            }

            List<int> subsetsResult = new List<int>();
            for (int subsetSize = 1; subsetSize < numberOfCities - 1; subsetSize++)
            {

                IEnumerable<IEnumerable<int>> test = GenerateSubsets(4, new List<int> { 1, 2, 3, 4 });

                // Build subsets of size subsetSize
                //foreach (int node in totalNodes)
                //{
                //    IEnumerable<IEnumerable<int>> subsets = GenerateSubsets(subsetSize, totalNodes.Where(n => n != node));
                //    subsetsResult.Add(ComputeDistanceValue(node, totalNodes.Where(n => n != node).ToList()));
                //}

            }
            return subsetsResult.Min();
        }

        private int ComputeDistanceValue(int x, List<int> set)
        {
            int minResult = int.MaxValue;
            int result = 0;
            if (set.Count == 1)
            {
                // Example g(3,{2}) = c32 + g(2, {O})
                result = _distanceMatrix[x, set[0]] + _costLookup[$"g({x},{{O}})"];
                _costLookup.Add($"g({x},{{{set[0]}}})", result);
                minResult = result;
            }
            else
            {
                foreach (int value in set)
                {
                    result = _distanceMatrix[x, value] + _costLookup[$"g({x},{{{value}}})"];
                    if (value < minResult)
                    {
                        minResult = value;
                    }
                }
                _costLookup.Add($"g({x},{{{string.Join(",", set)}}})", result);
            }
            return minResult;
        }

        private IEnumerable<IEnumerable<int>> GenerateSubsets(int subsetSize, List<int> nodesList)
        {
            List<IEnumerable<int>> results = new List<IEnumerable<int>>();

            List<int> set = new List<int>();
            int idx = 0;
            int removed = 0;

            List<int> auxNodesList = new List<int>(nodesList);

            if (subsetSize == 1)
            {
                foreach(var elem in nodesList)
                {
                    results.Add(new List<int> { elem });
                }
                return results;
            }

            while (auxNodesList.Count >= subsetSize)
            {      
                if(idx + subsetSize > auxNodesList.Count)
                {
                    auxNodesList.RemoveAt(0);
                    if(auxNodesList.Count < subsetSize)
                    {
                        break;
                    }
                    removed++;
                    idx = removed;
                }
                set = new List<int>();
                int currentNode = auxNodesList[0];
                set.Add(currentNode);

                int internalIterator = 1;

                while (internalIterator < subsetSize && internalIterator < auxNodesList.Count)
                {
                    set.Add(nodesList.Skip(idx).ToList()[internalIterator]);
                    internalIterator++;
                }
                idx++;

                results.Add(set);
            }
            return results.Distinct();
        }


    }
}
