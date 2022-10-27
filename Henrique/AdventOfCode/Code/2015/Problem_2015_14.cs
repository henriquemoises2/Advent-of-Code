using AdventOfCode._2015_14;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Code
{
    internal class Problem_2015_14 : Problem
    {

        private const string RaindeerSpeedPattern = @"^(?<name>\w+) can fly (?<speed>\d+) km/s for (?<flytime>\d+) seconds, but then must rest for (?<resttime>\d+) seconds.";
        private const int RaceTime = 2503;

        internal Problem_2015_14() : base()
        { }

        internal override string Solve()
        {
            Regex pattern = new Regex(RaindeerSpeedPattern, RegexOptions.Compiled);
            string raindeerName = string.Empty;
            int speed = 0;
            int flyTime = 0;
            int restTime = 0;

            List<Raindeer> raindeerList = new List<Raindeer>();

            foreach (string line in InputLines)
            {
                Match match = pattern.Match(line);
                if (!match.Success)
                {
                    throw new Exception("Invalid line in input.");
                }
                else
                {
                    raindeerName = match.Groups[1].Value;
                    speed = int.Parse(match.Groups[2].Value);
                    flyTime = int.Parse(match.Groups[3].Value);
                    restTime = int.Parse(match.Groups[4].Value);
                    raindeerList.Add(new Raindeer(raindeerName, speed, flyTime, restTime));
                }
            }
            string part1 = SolvePart1(raindeerList);

            ResetRaindeerList(raindeerList);

            string part2 = SolvePart2(raindeerList);

            return $"Part 1 solution: " + part1 + "\n"
                + "Part 2 solution: " + part2;
        }

        private string SolvePart1(IEnumerable<Raindeer> raindeerList)
        {
            Parallel.ForEach(raindeerList, raindeer => { raindeer.ActForN(RaceTime); });
            return raindeerList.Max(raindeer => raindeer.TraveledDistance).ToString();
        }

        private string SolvePart2(IEnumerable<Raindeer> raindeerList)
        {
            for(int i = 0; i < RaceTime; i++)
            {
                Parallel.ForEach(raindeerList, raindeer => { raindeer.ActForSingle(); });

                var maxDistanceTraveled = raindeerList.Max(raindeer => raindeer.TraveledDistance);

                // Get all raindeers in the lead for that specific second
                var roundWinners = raindeerList.Where(raindeer => raindeer.TraveledDistance == maxDistanceTraveled);
                foreach(var raindeer in roundWinners)
                {
                    raindeer.AccumulatedPoints++;
                }
            }
            
            return raindeerList.Max(raindeer => raindeer.AccumulatedPoints).ToString();
        }

        private void ResetRaindeerList(IEnumerable<Raindeer> raindeerList)
        {
            foreach(Raindeer raindeer in raindeerList)
            {
                raindeer.TraveledDistance = 0;
                raindeer.AccumulatedPoints = 0;
                raindeer.ChangeState(new FlyingRaindeerState(raindeer));
            }
        }

    }
}
