namespace AdventOfCode.Code._2022.Entities._2022_11
{
    internal class Monkey
    {
        internal int Number { get; set; }
        internal List<long> CurrentItems { get; set; } = [];
        internal Func<long, long> Operation { get; set; }
        internal Func<long, bool> Test { get; set; }
        internal int ReceiverMonkeyIfTestTrue { get; set; }
        internal int ReceiverMonkeyIfTestFalse { get; set; }
        internal int InspectionsCount { get; set; }

        internal Monkey(int number, List<long> initialItems, char operation, string operationValue, int testValue, int receiverMonkeyIfTestTrue, int receiverMonkeyIfTestFalse)
        {
            Number = number;
            CurrentItems = initialItems;
            Operation = operation switch
            {
                '+' => (value) => value + (operationValue == "old" ? value : long.Parse(operationValue)),
                '*' => (value) => value * (operationValue == "old" ? value : long.Parse(operationValue)),
                _ => throw new Exception("Invalid line in input.")
            };
            Test = (value) => value % testValue == 0;
            ReceiverMonkeyIfTestTrue = receiverMonkeyIfTestTrue;
            ReceiverMonkeyIfTestFalse = receiverMonkeyIfTestFalse;
        }

        internal long ApplyOperation(long value)
        {
            value = Operation(value);
            return value;
        }

        internal long DecreaseWorryLevelByDivision(long value)
        {
            value = (long)Math.Floor((decimal)value / 3);
            return value;
        }
    }
}
