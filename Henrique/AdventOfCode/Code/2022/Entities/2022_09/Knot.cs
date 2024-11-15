using AdventOfCode._2022_09;

namespace AdventOfCode.Code._2022.Entities._2022_09
{
    internal abstract class Knot
    {
        internal int x;
        internal int y;

        internal List<Tuple<int, int>> VisitedCoordinates = [];
        internal Knot()
        {
            AddVisitedCoordinate(0, 0);
        }

        internal virtual void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    x--;
                    break;
                case Direction.Right:
                    x++;
                    break;
                case Direction.Down:
                    y--;
                    break;
                case Direction.Up:
                    y++;
                    break;
                default:
                    break;
            }
        }

        internal void AddVisitedCoordinate(int x, int y)
        {
            Tuple<int, int> newCoordinate = new(x, y);
            if (!VisitedCoordinates.Contains(newCoordinate))
            {
                VisitedCoordinates.Add(newCoordinate);
            }
        }
    }
}
