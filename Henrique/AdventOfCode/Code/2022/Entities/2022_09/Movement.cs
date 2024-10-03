namespace AdventOfCode._2022_09
{
    internal class Movement
    {
        internal Direction Direction;
        internal int Steps;

        internal Movement(char direction, int steps)
        {
            Direction = direction switch
            {
                'L' => Direction.Left,
                'R' => Direction.Right,
                'U' => Direction.Up,
                'D' => Direction.Down,
                _ => throw new ArgumentException(direction.GetType().Name),
            };
            Steps = steps;
        }
    }

    internal enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }
}
