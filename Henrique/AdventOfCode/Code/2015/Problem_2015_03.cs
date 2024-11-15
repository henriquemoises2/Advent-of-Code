namespace AdventOfCode.Code
{
    public class Problem_2015_03 : Problem
    {
        private List<Tuple<int, int>> visitedHouses;
        private Tuple<int, int> SantaLastVisitedHouse;
        private Tuple<int, int> RoboSantaLastVisitedHouse;
        private int totalVisitedUniqueHouses;

        private enum MoveTurn
        {
            Santa,
            RoboSanta
        }

        public Problem_2015_03() : base()
        {
            visitedHouses = [];
            SantaLastVisitedHouse = Tuple.Create(0, 0);
            RoboSantaLastVisitedHouse = Tuple.Create(0, 0);
        }

        public override string Solve()
        {
            string part1 = SolvePart1();
            string part2 = SolvePart2();

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";
        }

        private string SolvePart1()
        {
            SantaLastVisitedHouse = new Tuple<int, int>(0, 0);
            visitedHouses =
            [
               SantaLastVisitedHouse
            ];
            totalVisitedUniqueHouses = 1;

            MoveTurn moveTurn = MoveTurn.Santa;

            for (int i = 0; i < InputFirstLine.Length; i++)
            {
                char direction = InputFirstLine[i];
                switch (direction)
                {
                    case '<':
                        GoWest(moveTurn);
                        break;
                    case '^':
                        GoNorth(moveTurn);
                        break;
                    case '>':
                        GoEast(moveTurn);
                        break;
                    case 'v':
                        GoSouth(moveTurn);
                        break;
                    default:
                        break;
                }
            }

            return totalVisitedUniqueHouses.ToString();
        }

        private string SolvePart2()
        {
            SantaLastVisitedHouse = new Tuple<int, int>(0, 0);
            RoboSantaLastVisitedHouse = new Tuple<int, int>(0, 0);
            visitedHouses =
            [
               SantaLastVisitedHouse
            ];
            totalVisitedUniqueHouses = 1;
            for (int i = 0; i < InputFirstLine.Length; i++)
            {

                MoveTurn moveTurn;
                // Alternate movement between Santa and RoboSanta
                if (i % 2 == 0)
                {
                    moveTurn = MoveTurn.Santa;
                }
                else
                {
                    moveTurn = MoveTurn.RoboSanta;
                }
                char direction = InputFirstLine[i];
                switch (direction)
                {
                    case '<':
                        GoWest(moveTurn);
                        break;
                    case '^':
                        GoNorth(moveTurn);
                        break;
                    case '>':
                        GoEast(moveTurn);
                        break;
                    case 'v':
                        GoSouth(moveTurn);
                        break;
                    default:
                        break;
                }
            }

            return totalVisitedUniqueHouses.ToString();
        }

        private void GoWest(MoveTurn moveTurn)
        {
            Tuple<int, int> lastVisitedHouse = (moveTurn == MoveTurn.Santa) ? SantaLastVisitedHouse : RoboSantaLastVisitedHouse;
            Tuple<int, int> newVisitedHouse = Tuple.Create(lastVisitedHouse.Item1 - 1, lastVisitedHouse.Item2);
            InsertHouse(newVisitedHouse, moveTurn);
        }

        private void GoNorth(MoveTurn moveTurn)
        {
            Tuple<int, int> lastVisitedHouse = (moveTurn == MoveTurn.Santa) ? SantaLastVisitedHouse : RoboSantaLastVisitedHouse;
            Tuple<int, int> newVisitedHouse = Tuple.Create(lastVisitedHouse.Item1, lastVisitedHouse.Item2 + 1);
            InsertHouse(newVisitedHouse, moveTurn);
        }

        private void GoEast(MoveTurn moveTurn)
        {
            Tuple<int, int> lastVisitedHouse = (moveTurn == MoveTurn.Santa) ? SantaLastVisitedHouse : RoboSantaLastVisitedHouse;
            Tuple<int, int> newVisitedHouse = Tuple.Create(lastVisitedHouse.Item1 + 1, lastVisitedHouse.Item2);
            InsertHouse(newVisitedHouse, moveTurn);
        }

        private void GoSouth(MoveTurn moveTurn)
        {
            Tuple<int, int> lastVisitedHouse = (moveTurn == MoveTurn.Santa) ? SantaLastVisitedHouse : RoboSantaLastVisitedHouse;
            Tuple<int, int> newVisitedHouse = Tuple.Create(lastVisitedHouse.Item1, lastVisitedHouse.Item2 - 1);
            InsertHouse(newVisitedHouse, moveTurn);
        }

        private void InsertHouse(Tuple<int, int> newVisitedHouse, MoveTurn moveTurn)
        {

            if (!visitedHouses.Contains(newVisitedHouse))
            {
                visitedHouses.Add(newVisitedHouse);
                totalVisitedUniqueHouses++;
            }
            if (moveTurn == MoveTurn.Santa)
            {
                SantaLastVisitedHouse = newVisitedHouse;
            }
            else
            {
                RoboSantaLastVisitedHouse = newVisitedHouse;
            }
        }
    }
}
