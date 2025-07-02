namespace AdventOfCode.Code._2022.Entities._2022_11
{
    internal class Monkey
    {
        internal int Number { get; set; }
        internal List<long> Items { get; set; } = [];
        internal Func<long, long> Operation { get; set; }
        internal Func<long, bool> Test { get; set; }
        internal int ReceiverMonkeyIfTestTrue { get; set; }
        internal int ReceiverMonkeyIfTestFalse { get; set; }
        internal int InspectionsCount { get; set; }
        internal int TestValue { get; set; }

        internal Monkey(int? number, List<long> initialItems, char? operation, string? operationValue, int? testValue, int? receiverMonkeyIfTestTrue, int? receiverMonkeyIfTestFalse)
        {
            if (number == null ||
                operation == null ||
                string.IsNullOrWhiteSpace(operationValue) ||
                testValue == null ||
                receiverMonkeyIfTestTrue == null ||
                receiverMonkeyIfTestFalse == null
                )
            {
                throw new Exception("Invalid line in input.");
            }

            Number = number.Value;
            Items = [.. initialItems];
            Operation = operation.Value switch
            {
                '+' => (value) => value + (operationValue == "old" ? value : long.Parse(operationValue)),
                '*' => (value) => value * (operationValue == "old" ? value : long.Parse(operationValue)),
                _ => throw new Exception("Invalid line in input.")
            };
            TestValue = testValue.Value;
            Test = (value) => value % TestValue == 0;
            ReceiverMonkeyIfTestTrue = receiverMonkeyIfTestTrue.Value;
            ReceiverMonkeyIfTestFalse = receiverMonkeyIfTestFalse.Value;
        }

        internal long ApplyOperation(long value)
        {
            value = Operation(value);
            return value;
        }

        internal static long DecreaseWorryValue(long value)
        {
            value = (long)Math.Floor((decimal)value / 3);
            return value;
        }
    }
}
