using AdventOfCode._2022_09;
using AdventOfCode.Code._2022.Entities._2022_09;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    public partial class Problem_2022_09 : Problem
    {

        private const string MovementRegexPattern = @"^(?<direction>\w) (?<steps>\d+)$";
        private const int RopeLength = 10;

        public Problem_2022_09() : base()
        { }

        public override string Solve()
        {
            Regex pattern = MyRegex();
            List<Movement> movementList = [];

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
            string part2 = SolvePart2(movementList);

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";
        }

        private static string SolvePart1(List<Movement> movementList)
        {
            Head head = new();
            Tail tail = new();

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

        private static string SolvePart2(List<Movement> movementList)
        {
            Head head = new();
            List<Tail> knots = [];
            for (int i = 0; i < RopeLength - 1; i++)
            {
                Tail tail = new();
                knots.Add(tail);
            }

            foreach (Movement movement in movementList)
            {
                for (int i = 0; i < movement.Steps; i++)
                {
                    head.Move(movement.Direction);
                    Knot previousKnot = head;
                    for (int tailIndex = 0; tailIndex < knots.Count; tailIndex++)
                    {
                        Tail tail = knots[tailIndex];
                        tail.Follow(previousKnot);
                        previousKnot = tail;
                    }
                }
            }
            Tail lastTail = knots[RopeLength - 2];
            return lastTail.VisitedCoordinates.Count.ToString();
        }

        [GeneratedRegex(MovementRegexPattern, RegexOptions.Compiled)]
        private static partial Regex MyRegex();
    }
}