using AdventOfCode.Code._2022.Entities._2022_11;
using AdventOfCode.Helpers;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    // Very hard!
    public partial class Problem_2022_11 : Problem
    {

        private const string MonkeyNumberRegexPattern = @"^Monkey (?<monkey>\d+):$";
        private const string StartingItemsRegexPattern = @"^  Starting items:((, | )*(?<value>\d+))+$";
        private const string OperationRegexPattern = @"^  Operation: new = old (?<operation>.) (?<value>(old|\d+))$";
        private const string TestRegexPattern = @"^  Test: divisible by (?<value>\d+)$";
        private const string ConditionRegexPattern = @"^    If (?<condition>true|false): throw to monkey (?<monkey>\d+)$";

        private const int NumRoundsPart1 = 20;
        private const int NumRoundsPart2 = 10000;
        private const int NumMostActiveMonkeys = 2;


        public Problem_2022_11() : base()
        { }

        public override string Solve()
        {
            List<Monkey> monkeys = [];
            List<Monkey> monkeyClones = [];
            MonkeyInputParameters monkeyInputParameters = new();

            foreach (string line in InputLines)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        monkeys.Add(new Monkey(monkeyInputParameters.Number, monkeyInputParameters.Items, monkeyInputParameters.Operation, monkeyInputParameters.OperationValue, monkeyInputParameters.ConditionTest, monkeyInputParameters.ConditionMonkeyReceiverIfTrue, monkeyInputParameters.ConditionMonkeyReceiverIfFalse));
                        monkeyClones.Add(new Monkey(monkeyInputParameters.Number, monkeyInputParameters.Items, monkeyInputParameters.Operation, monkeyInputParameters.OperationValue, monkeyInputParameters.ConditionTest, monkeyInputParameters.ConditionMonkeyReceiverIfTrue, monkeyInputParameters.ConditionMonkeyReceiverIfFalse));

                        monkeyInputParameters.Reset();
                    }

                    Regex monkeyNumberRegexPattern = MonkeyNumberRegex();
                    Regex startingItemsRegexPattern = StartingItemsRegex();
                    Regex testRegexPattern = TestRegex();
                    Regex operationRegexPattern = OperationRegex();
                    Regex conditionRegexPattern = ConditionRegex();

                    Match match = monkeyNumberRegexPattern.Match(line);
                    if (match.Success)
                    {
                        monkeyInputParameters.Number = int.Parse(match.Groups["monkey"].Value);
                    }

                    match = startingItemsRegexPattern.Match(line);
                    if (match.Success)
                    {
                        foreach (long value in match.Groups["value"].Captures.Select(x => long.Parse(x.Value)))
                        {
                            monkeyInputParameters.Items.Add(value);
                        }
                    }

                    match = operationRegexPattern.Match(line);
                    if (match.Success)
                    {
                        monkeyInputParameters.Operation = char.Parse(match.Groups["operation"].Value);
                        monkeyInputParameters.OperationValue = match.Groups["value"].Value;
                    }

                    match = testRegexPattern.Match(line);
                    if (match.Success)
                    {
                        monkeyInputParameters.ConditionTest = int.Parse(match.Groups["value"].Value);
                    }

                    match = conditionRegexPattern.Match(line);
                    if (match.Success)
                    {
                        monkeyInputParameters.ConditionValue = match.Groups["condition"].Value;
                        if (bool.Parse(monkeyInputParameters.ConditionValue))
                        {
                            monkeyInputParameters.ConditionMonkeyReceiverIfTrue = int.Parse(match.Groups["monkey"].Value);
                        }
                        if (!bool.Parse(monkeyInputParameters.ConditionValue))
                        {
                            monkeyInputParameters.ConditionMonkeyReceiverIfFalse = int.Parse(match.Groups["monkey"].Value);
                        }
                    }
                }
                catch
                {
                    throw new Exception("Invalid line in input.");
                }
            }

            string part1 = SolvePart1(monkeys);
            string part2 = SolvePart2(monkeyClones);

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";
        }

        private static string SolvePart1(List<Monkey> monkeys)
        {
            return PlayKeepAway(NumRoundsPart1, monkeys, true);
        }

        private static string SolvePart2(List<Monkey> monkeys)
        {
            return PlayKeepAway(NumRoundsPart2, monkeys, false);
        }

        private static string PlayKeepAway(int numberOfRounds, List<Monkey> monkeys, bool decreaseWorryValueAfterOperation)
        {
            // Compute LCM (Least Common Multiple) to be used bellow if worry values do not decrease after each operation
            int lcm = 1;
            if (!decreaseWorryValueAfterOperation)
            {
                lcm = MathOperations.LCM([.. monkeys.Select(m => m.TestValue)]);
            }

            for (int roundNumber = 0; roundNumber < numberOfRounds; roundNumber++)
            {
                foreach (Monkey monkey in monkeys)
                {
                    int monkeyTotalItems = monkey.Items.Count;
                    for (int monkeyItemNumber = 0; monkeyItemNumber < monkeyTotalItems; monkeyItemNumber++)
                    {
                        monkey.InspectionsCount++;
                        long itemBeforeOperation = monkey.Items.First();
                        long newItemValue;
                        newItemValue = monkey.ApplyOperation(monkey.Items.First());
                        if (decreaseWorryValueAfterOperation)
                        {
                            newItemValue = Monkey.DecreaseWorryValue(newItemValue);
                        }

                        monkey.Items.RemoveAt(0);

                        bool monkeySelectionPredicate(Monkey m) => monkey.Test(newItemValue) ?
                            m.Number == monkey.ReceiverMonkeyIfTestTrue :
                            m.Number == monkey.ReceiverMonkeyIfTestFalse;

                        monkeys.Single(monkeySelectionPredicate).Items.Add(newItemValue);
                    }
                }

                // Update all item values after each round, making sure that they are at the most the LCM (Least Common Multiple)
                // between all the monkey divisors. This guarantees that the worry values do not increase exponentially.
                if (!decreaseWorryValueAfterOperation)
                {
                    foreach (Monkey monkey in monkeys)
                    {
                        for (int j = 0; j < monkey.Items.Count; j++)
                        {
                            var worryValue = monkey.Items[j];
                            long newWorryValue = worryValue % lcm;
                            monkey.Items[j] = newWorryValue;
                        }
                    }
                }
            }

            long monkeyBusinessValue = ComputeMonkeyBusinessValue(monkeys);
            return monkeyBusinessValue.ToString();
        }

        private static long ComputeMonkeyBusinessValue(List<Monkey> monkeys)
        {
            IEnumerable<Monkey> mostActiveMonkeys = monkeys.OrderByDescending(m => m.InspectionsCount).Take(NumMostActiveMonkeys);
            long monkeyBusinessValue = 1;
            foreach (Monkey monkey in mostActiveMonkeys)
            {
                monkeyBusinessValue *= monkey.InspectionsCount;
            }

            return monkeyBusinessValue;
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