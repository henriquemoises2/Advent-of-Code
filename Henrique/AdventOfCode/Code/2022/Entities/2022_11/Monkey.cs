using AdventOfCode._2015_7;
using AdventOfCode.Helpers;

namespace AdventOfCode.Code._2022.Entities._2022_11
{
    internal class Monkey : ICloneable
    {
        internal int Number { get; set; }
        internal List<Tuple<int, string, int, List<int>>> CurrentItems { get; set; } = [];
        internal List<Tuple<int, string, int, List<int>>> InitialItems { get; set; } = [];

        internal Func<string, string> Operation { get; set; }
        internal Func<string, bool> Test { get; set; }
        internal int ReceiverMonkeyIfTestTrue { get; set; }
        internal int ReceiverMonkeyIfTestFalse { get; set; }
        internal int InspectionsCount { get; set; }
        internal int TestValue { get; set; }

        internal Monkey(int number, List<Tuple<int, string, List<int>>> initialItems, char operation, string operationValue, int testValue, int receiverMonkeyIfTestTrue, int receiverMonkeyIfTestFalse)
        {
            Number = number;
            CurrentItems = initialItems.Select(x => new Tuple <int, string, int, List<int>>(x.Item1, x.Item2, int.Parse(x.Item2), x.Item3)).ToList();
            InitialItems = initialItems.Select(x => new Tuple<int, string, int, List<int>>(x.Item1, x.Item2, int.Parse(x.Item2), x.Item3)).ToList();
            Operation = operation switch
            {
                '+' => (value) => MathOperations.SumStrings(value.ToString(), (operationValue == "old" ? value.ToString() : operationValue)),
                '*' => (value) => MathOperations.MultiplyStrings(value.ToString(), (operationValue == "old" ? value.ToString() : operationValue)),
                _ => throw new Exception("Invalid line in input.")
            };
            TestValue = testValue;
            Test = (value) => MathOperations.ModuloOfString(value, testValue) == 0;
            ReceiverMonkeyIfTestTrue = receiverMonkeyIfTestTrue;
            ReceiverMonkeyIfTestFalse = receiverMonkeyIfTestFalse;
        }

        private Monkey(int number, List<Tuple<int, string, int, List<int>>> initialItems, Func<string, string> operation, Func<string, bool> test, int receiverMonkeyIfTestTrue, int receiverMonkeyIfTestFalse)
        {
            Number = number;
            CurrentItems = initialItems.Select(x => new Tuple<int, string, int, List<int>>(x.Item1, x.Item2, x.Item3, x.Item4)).ToList();
            Operation = operation;
            Test = test;
            ReceiverMonkeyIfTestTrue = receiverMonkeyIfTestTrue;
            ReceiverMonkeyIfTestFalse = receiverMonkeyIfTestFalse;
        }

        internal string ApplyOperation(string value)
        {
            value = Operation(value);
            return value;
        }

        internal double DecreaseWorryLevelByDivision(double value)
        {
            value = (double)Math.Floor((decimal)value / 3);
            return value;
        }

        internal Tuple<int, string, int, List<int>> DecreaseWorryLevelByLookup(Tuple<int, string, int, List<int>> newItem)
        {
            if (MathOperations.ModuloOfString(newItem.Item2, newItem.Item3) == 0 && MathOperations.ModuloOfString(newItem.Item2, TestValue) == 0)
            {
                newItem = new Tuple<int, string, int, List<int>>(newItem.Item1, newItem.Item3.ToString(), newItem.Item3, newItem.Item4);
            }
            return newItem;
        }

        public object Clone()
        {
            Monkey cloneMonkey = new Monkey(Number, new List<Tuple<int, string, int, List<int>>>(CurrentItems), Operation, Test, ReceiverMonkeyIfTestTrue, ReceiverMonkeyIfTestFalse);
            cloneMonkey.TestValue = TestValue;
            return cloneMonkey;
        }
    }
}
