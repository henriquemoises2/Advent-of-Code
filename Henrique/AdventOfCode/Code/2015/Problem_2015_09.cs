using AdventOfCode._2015_9;
using AdventOfCode.Algorithms;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    public partial class Problem_2015_09 : Problem
    {

        private const string PatternDistance = @"^(\w+) to (\w+) = (\d+)$";
        private readonly List<Distance> Distances;
        private Dictionary<string, int> LocationIDs { get; set; }
        private int[,] _distanceMatrix;

        public Problem_2015_09() : base()
        {
            Distances = [];
            LocationIDs = [];
            _distanceMatrix = new int[0, 0];
        }

        public override string Solve()
        {
            Regex pattern = MyRegex();
            int locationIdsIndex = 0;
            foreach (string line in InputLines)
            {
                Match match = pattern.Match(line);
                if (!match.Success)
                {
                    throw new Exception("Invalid line in input.");
                }
                else
                {
                    string startingLocation = match.Groups[1].Value;
                    string endingLocation = match.Groups[2].Value;
                    // Already validated in regex that is a number
                    int cost = int.Parse(match.Groups[3].Value);

                    // Assign unique int to each location
                    if (LocationIDs.TryAdd(startingLocation, locationIdsIndex))
                    {
                        locationIdsIndex++;
                    }
                    if (LocationIDs.TryAdd(endingLocation, locationIdsIndex))
                    {
                        locationIdsIndex++;
                    }

                    Distances.Add(new Distance(startingLocation, endingLocation, cost));

                }
            }
            _distanceMatrix = BuildDistanceMatrix();

            string part1 = SolvePart1();
            string part2 = SolvePart2();

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";

        }

        private string SolvePart1()
        {
            return ComputeShortestPath();
        }

        private string SolvePart2()
        {
            return ComputeLongestPath();
        }

        private string ComputeShortestPath()
        {
            // We send the distance matrix, no starting node (we could select any node here but the results will vary) and the returnToOrigin flag set as false
            // to instruct the algorithm to ignore the path from the ending node back to the starting node, as default in the Held-Karp algorithm
            int shortestPathLength = new HeldKarpAlgorithm(_distanceMatrix, null, false).GetShortestPathCost();
            return shortestPathLength.ToString();
        }

        private string ComputeLongestPath()
        {
            int intLongestPath = new HeldKarpAlgorithm(_distanceMatrix, null, false).GetLongestPathCost();
            return intLongestPath.ToString();
        }

        private int[,] BuildDistanceMatrix()
        {
            int nLocations = LocationIDs.Values.Max();
            int[,] distanceMatrix = new int[nLocations + 1, nLocations + 1];

            // For the Held-Karp algorithm, we have to build a distance matrix with the distances between each location
            foreach (Distance distance in Distances)
            {
                int startingLocationIndex = LocationIDs[distance.StartingLocation];
                int endingLocationIndex = LocationIDs[distance.EndingLocation];

                // The direction is irrelevant, so it is the same cost going from A->B or B->A
                distanceMatrix[startingLocationIndex, endingLocationIndex] = distance.Cost;
                distanceMatrix[endingLocationIndex, startingLocationIndex] = distance.Cost;
            }
            return distanceMatrix;
        }

        [GeneratedRegex(PatternDistance, RegexOptions.Compiled)]
        private static partial Regex MyRegex();
    }
}
