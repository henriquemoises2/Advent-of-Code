using System.Text.RegularExpressions;
using AdventOfCode.Code._2022.Entities._2022_13;
using ValueType = AdventOfCode.Code._2022.Entities._2022_13.ValueType;

namespace AdventOfCode.Code;

public partial class Problem_2022_13 : Problem
{
    private const string InputRegexPattern = @"(?<number>^[\d,]+)|(?<listofnumbers>^\[\d+(,+\d+)*\])|(?<emptylist>^\[\])|(?<listoflists>(^\[[\[\d\]]+(,[\[\]\d]+)*\]))";

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
        List<int> orderedItemIndexes = [];


        // TODO: Move into separate function
        for (int i = 0, currentPair = 1; i < inputLines.Count; i += 2, currentPair++)
        {
            string first = inputLines[i];
            string second = inputLines[++i];
            string remainingFirstValue = "", remainingSecondValue = "";

            while (true)
            {
                // TODO: Improve code, rename varaibles
                (NextValue, string) nextValueSplitFirst = GrabNextValue(first, remainingFirstValue);
                NextValue nextFirstValue = nextValueSplitFirst.Item1;
                // TODO: Find a way to update the remaining value and prevent search repetition
                if (remainingFirstValue.Length == 0)
                {
                    remainingFirstValue = nextValueSplitFirst.Item2;
                }

                (NextValue, string) nextValueSplitSecond = GrabNextValue(second, remainingSecondValue);
                NextValue nextSecondValue = nextValueSplitSecond.Item1;
                if (remainingSecondValue.Length == 0)
                {
                    remainingSecondValue = nextValueSplitSecond.Item2;
                }

                // TODO: Rework ifs/scenarios
                if (nextFirstValue.ValueType == ValueType.Empty && nextSecondValue.ValueType == ValueType.Empty)
                {
                    first = remainingFirstValue;
                    remainingFirstValue = "";
                    second = remainingSecondValue;
                    remainingSecondValue = "";
                    continue;
                }

                if (nextFirstValue.ValueType == ValueType.Empty && nextSecondValue.ValueType != ValueType.Empty)
                {
                    // Items are ordered
                    orderedItemIndexes.Add(currentPair);
                    break;
                }

                if (nextFirstValue.ValueType != ValueType.Empty && nextSecondValue.ValueType == ValueType.Empty)
                {
                    // Items are not ordered
                    break;
                }

                if (nextFirstValue.ValueType == ValueType.ListOfNumbers && nextSecondValue.ValueType == ValueType.ListOfNumbers)
                {
                    first = nextFirstValue.Value;
                    nextFirstValue = GrabNextValue(first, remainingFirstValue).Item1;

                    second = nextSecondValue.Value;
                    nextSecondValue = GrabNextValue(second, remainingSecondValue).Item1;
                }

                if (nextFirstValue.ValueType == ValueType.Number && nextSecondValue.ValueType == ValueType.ListOfNumbers)
                {
                    second = nextSecondValue.Value;
                    nextSecondValue = GrabNextValue(second, remainingSecondValue).Item1;
                }

                if (nextFirstValue.ValueType == ValueType.ListOfNumbers && nextSecondValue.ValueType == ValueType.Number)
                {
                    first = nextFirstValue.Value;
                    nextFirstValue = GrabNextValue(first, remainingFirstValue).Item1;
                }

                if (nextFirstValue.ValueType == ValueType.Number && nextSecondValue.ValueType == ValueType.Number)
                {
                    int nextFirstValueNumber = int.Parse(nextFirstValue.Value);
                    int nextSecondValueNumber = int.Parse(nextSecondValue.Value);

                    if (nextFirstValueNumber < nextSecondValueNumber)
                    {
                        // Items are ordered
                        orderedItemIndexes.Add(currentPair);
                        break;
                    }

                    if (nextFirstValueNumber > nextSecondValueNumber)
                    {
                        // Items are unordered
                        break;
                    }

                    // Items have the same numeric value
                    first = RemoveFirstNumber(first);
                    second = RemoveFirstNumber(second);

                    continue;
                }
                first = nextFirstValue.Value;
                second = nextSecondValue.Value;
            }
        }
        return orderedItemIndexes.Sum().ToString();
    }

    private string SolvePart2()
    {
        return "";
    }

    private static (NextValue, string) GrabNextValue(string input, string remainingText)
    {
        // TODO: Improve code of function, define constants
        if (input == "")
        {
            return (BuildNextValue(ValueType.Empty, ""), "");
        }

        Match matches = InputRegex().Match(input);
        string matchedText = "";
        if (!matches.Success)
        {
            throw new Exception("Invalid input format.");
        }
        else
        {
            if (matches.Groups["number"].Success)
            {
                matchedText = matches.Groups["number"].Value;
                remainingText = input[matchedText.Length..];
                remainingText = GetUnmatchedValue(matchedText, input);

                return (BuildNextValue(ValueType.Number, matchedText.Split(',').First()), remainingText);
            }
            else if (matches.Groups["listofnumbers"].Success)
            {
                matchedText = matches.Groups["listofnumbers"].Value;
                remainingText = GetUnmatchedValue(matchedText, input);
                if (matchedText.Contains(','))
                {
                    return (BuildNextValue(ValueType.ListOfNumbers, RemoveOutermostBrackets(matchedText)), remainingText);
                }
                return (BuildNextValue(ValueType.Number, RemoveOutermostBrackets(matchedText)), remainingText);
            }
            else if (matches.Groups["emptylist"].Success)
            {
                matchedText = matches.Groups["emptylist"].Value;
                remainingText = GetUnmatchedValue( matchedText, input);
                return (BuildNextValue(ValueType.EmptyList, RemoveOutermostBrackets(matchedText)), remainingText);
            }
            else if (matches.Groups["listoflists"].Success)
            {
                matchedText = matches.Groups["listoflists"].Value;
                remainingText = GetUnmatchedValue(matchedText, input);
                if (matchedText.Count(x => x == '[') == 1 && matchedText.Count(x => x == ']') == 1)
                {
                    return (BuildNextValue(ValueType.ListOfNumbers, RemoveOutermostBrackets(matchedText)), remainingText);
                }
                return (BuildNextValue(ValueType.ListOfLists, RemoveOutermostBrackets(matchedText)), remainingText);
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

    private static string RemoveFirstNumber(string input)
    {
        int indexOfComma = input.IndexOf(',');
        if (indexOfComma > 0)
        {
            input = input[(indexOfComma + 1)..];
            return input;
        }
        return "";
    }

    private static NextValue BuildNextValue(ValueType valueType, string value)
    {
        return new NextValue(valueType, value);
    }

    private static string GetUnmatchedValue(string matchedValue, string input)
    {
            string currentUnmatchedValue = input[matchedValue.Length..];
            if (currentUnmatchedValue.StartsWith(','))
            {
                currentUnmatchedValue = currentUnmatchedValue[1..];
            }
        return currentUnmatchedValue;
    }

    [GeneratedRegex(InputRegexPattern, RegexOptions.Compiled)]
    private static partial Regex InputRegex();

}