using AdventOfCode.Code._2022.Entities._2022_13;
using System.Text.RegularExpressions;
using ValueType = AdventOfCode.Code._2022.Entities._2022_13.ValueType;

namespace AdventOfCode.Code;

public partial class Problem_2022_13 : Problem
{
    private const string InputRegexPattern = @"(?<number>^[\d,]+$)|(?<listofnumbers>^\[\d+(,+\d+)*\]$)|(?<emptylist>^\[\]$)|(?<listoflists>(^\[[\[\d\]]+(,[\[\]\d]+)*\]$))";


    public Problem_2022_13() : base()
    { }

    public override string Solve()
    {
        string part1 = SolvePart1();
        string part2 = SolvePart2();

        return string.Format(SolutionFormat, part1, part2);
    }

    private string SolvePart1()
    {
        List<string> inputLines = [.. InputLines];
        for (int i = 0; i < inputLines.Count; i += 3)
        {
            string first = inputLines[i];
            string second = inputLines[i + 1];
            NextValue nextFirstValue = GrabNextValue(first);
            NextValue nextSecondValue = GrabNextValue(second);
        }
        return "";
    }

    private string SolvePart2()
    {
        return "";
    }

    private static NextValue GrabNextValue(string input)
    {
        Match matches = InputRegex().Match(input);
        if (!matches.Success)
        {
            throw new Exception("Invalid input format.");
        }
        else
        {
            if (matches.Groups["number"].Success)
            {
                return BuildNextValue(ValueType.Number, matches.Groups["number"].Value);
            }
            else if (matches.Groups["listofnumbers"].Success)
            {
                return BuildNextValue(ValueType.ListOfNumbers, RemoveOutermostBrackets(matches.Groups["listofnumbers"].Value));
            }
            else if (matches.Groups["emptylist"].Success)
            {
                return BuildNextValue(ValueType.EmptyList, matches.Groups["emptylist"].Value);
            }
            else if (matches.Groups["listoflists"].Success)
            {
                return BuildNextValue(ValueType.ListOfLists, RemoveOutermostBrackets(matches.Groups["listoflists"].Value));
            }
            else
            {
                throw new Exception("Invalid input format.");
            }
        }
    }

    private static string RemoveOutermostBrackets(string input)
    {
        if (input[0] == '[' && input[^1] == ']')
        {
            input = input[1..^1];
        }
        return input;
    }

    private static NextValue BuildNextValue(ValueType valueType, string value)
    {
        return new NextValue(valueType, value);
    }

    [GeneratedRegex(InputRegexPattern, RegexOptions.Compiled)]
    private static partial Regex InputRegex();

}