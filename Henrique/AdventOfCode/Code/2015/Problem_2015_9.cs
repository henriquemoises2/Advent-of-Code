using AdventOfCode.Algorithms;
using AdventOfCode.Code._2015_9;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    internal class Problem_2015_9 : Problem
    {

        private const string PatternDistance = @"^(\w+) to (\w+) = (\d+)$";
        private List<Distance> Distances;
        private Dictionary<string, int> LocationIDs { get; set; }

        internal Problem_2015_9() : base()
        {
            Distances = new List<Distance>();
            LocationIDs = new Dictionary<string, int>();
        }

        internal override string Solve()
        {
            string part1 = SolvePart1();
            string part2 = SolvePart2();

            return $"Part 1 solution: " + part1 + "\n"
                + "Part 2 solution: " + part2;
        }

        private string SolvePart1()
        {
            Regex pattern = new Regex(PatternDistance, RegexOptions.Compiled);
            string startingLocation = string.Empty;
            string endingLocation = string.Empty;
            int cost = 0;
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
                    startingLocation = match.Groups[1].Value;
                    endingLocation = match.Groups[2].Value;
                    // Already validated in regex that is a number
                    cost = int.Parse(match.Groups[3].Value);

                    // Assign unique ID to each location
                    if (!LocationIDs.ContainsKey(startingLocation))
                    {
                        LocationIDs.Add(startingLocation, locationIdsIndex);
                        locationIdsIndex++;
                    }
                    if (!LocationIDs.ContainsKey(endingLocation))
                    {
                        LocationIDs.Add(endingLocation, locationIdsIndex);
                        locationIdsIndex++;
                    }

                    Distances.Add(new Distance(startingLocation, endingLocation, cost));

                }
            }
            string origin = SelectOrigin();
            string destination = SelectDestination();

            string result = ComputeShortestPath(origin, destination);

            return result;
        }

        private string SolvePart2()
        {
            return "";
        }

        private string SelectOrigin()
        {
            if (Distances == null || !Distances.Any())
            {
                return String.Empty;
            }
            // Look throught the Distances set to see which location never appears as an ending location.
            Distance? startingLocation = Distances.Where(d => !Distances.Select(ad => ad.EndingLocation).Contains(d.StartingLocation)).FirstOrDefault();
            if (startingLocation == null)
            {
                return Distances.First().StartingLocation;
            }
            return startingLocation.StartingLocation;
        }

        private string SelectDestination()
        {
            if (Distances == null || !Distances.Any())
            {
                return String.Empty;
            }
            // Look throught the Distances set to see which location never appears as a starting location.
            Distance? endingLocation = Distances.Where(d => !Distances.Select(ad => ad.StartingLocation).Contains(d.EndingLocation)).FirstOrDefault();
            if (endingLocation == null)
            {
                return Distances.First().EndingLocation;
            }
            return endingLocation.EndingLocation;
        }

        private string ComputeShortestPath(string origin, string destination)
        {
            int nLocations = LocationIDs.Values.Max();
            int[,] distanceMatrix = new int[nLocations + 1, nLocations + 1];
            for (int i = 0; i < nLocations + 1; i++)
            {
                for (int j = 0; j < nLocations + 1; j++)
                {
                    if(i != j)
                    {
                        // We simulate infinity in this step with an arbitrarily large number but to which we can still add other values
                        distanceMatrix[i, j] = int.MaxValue / 2;
                    }
                }
            }

            foreach (Distance distance in Distances)
            {
                int startingLocationIndex = LocationIDs[distance.StartingLocation];
                int endingLocationIndex = LocationIDs[distance.EndingLocation];

                distanceMatrix[startingLocationIndex, endingLocationIndex] = distance.Cost;
            }

            int shortestPathLength = new HeldKarpAlgorithm(distanceMatrix, LocationIDs[origin]).GetShortestPathLenght();
            return shortestPathLength.ToString();
        }
    }
}
