using AdventOfCode.Algorithms;
using AdventOfCode.Code._2022.Entities._2022_12;

namespace AdventOfCode.Code;

public partial class Problem_2022_12 : Problem
{

    private const char StartElevation = 'E';
    private const char GoFromStartElevation = 'y';
    private const char GoalElevation = 'S';
    private const char GoToGoalElevation = 'b';

    public Problem_2022_12() : base()
    { }

    public override string Solve()
    {
        List<AStarNode<Square>> nodes = BuildAStarGraph();

        string part1 = SolvePart1(nodes);
        string part2 = SolvePart2();

        return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";
    }

    private string SolvePart1(List<AStarNode<Square>> nodes)
    {
        AStarNode<Square> start = nodes.First(x => x.Item.Elevation == StartElevation);
        AStarNode<Square> goal = nodes.First(x => x.Item.Elevation == GoalElevation);
        AStarAlgorithm<Square> aStarAlgorithm = new(start, goal, GetHeuristicFunction());
        List<AStarNode<Square>>? solutionPath = aStarAlgorithm.ComputeShortestPath();

        if (solutionPath == null)
        {
            return "No path found.";
        }

        if (IsDebugActive)
        {
            // Prevent stopwatch from counting map drawing time
            StopWatch.Stop();
            DrawMapAndSolutionPath(solutionPath);
        }

        return (solutionPath.Count - 1).ToString();
    }

    private static string SolvePart2()
    {
        return "";
    }

    private List<AStarNode<Square>> BuildAStarGraph()
    {
        List<AStarNode<Square>> nodes = [];
        int xSize = InputFirstLine.Length;
        int ySize = InputLines.Count();
        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                nodes.Add(new AStarNode<Square>()
                {
                    Item = new Square(x, y, InputLines.ElementAt(y)[x])
                });
            }
        }
        foreach (AStarNode<Square> node in nodes)
        {
            node.Neighbours = AggregateNeighbours(node, nodes);
        }

        return nodes;
    }

    private static List<AStarNode<Square>> AggregateNeighbours(AStarNode<Square> currentNode, List<AStarNode<Square>> nodes)
    {
        List<AStarNode<Square>> neighbours = [];
        Square currentSquareNode = currentNode.Item;
        int upX = currentSquareNode.X, upY = currentSquareNode.Y + 1;
        int rightX = currentSquareNode.X + 1, rightY = currentSquareNode.Y;
        int downX = currentSquareNode.X, downY = currentSquareNode.Y - 1;
        int leftX = currentSquareNode.X - 1, leftY = currentSquareNode.Y;

        AStarNode<Square>? upNode = nodes.SingleOrDefault(node => node.Item?.X == upX && node.Item?.Y == upY);
        AStarNode<Square>? rightNode = nodes.SingleOrDefault(node => node.Item?.X == rightX && node.Item?.Y == rightY);
        AStarNode<Square>? downNode = nodes.SingleOrDefault(node => node.Item?.X == downX && node.Item?.Y == downY);
        AStarNode<Square>? leftNode = nodes.SingleOrDefault(node => node.Item?.X == leftX && node.Item?.Y == leftY);

        ValidateAndAddNeighbourNode(neighbours, currentSquareNode, upNode);
        ValidateAndAddNeighbourNode(neighbours, currentSquareNode, rightNode);
        ValidateAndAddNeighbourNode(neighbours, currentSquareNode, downNode);
        ValidateAndAddNeighbourNode(neighbours, currentSquareNode, leftNode);

        return neighbours;
    }

    private static void ValidateAndAddNeighbourNode(List<AStarNode<Square>> neighbours, Square currentSquareNode, AStarNode<Square>? neighbourNode)
    {
        if (neighbourNode == null)
        {
            return;
        }

        Square neighbourSquare = neighbourNode.Item;
        if (currentSquareNode.Elevation == StartElevation)
        {
            if (neighbourSquare.Elevation >= GoFromStartElevation)
            {
                neighbours.Add(neighbourNode);
            }
        }
        else if (neighbourSquare.Elevation == GoalElevation)
        {
            if (currentSquareNode.Elevation <= GoToGoalElevation)
            {
                neighbours.Add(neighbourNode);
            }
        }
        else if (neighbourSquare.Elevation >= currentSquareNode.Elevation - 1)
        {
            neighbours.Add(neighbourNode);
        }
    }

    private static Func<AStarNode<Square>, AStarNode<Square>, double> GetHeuristicFunction()
    {
        return (currentNode, goal) =>
        {
            // Manhattan distance
            return Math.Abs(goal.Item.X - currentNode.Item.X) + Math.Abs(goal.Item.Y - currentNode.Item.Y);
        };
    }

    private void DrawMapAndSolutionPath(List<AStarNode<Square>> solutionPath)
    {
        int xSize = InputFirstLine.Length;
        int ySize = InputLines.Count();
        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                // Required for drawing the map correctly
                int invertedY = InputLines.Count() - y - 1;

                char charToPrint = solutionPath?.FirstOrDefault(node => node.Item.X == x && node.Item.Y == invertedY)?.Item.Elevation.ToString().ToUpper()[0] != null ? '.' : InputLines.ElementAt(invertedY)[x];
                Console.Write(charToPrint);
            }
            Console.WriteLine();
        }
    }

}