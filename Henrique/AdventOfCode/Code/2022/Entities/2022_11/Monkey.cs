namespace AdventOfCode.Code._2022.Entities._2022_11
{
    internal class Monkey : ICloneable
    {
        internal int Number { get; set; }
        internal List<ulong> Items { get; set; }
        internal Func<ulong, ulong> Operation { get; set; }
        internal Func<ulong, bool> Test { get; set; }
        internal int ReceiverMonkeyIfTestTrue { get; set; }
        internal int ReceiverMonkeyIfTestFalse { get; set; }
        internal int InspectionsCount { get; set; }
        internal int TestValue { get; set; }

        internal Monkey(int number, List<ulong> initialItems, char operation, string operationValue, int testValue, int receiverMonkeyIfTestTrue, int receiverMonkeyIfTestFalse)
        {
            Number = number;
            Items = initialItems;
            Operation = operation switch
            {
                '+' => (value) => value + (operationValue == "old" ? value : ulong.Parse(operationValue)),
                '*' => (value) => value * (operationValue == "old" ? value : ulong.Parse(operationValue)),
                _ => throw new Exception("Invalid line in input.")
            };
            TestValue = testValue;
            Test = (value) => value % (ulong)testValue == 0;
            ReceiverMonkeyIfTestTrue = receiverMonkeyIfTestTrue;
            ReceiverMonkeyIfTestFalse = receiverMonkeyIfTestFalse;
        }

        private Monkey(int number, List<ulong> items, Func<ulong, ulong> operation, Func<ulong, bool> test, int receiverMonkeyIfTestTrue, int receiverMonkeyIfTestFalse)
        {
            Number = number;
            Items = items;
            Operation = operation;
            Test = test;
            ReceiverMonkeyIfTestTrue = receiverMonkeyIfTestTrue;
            ReceiverMonkeyIfTestFalse = receiverMonkeyIfTestFalse;
        }

        internal ulong ApplyOperation(ulong value, bool decreaseWorryLevelAfterOperation)
        {
            value = Operation(value);
            if (decreaseWorryLevelAfterOperation)
            {
                value = (ulong)Math.Floor((double)value / 3);
            }
            return value;
        }

        public object Clone()
        {
            return new Monkey(Number, new List<ulong>(Items), Operation, Test, ReceiverMonkeyIfTestTrue, ReceiverMonkeyIfTestFalse);
        }
    }
}
