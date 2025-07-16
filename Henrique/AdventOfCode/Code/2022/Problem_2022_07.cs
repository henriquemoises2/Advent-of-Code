using AdventOfCode._2022_07;
using AdventOfCode.DataStructures;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    public partial class Problem_2022_07 : Problem
    {

        private const string CommandRegexPattern = @"(?<cdExpression>\$ cd (\w+|\/|\.\.))|(?<lsExpression>\$ ls)|(?<dirExpression>dir \w+)|(?<fileExpression>\d+ \w+\.*\w*)";
        private const double MaxFileSize = 100000;
        private const double RequiredSpace = 30000000;
        private const double TotalSystemSpace = 70000000;

        public Problem_2022_07() : base()
        { }

        public override string Solve()
        {

            Tree<FileSystemItem> fileSystem;
            TreeNode<FileSystemItem> startingNode;
            Regex pattern = InputRegex();
            Match match = pattern.Match(InputFirstLine);
            if (match.Groups["cdExpression"].Success && match.Value[5] == '/')
            {
                startingNode = new TreeNode<FileSystemItem>(null, new FileSystemItem("/", true));
                startingNode.Parent = startingNode;
                fileSystem = new Tree<FileSystemItem>(startingNode);
            }
            else
            {
                throw new Exception("Invalid line in input.");
            }

            foreach (string line in InputLines.Skip(1))
            {
                match = pattern.Match(line);
                if (match.Groups["cdExpression"].Success)
                {
                    ProcessCdExpression(fileSystem, match.Value);
                }
                else if (match.Groups["lsExpression"].Success)
                {
                    ProcessLsExpression();
                }
                else if (match.Groups["dirExpression"].Success)
                {
                    ProcessDirExpression(fileSystem, match.Value);
                }
                else if (match.Groups["fileExpression"].Success)
                {
                    ProcessFileExpression(fileSystem, match.Value);
                }
                else
                {
                    throw new Exception("Invalid line in input.");
                }
            }

            fileSystem.Root = startingNode;
            fileSystem.CurrentNode = startingNode;

            string part1 = SolvePart1(fileSystem);
            string part2 = SolvePart2(fileSystem);

            return string.Format(SolutionFormat, part1, part2);

        }

        private static void ProcessFileExpression(Tree<FileSystemItem> fileSystem, string value)
        {
            string[] fileValues = value.Split(' ');
            fileSystem.CurrentNode.AddChild(new TreeNode<FileSystemItem>(fileSystem.CurrentNode, new FileSystemItem(fileValues[1], false, long.Parse(fileValues[0]))));
        }

        private static void ProcessDirExpression(Tree<FileSystemItem> fileSystem, string value)
        {
            value = value[4..];
            fileSystem.CurrentNode.AddChild(new TreeNode<FileSystemItem>(fileSystem.CurrentNode, new FileSystemItem(value, true)));
        }

        private static void ProcessLsExpression()
        {
            // Do nothing as we are reading on a line-by-line basis.
        }

        private static void ProcessCdExpression(Tree<FileSystemItem> fileSystem, string value)
        {
            value = value[5..];
            if (value == "..")
            {
                fileSystem.CurrentNode = fileSystem.CurrentNode.Parent ?? throw new Exception("Cannot back from root node as it has no parent.");
            }
            else
            {
                fileSystem.CurrentNode = fileSystem.CurrentNode.Children.First(children => children.Value.Name == value);
            }
        }

        private static string SolvePart1(Tree<FileSystemItem> fileSystem)
        {
            double totalSize = fileSystem.Root.ComputeNodeValue(a =>
            {
                return a.Size;
            });

            static bool fileSystemFilteringBySize(TreeNode<FileSystemItem> file) => file.NodeValue <= MaxFileSize && file.Value.IsFolder;

            IEnumerable<TreeNode<FileSystemItem>> filteredFileSystem = fileSystem.FilterBy(fileSystemFilteringBySize);

            return filteredFileSystem.Sum(folder => folder.NodeValue).ToString();
        }

        private static string SolvePart2(Tree<FileSystemItem> fileSystem)
        {
            double totalSystemSize = fileSystem.Root.NodeValue;
            bool fileSystemFilteringBySize(TreeNode<FileSystemItem> file) => TotalSystemSpace - totalSystemSize + file.NodeValue >= RequiredSpace && file.Value.IsFolder;

            IEnumerable<TreeNode<FileSystemItem>> filteredFileSystem = fileSystem.FilterBy(fileSystemFilteringBySize);

            return filteredFileSystem.Min(folder => folder.NodeValue).ToString();
        }

        [GeneratedRegex(CommandRegexPattern, RegexOptions.Compiled)]
        private static partial Regex InputRegex();
    }
}