namespace AdventOfCode._2015_7
{
    internal class UnaryOperation : ISource
    {
        private ISource _value;
        private UnaryOperationType _operationType;

        internal UnaryOperation(ISource value, UnaryOperationType operationType)
        {
            _value = value;
            _operationType = operationType;
        }

        public ushort Evaluate(IDictionary<string, ISource> circuit)
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

    internal enum UnaryOperationType
    {
        Not = 1
    }
}
