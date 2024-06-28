using AdventOfCode._2022.Entities;
using AdventOfCode.DataStructures;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    public class Problem_2022_07 : Problem
    {

        private const string CommandRegexPattern = @"(?<cdExpression>\$ cd (\w+|\/|\.\.))|(?<lsExpression>\$ ls)|(?<dirExpression>dir \w+)|(?<fileExpression>\d+ \w+\.*\w*)";

        public Problem_2022_07() : base()
        { }

        public override string Solve()
        {
            TreeNode<FileSystemItem> startingNode;
            Tree<FileSystemItem> fileSystem;

            Regex pattern = new(CommandRegexPattern, RegexOptions.Compiled);
            Match match = pattern.Match(InputFirstLine);
            if (match.Groups["cdExpression"].Success && match.Value[5] == '/')
            {
                startingNode = new TreeNode<FileSystemItem>(null, new FileSystemItem("/"));
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

            string part1 = SolvePart1();
            string part2 = SolvePart2();

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";

        }

        private void ProcessFileExpression(Tree<FileSystemItem> fileSystem, string value)
        {
            string[] fileValues = value.Split(' ');
            fileSystem.CurrentNode.AddChild(new TreeNode<FileSystemItem>(fileSystem.CurrentNode, new FileSystemItem(fileValues[1], long.Parse(fileValues[0]))));
        }

        private void ProcessDirExpression(Tree<FileSystemItem> fileSystem, string value)
        {
            value = value.Substring(4);
            fileSystem.CurrentNode.AddChild(new TreeNode<FileSystemItem>(fileSystem.CurrentNode, new FileSystemItem(value)));
        }

        private void ProcessLsExpression()
        {
            // Do nothing
        }

        private void ProcessCdExpression(Tree<FileSystemItem> fileSystem, string value)
        {
            value = value.Substring(5);
            if (value == "..")
            {
                fileSystem.CurrentNode = fileSystem.CurrentNode.Parent;
            }
            else
            {
                fileSystem.CurrentNode = fileSystem.CurrentNode.Children.First(children => children.Value.Name == value);
            }
        }

        private static string SolvePart1()
        {
            return "";
        }

        private static string SolvePart2()
        {
            return "";
        }
    }
}