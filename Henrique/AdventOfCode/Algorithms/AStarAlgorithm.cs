namespace AdventOfCode.Algorithms;

internal class AStarAlgorithm<T>
{
    private AStarNode<T> StartNode { get; set; }
    private AStarNode<T> GoalNode { get; set; }
    private Func<AStarNode<T>, AStarNode<T>, double> HeuristicFunction { get; set; }

    internal AStarAlgorithm(AStarNode<T> start, AStarNode<T> goal, Func<AStarNode<T>, AStarNode<T>, double> heuristicFunction)
    {
        StartNode = start;
        GoalNode = goal;
        HeuristicFunction = heuristicFunction;
    }

    internal List<AStarNode<T>>? ComputeShortestPath()
    {
        PriorityQueue<AStarNode<T>, double> openNodes = new();
        StartNode.ComesFrom = null;
        StartNode.AccummulatedCost = 0;
        openNodes.Enqueue(StartNode, 0);

        while (openNodes.Count > 0)
        {
            // Get node with lowest f value - implement using a priority queue for faster retrieval of the best node
            AStarNode<T> currentNode = openNodes.Dequeue();

            // Check if we've reached the goal
            if (currentNode == GoalNode)
            {
                return ReconstructPath(currentNode);
            }

            // Check all neighboring nodes
            foreach (AStarNode<T> neighbourNode in currentNode.Neighbours)
            {
                double newCost = currentNode.AccummulatedCost + 1;

                if (neighbourNode.AccummulatedCost == 0 || newCost < neighbourNode.AccummulatedCost)
                {
                    neighbourNode.AccummulatedCost = newCost;
                    neighbourNode.HeuristicCost = HeuristicFunction(neighbourNode, GoalNode);
                    openNodes.Enqueue(neighbourNode, neighbourNode.TotalCost);
                    neighbourNode.ComesFrom = currentNode;
                }
            }
        }

        // Could not find path
        return null;
    }

    private static List<AStarNode<T>> ReconstructPath(AStarNode<T> node)
    {
        List<AStarNode<T>> path = [];
        AStarNode<T>? currentNode = node;
        while (currentNode != null)
        {
            path.Add(currentNode);
            currentNode = currentNode.ComesFrom;
        }
        path.Reverse();
        return path;
    }

}
