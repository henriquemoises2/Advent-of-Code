using AdventOfCode.Helpers;

namespace AdventOfCode.Algorithms
{
    internal class HeldKarpAlgorithm
    {
        private readonly int[,] _distanceMatrix;
        private readonly int? _startingNode;
        private readonly bool _returnToOrigin;

        private readonly Dictionary<string, int> _costLookup;

        internal HeldKarpAlgorithm(int[,] distanceMatrix, int? startingNode, bool returnToOrigin = true)
        {
            _distanceMatrix = distanceMatrix;
            _startingNode = startingNode;
            _returnToOrigin = returnToOrigin;
            _costLookup = new Dictionary<string, int>();
        }

        internal int GetShortestPathCost()
        {
            List<int> totalNodes = new();
            List<int> subsetsResult = new();
            List<int> currentSubsets = new();

            int numberOfNodes = _distanceMatrix.GetLength(0) - 1;
            totalNodes = ComputePathCostsToOrigin(numberOfNodes);

            // If we have a starting node the algorithm should not run the last iteration
            int numberOfIterations = numberOfNodes + (_startingNode.HasValue ? 0 : 1);

            for (int subsetSize = 1; subsetSize < numberOfIterations; subsetSize++)
            {
                foreach (var node in totalNodes)
                {
                    // Generate subsets of size subsetSize where the current node does not appear
                    IEnumerable<IEnumerable<int>> subsets = SetsGenerator<int>.GenerateSets(subsetSize, totalNodes.Where(n => n != node).ToList());
                    foreach (var subset in subsets)
                    {
                        // Compute value of going to node passing through subset , i.e. g(node, {subset})
                        currentSubsets.Add(ComputeShortestCostValue(node, subset.ToList()));
                    }
                }
                subsetsResult = new List<int>(currentSubsets);
                currentSubsets.Clear();
            }
            // Also add the cost of going back to the origin
            if (_returnToOrigin && _startingNode.HasValue)
            {
                return ComputeShortestCostValue(_startingNode.Value, totalNodes);
            }
            return subsetsResult.Min();
        }

        private int ComputeShortestCostValue(int x, List<int> set)
        {
            int minResult = int.MaxValue;
            int result = 0;

            // cost(xi, S) = min{cost(xj,S\{xi}) + Dji}
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
                if (!_costLookup.ContainsKey($"g({x},{{{set[0]}}})"))
                {
                    _costLookup.Add($"g({x},{{{set[0]}}})", result);
                }
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
                if (!_costLookup.ContainsKey($"g({x},{{{string.Join(",", set)}}})"))
                {
                    _costLookup.Add($"g({x},{{{string.Join(",", set)}}})", minResult);
                }

            }
            return minResult;
        }

        internal int GetLongestPathCost()
        {
            List<int> totalNodes = new();
            List<int> subsetsResult = new();
            List<int> currentSubsets = new();

            int numberOfNodes = _distanceMatrix.GetLength(0) - 1;
            totalNodes = ComputePathCostsToOrigin(numberOfNodes);

            // If we have a starting node the algorithm should not run the last iteration
            int numberOfIterations = numberOfNodes + (_startingNode.HasValue ? 0 : 1);

            for (int subsetSize = 1; subsetSize < numberOfIterations; subsetSize++)
            {
                foreach (var node in totalNodes)
                {
                    // Generate subsets of size subsetSize where the current node does not appear
                    IEnumerable<IEnumerable<int>> subsets = SetsGenerator<int>.GenerateSets(subsetSize, totalNodes.Where(n => n != node).ToList());
                    foreach (var subset in subsets)
                    {
                        // Compute value of going to node passing through subset , i.e. g(node, {subset})
                        currentSubsets.Add(ComputeLongestCostValue(node, subset.ToList()));
                    }
                }
                subsetsResult = new List<int>(currentSubsets);
                currentSubsets.Clear();
            }
            // Also add the cost of going back to the origin
            if (_returnToOrigin && _startingNode.HasValue)
            {
                return ComputeLongestCostValue(_startingNode.Value, totalNodes);
            }
            return subsetsResult.Max();
        }

        private int ComputeLongestCostValue(int x, List<int> set)
        {
            int maxResult = int.MinValue;
            int result = 0;

            // cost(xi, S) = min{cost(xj,S\{xi}) + Dji}

            if (set.Count == 1)
            {
                var lookupValue = _costLookup[$"g({set[0]},{{O}})"];

                // Handles infinity
                if (_distanceMatrix[set[0] - 1, x - 1] == int.MinValue || lookupValue == int.MinValue)
                {
                    result = int.MinValue;
                }
                else
                {
                    result = _distanceMatrix[set[0] - 1, x - 1] + lookupValue;
                }
                if (!_costLookup.ContainsKey($"g({x},{{{set[0]}}})"))
                {
                    _costLookup.Add($"g({x},{{{set[0]}}})", result);
                }
                maxResult = result;
            }
            else
            {
                foreach (int value in set)
                {
                    var lookupValue = _costLookup[$"g({value},{{{string.Join(",", set.Where(e => e != value))}}})"];

                    // Handles Infinity
                    if (_distanceMatrix[value - 1, x - 1] == int.MinValue || lookupValue == int.MinValue)
                    {
                        result = int.MinValue;
                    }
                    else
                    {
                        result = _distanceMatrix[value - 1, x - 1] + lookupValue;

                    }
                    if (result > maxResult)
                    {
                        maxResult = result;
                    }
                }
                if (!_costLookup.ContainsKey($"g({x},{{{string.Join(",", set)}}})"))
                {
                    _costLookup.Add($"g({x},{{{string.Join(",", set)}}})", maxResult);
                }

            }
            return maxResult;
        }


        private List<int> ComputePathCostsToOrigin(int numberOfNodes)
        {
            List<int> totalNodes = new();

            for (int i = 1; i <= numberOfNodes + 1; i++)
            {
                if (!_startingNode.HasValue)
                {
                    // Insert all nodes except the starting one into a list
                    totalNodes.Add(i);

                    // Compute the cost of going from starting position into current position
                    _costLookup.Add($"g({i},{{O}})", 0);
                }
                else if (i != _startingNode)
                {
                    // Insert all nodes except the starting one into a list
                    totalNodes.Add(i);

                    // Compute the cost of going from starting position into current position
                    _costLookup.Add($"g({i},{{O}})", _distanceMatrix[_startingNode.Value - 1, i - 1]);
                }
            }

            return totalNodes;
        }
    }
}
