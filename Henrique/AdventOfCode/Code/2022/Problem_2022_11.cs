using AdventOfCode._2015_21;
using AdventOfCode.Code._2022.Entities._2022_11;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode.Code
{
    public partial class Problem_2022_11 : Problem
    {

        private const string MonkeyNumberRegexPattern = @"^Monkey (?<monkey>\d+):$";
        private const string StartingItemsRegexPattern = @"^  Starting items:((, | )*(?<value>\d+))+$";
        private const string OperationRegexPattern = @"^  Operation: new = old (?<operation>.) (?<value>(old|\d+))$";
        private const string TestRegexPattern = @"^  Test: divisible by (?<value>\d+)$";
        private const string ConditionRegexPattern = @"^    If (?<condition>true|false): throw to monkey (?<monkey>\d+)$";

        private const int NumRoundsPart1 = 20;
        private const int NumRoundsPart2 = 10000;

        public Problem_2022_11() : base()
        { }

        public override string Solve()
        {
            List<Monkey> monkeys = [];
            int currentMonkey = 0;
            List<Tuple<int, string, List<int>>> currentMonkeyInitialItems = [];
            char currentMonkeyOperation = new();
            string currentMonkeyOperationValue = "";
            int currentMonkeyTest = 0;
            string currentMonkeyConditionValue = "";
            int currentMonkeyConditionReceiverMonkeyTrue = 0;
            int currentMonkeyConditionReceiverMonkeyFalse = 0;
            int itemNumber = 0;

            foreach (string line in InputLines)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        monkeys.Add(new Monkey(currentMonkey, currentMonkeyInitialItems, currentMonkeyOperation, currentMonkeyOperationValue, currentMonkeyTest, currentMonkeyConditionReceiverMonkeyTrue, currentMonkeyConditionReceiverMonkeyFalse));

                        currentMonkeyInitialItems = [];
                        currentMonkeyOperation = new();
                        currentMonkeyOperationValue = "";
                        currentMonkeyTest = 0;
                        currentMonkeyConditionValue = "";
                        currentMonkeyConditionReceiverMonkeyTrue = 0;
                        currentMonkeyConditionReceiverMonkeyFalse = 0;
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
                        foreach (string value in match.Groups["value"].Captures.Select(x => x.Value))
                        {
                            currentMonkeyInitialItems.Add(new Tuple<int, string, List<int>>(itemNumber, value, []));
                            itemNumber++;
                        }
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

            List<Monkey> monkeyListClone = monkeys.ConvertAll(m => (Monkey)m.Clone());
            string part1 = "";// SolvePart1(monkeys);
            string part2 = SolvePart2(monkeyListClone);

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

        private static string PlayKeepAway(int numberOfRounds, List<Monkey> monkeys, bool decreaseWorryLevelAfterOperation)
        {
            for (int i = 0; i < numberOfRounds; i++)
            {
                foreach (Monkey monkey in monkeys)
                {
                    int monkeyTotalItems = monkey.CurrentItems.Count;
                    for (int j = 0; j < monkeyTotalItems; j++)
                    {
                        monkey.InspectionsCount++;
                        string itemBeforeOperation = monkey.CurrentItems.First().Item2;
                        Tuple<int, string, int, List<int>> newItem;
                        string newItemValue;
                        if (!decreaseWorryLevelAfterOperation)
                        {
                            newItem = new Tuple<int, string, int, List<int>>(monkey.CurrentItems[0].Item1, monkey.CurrentItems[0].Item2, monkey.CurrentItems[0].Item3, monkey.CurrentItems[0].Item4);
                            newItemValue = monkey.ApplyOperation(monkey.CurrentItems[0].Item2);
                            newItem = new Tuple<int, string,int, List<int>>(monkey.CurrentItems[0].Item1, newItemValue, monkey.CurrentItems[0].Item3, monkey.CurrentItems[0].Item4);
                        }
                        else
                        {
                            newItemValue = monkey.ApplyOperation(monkey.CurrentItems.First().Item2);
                            newItemValue = monkey.DecreaseWorryLevelByDivision(double.Parse(newItemValue)).ToString();
                            newItem = new Tuple<int, string,int, List<int>>(monkey.CurrentItems[0].Item1, newItemValue, monkey.CurrentItems[0].Item3, []);
                        }

                        monkey.CurrentItems.RemoveAt(0);
                        newItem.Item4.Add(monkey.Number);
                        if (monkey.Test(newItemValue))
                        {
                            monkeys.Single(m => m.Number == monkey.ReceiverMonkeyIfTestTrue).CurrentItems.Add(newItem);
                        }
                        else
                        {
                            monkeys.Single(m => m.Number == monkey.ReceiverMonkeyIfTestFalse).CurrentItems.Add(newItem);
                        }
                    }
                }

                foreach (Monkey monkey in monkeys)
                {
                    List<Tuple<int, string, int, List<int>>> newValues = [];
                    for(int j = 0; j < monkey.CurrentItems.Count; j++ )
                    {
                        var item = monkey.CurrentItems[j];
                        double itemValue = double.Parse(item.Item2) % 9699690;
                        item = new Tuple<int, string, int, List<int>>(item.Item1, itemValue.ToString(), item.Item3, item.Item4);
                        monkey.CurrentItems[j] = item;
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