namespace AdventOfCode.Code._2022.Entities._2022_11
{
    internal class MonkeyInputParameters
    {
        internal int Number { get; set; }
        internal List<long> Items { get; set; } = [];
        internal char? Operation { get; set; }
        internal string? OperationValue { get; set; }
        internal int? ConditionTest { get; set; }
        internal string? ConditionValue { get; set; }
        internal int? ConditionMonkeyReceiverIfTrue { get; set; }
        internal int? ConditionMonkeyReceiverIfFalse { get; set; }

        internal void Reset()
        {
            Items = [];
            Operation = null;
            OperationValue = null;
            ConditionTest = null;
            ConditionValue = null;
            ConditionMonkeyReceiverIfTrue = null;
            ConditionMonkeyReceiverIfFalse = null;

        }

    }
}
