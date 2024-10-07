using AdventOfCode._2015_14;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    public partial class Problem_2015_14 : Problem
    {

        private const string RaindeerSpeedPattern = @"^(?<name>\w+) can fly (?<speed>\d+) km/s for (?<flytime>\d+) seconds, but then must rest for (?<resttime>\d+) seconds.";
        private const int RaceTime = 2503;

        public Problem_2015_14() : base()
        { }

        public override string Solve()
        {
            Regex pattern = InputRegex();
            List<Raindeer> raindeerList = [];

            foreach (string line in InputLines)
            {
                Match match = pattern.Match(line);
                if (!match.Success)
                {
                    throw new Exception("Invalid line in input.");
                }
                else
                {
                    string raindeerName = match.Groups[1].Value;
                    int speed = int.Parse(match.Groups[2].Value);
                    int flyTime = int.Parse(match.Groups[3].Value);
                    int restTime = int.Parse(match.Groups[4].Value);
                    raindeerList.Add(new Raindeer(raindeerName, speed, flyTime, restTime));
                }
            }
            string part1 = SolvePart1(raindeerList);

            ResetRaindeerList(raindeerList);

            string part2 = SolvePart2(raindeerList);

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";

        }

        private static string SolvePart1(IEnumerable<Raindeer> raindeerList)
        {
            Parallel.ForEach(raindeerList, raindeer => { raindeer.ActForN(RaceTime); });
            return raindeerList.Max(raindeer => raindeer.TraveledDistance).ToString();
        }

        private static string SolvePart2(IEnumerable<Raindeer> raindeerList)
        {
            for (int i = 0; i < RaceTime; i++)
            {
                Parallel.ForEach(raindeerList, raindeer => { raindeer.ActForSingle(); });

                var maxDistanceTraveled = raindeerList.Max(raindeer => raindeer.TraveledDistance);

                // Get all raindeers in the lead for that specific second
                var roundWinners = raindeerList.Where(raindeer => raindeer.TraveledDistance == maxDistanceTraveled);
                foreach (var raindeer in roundWinners)
                {
                    raindeer.AccumulatedPoints++;
                }
            }

            return raindeerList.Max(raindeer => raindeer.AccumulatedPoints).ToString();
        }

        private static void ResetRaindeerList(IEnumerable<Raindeer> raindeerList)
        {
            foreach (Raindeer raindeer in raindeerList)
            {
                raindeer.TraveledDistance = 0;
                raindeer.AccumulatedPoints = 0;
                raindeer.ChangeState(new FlyingRaindeerState(raindeer));
            }
        }

        [GeneratedRegex(RaindeerSpeedPattern, RegexOptions.Compiled)]
        private static partial Regex InputRegex();
    }
}
