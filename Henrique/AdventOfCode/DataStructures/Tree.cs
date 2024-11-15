namespace AdventOfCode.DataStructures
{
    internal class Tree<T>
    {
        internal TreeNode<T> Root { get; set; }
        internal TreeNode<T> CurrentNode { get; set; }

        internal Tree(TreeNode<T> startingNode)
        {
            Root = startingNode;
            CurrentNode = startingNode;
        }

        internal IEnumerable<TreeNode<T>> GetAsEnumerable()
        {
            IEnumerable<TreeNode<T>> result = Root.GetChildren();
            return result;
        }

        internal IEnumerable<TreeNode<T>> FilterBy(Func<TreeNode<T>, bool> func)
        {
            return GetAsEnumerable().Where(func);
        }
    }

    internal class TreeNode<T>
    {
        internal TreeNode<T>? Parent { get; set; }
        internal T Value { get; set; }
        internal double NodeValue { get; set; }
        internal List<TreeNode<T>> Children { get; set; } = [];

        internal TreeNode(TreeNode<T>? parent, T value)
        {
            Parent = parent;
            Value = value;
        }

        internal void AddChild(TreeNode<T> node)
        {
            Children.Add(node);
        }

        internal double ComputeNodeValue(Func<T, double> func)
        {
            foreach (var node in Children)
            {
                NodeValue += node.ComputeNodeValue(func);
            }
            NodeValue += func(Value);
            return NodeValue;
        }

        internal IEnumerable<TreeNode<T>> GetChildren()
        {

            foreach (var node in Children)
            {
                foreach (var child in node.GetChildren())
                {
                    yield return child;
                }
            }
            yield return this;

        }


    }
}
