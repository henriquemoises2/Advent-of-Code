using AdventOfCode._2022_09;

namespace AdventOfCode.Code._2022.Entities._2022_09
{
    internal class Head : Knot
    {
        internal override void Move(Direction direction)
        {
            base.Move(direction);
            AddVisitedCoordinate(x, y);
        }
    }
}
