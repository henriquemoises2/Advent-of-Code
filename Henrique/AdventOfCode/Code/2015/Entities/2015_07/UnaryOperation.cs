namespace AdventOfCode._2015_7
{
    internal enum UnaryOperationType
    {
        Not = 1
    }

    internal class UnaryOperation : ISource
    {
        private ISource _value;
        private UnaryOperationType _operationType;

        internal UnaryOperation(ISource value, UnaryOperationType operationType)
        {
            _value = value;
            _operationType = operationType;
        }

        ushort ISource.Evaluate(IDictionary<string, ISource> circuit)
        {
            switch (_operationType)
            {
                case UnaryOperationType.Not:
                    {
                        return (ushort)~_value.Evaluate(circuit);
                    };
                default:
                    throw new Exception("Invalid operation.");
            }
        }
    }
}
