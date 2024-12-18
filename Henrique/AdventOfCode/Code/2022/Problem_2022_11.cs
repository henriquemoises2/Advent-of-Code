using AdventOfCode.Code._2022.Entities._2022_11;
using AdventOfCode.Helpers;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    public partial class Problem_2022_11 : Problem
    {

        private const string MonkeyNumberRegexPattern = @"^Monkey (?<monkey>\d+):$";
        private const string StartingItemsRegexPattern = @"^  Starting items:((, | )*(?<value>\d+))+$";
        private const string OperationRegexPattern = @"^  Operation: new = old (?<operation>.) (?<value>(old|\d+))$";
        private const string TestRegexPattern = @"^  Test: divisible by (?<value>\d+)$";
        private const string ConditionRegexPattern = @"^    If (?<condition>true|false): throw to monkey (?<monkey>\d+)$";

        private const int MaxRoundsPart1 = 20;
        private const int MaxRoundsPart2 = 10000;


        public Problem_2022_11() : base()
        { }

        public override string Solve()
        {
            List<Monkey> monkeys = [];
            int currentMonkey = 0;
            List<ulong> currentMonkeyInitialItems = [];
            char currentMonkeyOperation = new();
            string currentMonkeyOperationValue = "";
            int currentMonkeyTest = 0;
            string currentMonkeyConditionValue = "";
            int currentMonkeyConditionReceiverMonkeyTrue = 0;
            int currentMonkeyConditionReceiverMonkeyFalse = 0;

            foreach (string line in InputLines)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        monkeys.Add(new Monkey(currentMonkey, currentMonkeyInitialItems, currentMonkeyOperation, currentMonkeyOperationValue, currentMonkeyTest, currentMonkeyConditionReceiverMonkeyTrue, currentMonkeyConditionReceiverMonkeyFalse));
                    }

                    Regex monkeyNumberRegexPattern = MonkeyNumberRegex();
                    Regex startingItemsRegexPattern = StartingItemsRegex();
                    Regex testRegexPattern = TestRegex();
                    Regex operationRegexPattern = OperationRegex();
                    Regex conditionRegexPattern = ConditionRegex();

                    Match match = monkeyNumberRegexPattern.Match(line);
                    if (match.Success)
                    {
                        currentMonkey = int.Parse(match.Groups["monkey"].Value);
                    }

                    match = startingItemsRegexPattern.Match(line);
                    if (match.Success)
                    {
                        currentMonkeyInitialItems = match.Groups["value"].Captures.Select(x => ulong.Parse(x.Value)).ToList();
                    }

                    match = operationRegexPattern.Match(line);
                    if (match.Success)
                    {
                        currentMonkeyOperation = char.Parse(match.Groups["operation"].Value);
                        currentMonkeyOperationValue = match.Groups["value"].Value;
                    }

                    match = testRegexPattern.Match(line);
                    if (match.Success)
                    {
                        currentMonkeyTest = int.Parse(match.Groups["value"].Value);
                    }

                    match = conditionRegexPattern.Match(line);
                    if (match.Success)
                    {
                        currentMonkeyConditionValue = match.Groups["condition"].Value;
                        if (bool.Parse(currentMonkeyConditionValue))
                        {
                            currentMonkeyConditionReceiverMonkeyTrue = int.Parse(match.Groups["monkey"].Value);
                        }
                        if (!bool.Parse(currentMonkeyConditionValue))
                        {
                            currentMonkeyConditionReceiverMonkeyFalse = int.Parse(match.Groups["monkey"].Value);
                        }
                    }
                }
                catch
                {
                    throw new Exception("Invalid line in input.");
                }
            }

            List<Monkey> monkeyListClone = monkeys.ConvertAll<Monkey>(m => (Monkey)m.Clone());
            string part1 = SolvePart1(monkeys);
            string part2 = SolvePart2(monkeyListClone);

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";

        }

        private static string SolvePart1(List<Monkey> monkeys)
        {
            return PlayKeepAway(MaxRoundsPart1, monkeys, true);
        }


        private static string SolvePart2(List<Monkey> monkeys)
        {
            return MathOperations.MultiplyStrings("123", "10");
            //return PlayKeepAway(MaxRoundsPart2, monkeys, false);
        }

        private static string PlayKeepAway(int numberOfRounds, List<Monkey> monkeys, bool decreaseWorryLevelAfterOperation)
        {
            for (int i = 0; i < numberOfRounds; i++)
            {
                foreach (Monkey monkey in monkeys)
                {
                    int monkeyTotalItems = monkey.Items.Count;
                    for (int j = 0; j < monkeyTotalItems; j++)
                    {
                        monkey.InspectionsCount++;
                        ulong newItemValue = monkey.ApplyOperation(monkey.Items.First(), decreaseWorryLevelAfterOperation);
                        if (monkey.Test(newItemValue))
                        {
                            monkey.Items.RemoveAt(0);
                            monkeys.Single(m => m.Number == monkey.ReceiverMonkeyIfTestTrue).Items.Add(newItemValue);
                        }
                        else
                        {
                            monkey.Items.RemoveAt(0);
                            monkeys.Single(m => m.Number == monkey.ReceiverMonkeyIfTestFalse).Items.Add(newItemValue);
                        }
                    }
                }
            }

            IEnumerable<Monkey> mostActiveMonkeys = monkeys.OrderByDescending(m => m.InspectionsCount).Take(2);
            long monkeyBusinessValue = 1;
            foreach (Monkey monkey in mostActiveMonkeys)
            {
                monkeyBusinessValue *= monkey.InspectionsCount;
            }
            return monkeyBusinessValue.ToString();
        }


        [GeneratedRegex(MonkeyNumberRegexPattern, RegexOptions.Compiled)]
        private static partial Regex MonkeyNumberRegex();
        [GeneratedRegex(StartingItemsRegexPattern, RegexOptions.Compiled)]
        private static partial Regex StartingItemsRegex();
        [GeneratedRegex(TestRegexPattern, RegexOptions.Compiled)]
        private static partial Regex TestRegex();
        [GeneratedRegex(OperationRegexPattern, RegexOptions.Compiled)]
        private static partial Regex OperationRegex();
        [GeneratedRegex(ConditionRegexPattern, RegexOptions.Compiled)]
        private static partial Regex ConditionRegex();

    }
}