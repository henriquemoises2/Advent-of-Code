using AdventOfCode._2022_09;

namespace AdventOfCode.Code._2022.Entities._2022_09
{
    internal class Tail : Knot
    {
        internal void Follow(Knot knot)
        {
            if (!IsAdjacent(knot))
            {
                MoveToKnot(knot);
            }
        }

        private bool IsAdjacent(Knot knot)
        {
            return
                   // Left side
                   x + 1 == knot.x && y - 1 == knot.y
                || x + 1 == knot.x && y == knot.y
                || x + 1 == knot.x && y + 1 == knot.y

                // Right side
                || x - 1 == knot.x && y - 1 == knot.y
                || x - 1 == knot.x && y == knot.y
                || x - 1 == knot.x && y + 1 == knot.y

                // Up
                || y - 1 == knot.y && x == knot.x

                // Down
                || y + 1 == knot.y && x == knot.x;
        }

        private void MoveToKnot(Knot knot)
        {
            if (x < knot.x && y < knot.y)
            {
                Move(new Movement('R', 1).Direction);
                Move(new Movement('U', 1).Direction);
            }
            else if (x < knot.x && y > knot.y)
            {
                Move(new Movement('R', 1).Direction);
                Move(new Movement('D', 1).Direction);
            }
            else if (x > knot.x && y < knot.y)
            {
                Move(new Movement('L', 1).Direction);
                Move(new Movement('U', 1).Direction);
            }
            else if (x > knot.x && y > knot.y)
            {
                Move(new Movement('L', 1).Direction);
                Move(new Movement('D', 1).Direction);
            }
            else if (x < knot.x && y == knot.y)
            {
                Move(new Movement('R', 1).Direction);
            }
            else if (x > knot.x && y == knot.y)
            {
                Move(new Movement('L', 1).Direction);
            }
            else if (x == knot.x && y > knot.y)
            {
                Move(new Movement('D', 1).Direction);
            }
            else if (x == knot.x && y < knot.y)
            {
                Move(new Movement('U', 1).Direction);
            }
            AddVisitedCoordinate(x, y);
        }
    }
}
