namespace AdventOfCode.DataStructures;

internal class Coordinates : IEquatable<Coordinates>
{
    internal int X;
    internal int Y;

    internal Coordinates(int x, int y)
    {
        X = x;
        Y = y;
    }

    internal Coordinates(Coordinates coordinates)
    {
        X = coordinates.X;
        Y = coordinates.Y;
    }

    public bool Equals(Coordinates? other) => other is Coordinates c && c?.X == X && c?.Y == Y;
    public override bool Equals(object? other) => other is Coordinates c && c.X == X && c.Y == Y;
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}