using AdventOfCode.Code._2022.Entities._2022_13;
using AdventOfCode.Constants;
using AdventOfCode.Helpers;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code;

public partial class Problem_2022_13 : Problem
{
    private const string NumberGroupName = "Number";
    private const string ListOfNumbersGroupName = "ListOfNumbers";
    private const string EmptyListGroupName = "EmptyList";
    private const string ListOfListsGroupName = "ListOfLists";
    private const string InputRegexPattern = $@"(?<{NumberGroupName}>^[\d,]+)|(?<{ListOfNumbersGroupName}>^\[\d+(,+\d+)*\])|(?<{EmptyListGroupName}>^\[\])|(?<{ListOfListsGroupName}>(^\[[\[\d\]]+(,[\[\]\d]+)*\]))";
    private const string DividerPacket1 = "[[2]]";
    private const string DividerPacket2 = "[[6]]";


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
        List<int> orderedItemsIndexes = [];

        for (int i = 0, currentPair = 1; i < inputLines.Count; i += 2, currentPair++)
        {
            string firstItem = inputLines[i], secondItem = inputLines[++i];

            if (AreItemsOrdered(firstItem, secondItem))
            {
                orderedItemsIndexes.Add(currentPair);
            }
        }
        return orderedItemsIndexes.Sum().ToString();
    }

    private string SolvePart2()
    {
        List<string> inputLines = [.. InputLines.Except([""])];

        List<string> itemsSequence = [
            DividerPacket1,
            DividerPacket2
        ];

        for (int inputLinesIndex = 0; inputLinesIndex < inputLines.Count; inputLinesIndex++)
        {
            for (int itemsSequenceIndex = 0; itemsSequenceIndex < itemsSequence.Count; itemsSequenceIndex++)
            {
                string firstItem = inputLines[inputLinesIndex], secondItem = itemsSequence[itemsSequenceIndex];

                if (AreItemsOrdered(firstItem, secondItem))
                {
                    itemsSequence.Insert(itemsSequenceIndex, inputLines[inputLinesIndex]);
                    break;
                }
                if (itemsSequenceIndex == itemsSequence.Count - 1)
                {
                    itemsSequence.Add(inputLines[inputLinesIndex]);
                    break;
                }
            }
        }
        int indexOfDividerPacket1 = itemsSequence.IndexOf(DividerPacket1) + 1;
        int indexOfDividerPacket2 = itemsSequence.IndexOf(DividerPacket2) + 1;

        return (indexOfDividerPacket1 * indexOfDividerPacket2).ToString();
    }

    private static bool AreItemsOrdered(string firstItem, string secondItem)
    {
        string remainingValueFirstItem = "", remainingValueSecondItem = "";

        while (true)
        {
            NextValue nextValueFirstItem = ProcessNextValue(firstItem, remainingValueFirstItem);
            // Improvement: Find a way to update the remaining value and prevent search repetition
            if (remainingValueFirstItem.Length == 0)
            {
                remainingValueFirstItem = nextValueFirstItem.RemainingValue;
            }

            NextValue nextValueSecondItem = ProcessNextValue(secondItem, remainingValueSecondItem);
            if (remainingValueSecondItem.Length == 0)
            {
                remainingValueSecondItem = nextValueSecondItem.RemainingValue;
            }

            // First value is empty and second is not: Items are ordered
            if (nextValueFirstItem.ValueType == NextValueType.Empty && nextValueSecondItem.ValueType != NextValueType.Empty)
            {
                return true;
            }

            // Second value is empty and first is not: Items are not ordered
            if (nextValueFirstItem.ValueType != NextValueType.Empty && nextValueSecondItem.ValueType == NextValueType.Empty)
            {
                return false;
            }

            // Both values are numbers: Compare them to decide if they are ordered
            if (nextValueFirstItem.ValueType == NextValueType.Number && nextValueSecondItem.ValueType == NextValueType.Number)
            {
                int nextFirstNumberValue = int.Parse(nextValueFirstItem.Value);
                int nextSecondNumberValue = int.Parse(nextValueSecondItem.Value);

                // Items are ordered
                if (nextFirstNumberValue < nextSecondNumberValue)
                {
                    return true;
                }

                // Items are unordered
                if (nextFirstNumberValue > nextSecondNumberValue)
                {
                    return false;
                }

                // Items have the same numeric value
                firstItem = RemoveFirstNumber(firstItem);
                secondItem = RemoveFirstNumber(secondItem);

                continue;
            }

            // Both values are empty: Assign remaining value and process
            if (nextValueFirstItem.ValueType == NextValueType.Empty && nextValueSecondItem.ValueType == NextValueType.Empty)
            {
                firstItem = remainingValueFirstItem;
                remainingValueFirstItem = "";

                secondItem = remainingValueSecondItem;
                remainingValueSecondItem = "";

                continue;
            }

            // Both values are lists of numbers: Continue processing
            if (nextValueFirstItem.ValueType == NextValueType.ListOfNumbers && nextValueSecondItem.ValueType == NextValueType.ListOfNumbers)
            {
                firstItem = nextValueFirstItem.Value;
                secondItem = nextValueSecondItem.Value;
                continue;
            }

            // First item is a number and second is a list: Continue processing second item
            if (nextValueFirstItem.ValueType == NextValueType.Number && nextValueSecondItem.ValueType == NextValueType.ListOfNumbers)
            {
                secondItem = nextValueSecondItem.Value;
                continue;
            }

            // Second item is a number and first is a list: Continue processing first item
            if (nextValueFirstItem.ValueType == NextValueType.ListOfNumbers && nextValueSecondItem.ValueType == NextValueType.Number)
            {
                firstItem = nextValueFirstItem.Value;
                continue;
            }

            // Continue to process value until a known pattern is obtained
            firstItem = nextValueFirstItem.Value;
            secondItem = nextValueSecondItem.Value;
        }
    }

    private static NextValue ProcessNextValue(string input, string remainingText)
    {
        if (input == "")
        {
            return BuildNextValue(NextValueType.Empty, "", "");
        }

        Match matches = InputRegex().Match(input);
        string matchedText = "";
        if (matches.Success)
        {
            if (matches.Groups[NumberGroupName].Success)
            {
                matchedText = matches.Groups[NumberGroupName].Value.Split(',').First();
                remainingText = GetUnmatchedValue(matchedText, input);

                return BuildNextValue(NextValueType.Number, RemoveNextBracketNesting(matchedText), remainingText);
            }
            else if (matches.Groups[ListOfNumbersGroupName].Success)
            {
                matchedText = RemoveNextBracketNesting(matches.Groups[ListOfNumbersGroupName].Value);
                remainingText = GetUnmatchedValue(matchedText, input);
                if (matchedText.Contains(','))
                {
                    return BuildNextValue(NextValueType.ListOfNumbers, matchedText, remainingText);
                }
                return BuildNextValue(NextValueType.Number, matchedText, remainingText);
            }
            else if (matches.Groups[EmptyListGroupName].Success)
            {
                matchedText = matches.Groups[EmptyListGroupName].Value;
                remainingText = GetUnmatchedValue(matchedText, input);
                return BuildNextValue(NextValueType.EmptyList, RemoveNextBracketNesting(matchedText), remainingText);
            }
            else if (matches.Groups[ListOfListsGroupName].Success)
            {
                matchedText = matches.Groups[ListOfListsGroupName].Value;
                remainingText = GetUnmatchedValue(matchedText, input);
                if (matchedText.Count(x => x == '[') == 1 && matchedText.Count(x => x == ']') == 1)
                {
                    return BuildNextValue(NextValueType.ListOfNumbers, RemoveNextBracketNesting(matchedText), remainingText);
                }
                return BuildNextValue(NextValueType.ListOfLists, RemoveNextBracketNesting(matchedText), remainingText);
            }
        }
        throw new Exception(Messages.InvalidInputErrorMessage);
    }

    private static string RemoveNextBracketNesting(string input)
    {
        if (input[0] == '[' && input[^1] == ']')
        {
            var parenthesesLevel = 0;
            int i = 0;
            for (; i < input.Length - 1; i++)
            {
                if (input[i] == '[')
                {
                    parenthesesLevel++;
                }
                else if (input[i] == ']')
                {
                    parenthesesLevel--;
                }
                if (parenthesesLevel == 0)
                {
                    break;
                }
            }
            input = input.RemoveAtIndex(0, "[");
            input = input.RemoveAtIndex(i - 1, "]");

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

    private static NextValue BuildNextValue(NextValueType valueType, string value, string remainingValue)
    {
        return new NextValue(valueType, value, remainingValue);
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