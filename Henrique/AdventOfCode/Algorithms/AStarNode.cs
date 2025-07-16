namespace AdventOfCode.Algorithms;

internal class AStarNode<T>
{
    // G = Accummulated heuristic value. Represents the accummulated cost to reach the goal from the start until this node.
    internal double AccummulatedCost { get; set; }

    // H = Heuristic value. Represents the value from this node to reach the goal.
    internal double HeuristicCost { get; set; }

    // F = Total heuristic value. Represents the current cost of this node.
    internal double TotalCost { get { return AccummulatedCost + HeuristicCost; } }

    // List of neighbours for this specific node.
    internal List<AStarNode<T>> Neighbours { get; set; } = [];

    // The best node from where this node comes from.
    internal AStarNode<T>? ComesFrom { get; set; }

    // The item that this node represents.
    internal required T Item { get; set; }
}
