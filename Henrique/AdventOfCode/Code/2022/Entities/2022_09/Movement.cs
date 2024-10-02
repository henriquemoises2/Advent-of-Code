namespace AdventOfCode._2022_09
{
    internal class Movement
    {
        internal Direction Direction;
        internal int Steps;

        internal Movement(char direction, int steps)
        {
            switch (direction)
            {
                case 'L':
                    Direction = Direction.Left;
                    break;
                case 'R':
                    Direction = Direction.Right;
                    break;
                case 'U':
                    Direction = Direction.Up;
                    break;
                case 'D':
                    Direction = Direction.Down;
                    break;
                default: throw new ArgumentException();
            }
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
