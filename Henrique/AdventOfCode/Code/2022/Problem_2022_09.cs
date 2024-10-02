using AdventOfCode._2022_09;
using AdventOfCode.Code._2022.Entities._2022_09;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    public class Problem_2022_09 : Problem
    {

        private const string MovementRegexPattern = @"^(?<direction>\w) (?<steps>\d+)$";

        public Problem_2022_09() : base()
        { }

        public override string Solve()
        {
            Regex pattern = new(MovementRegexPattern, RegexOptions.Compiled);
            List<Movement> movementList = new List<Movement>();

            foreach (string line in InputLines)
            {
                Match match = pattern.Match(line);
                if (!match.Success)
                {
                    throw new Exception("Invalid line in input.");
                }
                else
                {
                    movementList.Add(new Movement(match.Groups["direction"].Value[0], int.Parse(match.Groups["steps"].Value.ToString())));
                }
            }

            string part1 = SolvePart1(movementList);
            string part2 = SolvePart2();

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";
        }

        private static string SolvePart1(List<Movement> movementList)
        {
            Head head = new Head();
            Tail tail = new Tail();

            foreach (Movement movement in movementList) 
            {
                for (int i = 0; i < movement.Steps; i++)
                {
                    head.Move(movement.Direction);
                    tail.Follow(head);
                }
            }
            return tail.VisitedCoordinates.Count.ToString();
        }

        private static string SolvePart2()
        {
            return "";
        }
    }
}