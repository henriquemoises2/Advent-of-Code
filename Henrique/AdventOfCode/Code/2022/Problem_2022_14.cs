using AdventOfCode.Constants;
using AdventOfCode.DataStructures;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code;
public partial class Problem_2022_14 : Problem
{
    private const string CoordinatesGroupName = "Coordinates";
    private const string SomeRegexPattern = $@"(?<{CoordinatesGroupName}>\d+\,\d+)( -> )*";

    private static readonly Coordinates SandSourceCoordinates = new(500, 0);
    private const char StoneChar = '#';
    private const char SandChar = 'o';

    public Problem_2022_14() : base()
    { }

    public override string Solve()
    {
        Dictionary<Coordinates, char> initialCaveMap = InitializeCaveMap();

        Dictionary<Coordinates, char> caveMap = new(initialCaveMap);
        int abyssLevel = GetAbyssLevel(caveMap);
        int floorLevel = abyssLevel + 2;

        string part1 = SolvePart1(SandSourceCoordinates, caveMap, floorLevel, abyssLevel);

        caveMap = new(initialCaveMap);
        string part2 = SolvePart2(SandSourceCoordinates, caveMap, floorLevel);

        return string.Format(SolutionFormat, part1, part2);
    }

    private static string SolvePart1(Coordinates sandSourceCoordinates, Dictionary<Coordinates, char> caveMap, int floorLevel, int abyssLevel)
    {
        Coordinates sandRestingPosition;
        do
        {
            sandRestingPosition = DropUnitOfSand(caveMap, sandSourceCoordinates, floorLevel);
        }
        while (!HasSandReachedAbyss(sandRestingPosition, abyssLevel));

        // Subtract one since we are considering the last piece of sand when trying to figure out if it has reached the abyss threshold
        return (caveMap.Count(x => x.Value == SandChar) - 1).ToString();
    }

    private static string SolvePart2(Coordinates sandSourceCoordinates, Dictionary<Coordinates, char> caveMap, int floorLevel)
    {
        Coordinates sandRestingPosition;
        do
        {
            sandRestingPosition = DropUnitOfSand(caveMap, sandSourceCoordinates, floorLevel);
        }
        while (!HasSandReachedSource(sandRestingPosition));
        return caveMap.Count(x => x.Value == SandChar).ToString();
    }

    private static Coordinates DropUnitOfSand(Dictionary<Coordinates, char> caveMap, Coordinates SandSourceCoordinates, int maxDepth)
    {
        // Compute sand fall until it comes to a halt
        Coordinates sandCoordinates = new(SandSourceCoordinates);
        do
        {
            Coordinates spaceBelow = new(sandCoordinates.X, sandCoordinates.Y + 1);
            if (IsSandOrStone(caveMap, spaceBelow))
            {
                // There is sand or rock below
                Coordinates spaceBelowLeft = new(sandCoordinates.X - 1, sandCoordinates.Y + 1);
                if (IsSandOrStone(caveMap, spaceBelowLeft))
                {
                    // There is sand or rock below and left
                    Coordinates spaceBelowRight = new(sandCoordinates.X + 1, sandCoordinates.Y + 1);
                    if (IsSandOrStone(caveMap, spaceBelowRight))
                    {
                        // There is sand or rock below and right
                        // Sand comes to a halt
                        caveMap[sandCoordinates] = SandChar;
                        return sandCoordinates;
                    }
                    else
                    {
                        sandCoordinates.X++;
                    }
                }
                else
                {
                    sandCoordinates.X--;
                }
            }
            sandCoordinates.Y++;
        }
        while (sandCoordinates.Y < maxDepth);

        // Insert last sand one level above the max depth because its fall was simulated until it reached the threshold
        sandCoordinates.Y--;
        caveMap[sandCoordinates] = SandChar;
        return sandCoordinates;
    }

    private static bool HasSandReachedAbyss(Coordinates position, int abyssLevel) => position.Y > abyssLevel;

    private static bool HasSandReachedSource(Coordinates position) => position.X == SandSourceCoordinates.X && position.Y == SandSourceCoordinates.Y;

    private Dictionary<Coordinates, char> InitializeCaveMap()
    {
        Dictionary<Coordinates, char> caveMap = [];
        Regex pattern = InputRegex();
        foreach (string line in InputLines)
        {
            MatchCollection matches = pattern.Matches(line);
            if (matches.Count > 0)
            {
                var listOfcoordinates = matches.Select(x => x.Groups[CoordinatesGroupName]).ToList();
                for (int i = 0; i < listOfcoordinates.Count - 1; i++)
                {
                    string startCoordinates = listOfcoordinates[i].Value;
                    int startX = int.Parse(startCoordinates.Split(',')[0]);
                    int startY = int.Parse(startCoordinates.Split(',')[1]);

                    string endCoordinates = listOfcoordinates[i + 1].Value;
                    int endX = int.Parse(endCoordinates.Split(',')[0]);
                    int endY = int.Parse(endCoordinates.Split(',')[1]);

                    caveMap.TryAdd(new(startX, startY), StoneChar);
                    while (endX - startX != 0 || endY - startY != 0)
                    {
                        // Right
                        if (endX - startX > 0)
                        {
                            startX++;
                        }
                        // Left
                        else if (endX - startX < 0)
                        {
                            startX--;
                        }
                        // Down
                        else if (endY - startY > 0)
                        {
                            startY++;
                        }
                        // Up
                        else if (endY - startY < 0)
                        {
                            startY--;
                        }
                        caveMap.TryAdd(new(startX, startY), StoneChar);
                    }
                }
            }
            else
            {
                throw new Exception(Messages.InvalidInputErrorMessage);
            }
        }
        return caveMap;
    }

    private static int GetAbyssLevel(Dictionary<Coordinates, char> caveMap) => caveMap.Keys.Max(coord => coord.Y);

    private static bool IsSandOrStone(Dictionary<Coordinates, char> caveMap, Coordinates coordinates) =>
        caveMap.TryGetValue(coordinates, out char spaceContent) && spaceContent is SandChar or StoneChar;

    [GeneratedRegex(SomeRegexPattern, RegexOptions.Compiled)]
    private static partial Regex InputRegex();
}