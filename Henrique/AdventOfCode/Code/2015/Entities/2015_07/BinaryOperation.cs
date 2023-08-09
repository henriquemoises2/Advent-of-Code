namespace AdventOfCode._2015_7
{
    internal enum BinaryOperationType
    {
        And = 1,
        Or = 2,
        LShift = 3,
        RShift = 4,
    }

    internal class BinaryOperation : ISource
    {
        private ISource _value1;
        private ISource _value2;

        private BinaryOperationType _operationType;

        internal BinaryOperation(ISource value1, ISource value2, BinaryOperationType operationType)
        {
            _value1 = value1;
            _value2 = value2;
            _operationType = operationType;
        }

        ushort ISource.Evaluate(IDictionary<string, ISource> circuit)
        {
            switch (_operationType)
            {
                case BinaryOperationType.And:
                    {
                        return (ushort)(_value1.Evaluate(circuit) & _value2.Evaluate(circuit));
                    };
                case BinaryOperationType.Or:
                    {
                        return (ushort)(_value1.Evaluate(circuit) | _value2.Evaluate(circuit));
                    };
                case BinaryOperationType.LShift:
                    {
                        return (ushort)(_value1.Evaluate(circuit) << _value2.Evaluate(circuit));
                    };
                case BinaryOperationType.RShift:
                    {
                        return (ushort)(_value1.Evaluate(circuit) >> _value2.Evaluate(circuit));
                    };
                default:
                    throw new Exception("Invalid operation.");
            }
        }
    }
}
