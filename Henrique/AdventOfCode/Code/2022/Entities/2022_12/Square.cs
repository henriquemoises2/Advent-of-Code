using AdventOfCode.DataStructures;

namespace AdventOfCode.Code._2022.Entities._2022_12;

internal class Square(Coordinates coordinates, char elevation)
{
    internal Coordinates Position { get; } = coordinates;
    internal char Elevation { get; } = elevation;

}
