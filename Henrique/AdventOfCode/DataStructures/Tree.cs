namespace AdventOfCode.DataStructures
{
    internal class Tree<T>
    {
        internal TreeNode<T> StartingNode { get; set; }
        internal TreeNode<T> CurrentNode { get; set; }

        internal Tree(TreeNode<T> startingNode)
        {
            StartingNode = startingNode;
            CurrentNode = startingNode;
        }
    }

    internal class TreeNode<T>
    {
        internal TreeNode<T>? Parent { get; set; }
        internal T Value { get; set; }
        internal List<TreeNode<T>> Children { get; set; } = new();

        internal TreeNode(TreeNode<T>? parent, T value)
        {
            Parent = parent;
            Value = value;
        }

        internal void AddChild(TreeNode<T> node)
        {
            Children.Add(node);
        }
    }
}
