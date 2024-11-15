namespace AdventOfCode.Code._2022.Entities._2022_10
{
    internal class Operation
    {
        internal OperationType Type { get; set; }
        internal int? Value { get; set; }
        internal int PendingCycles { get; set; }

        internal Operation(string operationName, int? value)
        {
            Type = operationName switch
            {
                "noop" => OperationType.Noop,
                "addx" => OperationType.Addx,
                _ => throw new Exception("Invalid line in input.")
            };
            Value = value;
            PendingCycles = operationName switch
            {
                "noop" => 1,
                "addx" => 2,
                _ => throw new Exception("Invalid line in input.")
            };
        }
    }

    internal enum OperationType
    {
        Noop = 0,
        Addx = 1
    }
}
