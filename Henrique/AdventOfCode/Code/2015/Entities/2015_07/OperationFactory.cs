namespace AdventOfCode._2015_7
{
    internal class OperationFactory
    {
        private readonly IDictionary<string, ISource> _circuit;

        internal OperationFactory(IDictionary<String, ISource> circuit)
        {
            _circuit = circuit;
        }

        internal IDictionary<string, ISource> GetCircuit()
        {
            return _circuit;
        }

        internal void AddSignal(ushort signal, string destinationCableName)
        {
            Value signalValue = new(signal);
            if (!_circuit.TryAdd(destinationCableName, signalValue))
            {
                throw new Exception("Cable already has a different source.");
            }
        }

        internal void AddCableUnion(string originCableName, string destinationCableName)
        {
            Cable originCable = new(originCableName);
            if (!_circuit.TryAdd(destinationCableName, originCable))
            {
                throw new Exception("Cable already has a different source.");
            }
        }

        internal void AddUnaryOperation(ushort signal, string operation, string destinationCableName)
        {
            Value signalValue = new(signal);

            if (!Enum.TryParse(operation, ignoreCase: true, out UnaryOperationType operationType))
            {
                throw new Exception("Invalid operation.");
            }
            UnaryOperation parsedOperation = new(signalValue, operationType);
            if (!_circuit.TryAdd(destinationCableName, parsedOperation))
            {
                throw new Exception("Cable already has a different source.");
            }
        }

        internal void AddUnaryOperation(string originCableName, string operation, string destinationCableName)
        {
            Cable originCable = new(originCableName);

            if (!Enum.TryParse(operation, ignoreCase: true, out UnaryOperationType operationType))
            {
                throw new Exception("Invalid operation.");
            }
            UnaryOperation parsedOperation = new(originCable, operationType);
            if (!_circuit.TryAdd(destinationCableName, parsedOperation))
            {
                throw new Exception("Cable already has a different source.");
            }
        }

        internal void AddBinaryOperation(ushort signal1, ushort signal2, string operation, string destinationCableName)
        {
            Value signalValue1 = new(signal1);
            Value signalValue2 = new(signal2);

            if (!Enum.TryParse(operation, ignoreCase: true, out BinaryOperationType operationType))
            {
                throw new Exception("Invalid operation.");
            }
            BinaryOperation parsedOperation = new(signalValue1, signalValue2, operationType);
            if (!_circuit.TryAdd(destinationCableName, parsedOperation))
            {
                throw new Exception("Cable already has a different source.");
            }
        }

        internal void AddBinaryOperation(string originCableName, ushort signal, string operation, string destinationCableName)
        {
            Cable originCable = new(originCableName);
            Value signalValue = new(signal);

            if (!Enum.TryParse(operation, ignoreCase: true, out BinaryOperationType operationType))
            {
                throw new Exception("Invalid operation.");
            }
            BinaryOperation parsedOperation = new(originCable, signalValue, operationType);
            if (!_circuit.TryAdd(destinationCableName, parsedOperation))
            {
                throw new Exception("Cable already has a different source.");
            }
        }

        internal void AddBinaryOperation(ushort signal, string originCableName, string operation, string destinationCableName)
        {

            Value signalValue = new(signal);
            Cable originCable = new(originCableName);

            if (!Enum.TryParse(operation, ignoreCase: true, out BinaryOperationType operationType))
            {
                throw new Exception("Invalid operation.");
            }
            BinaryOperation parsedOperation = new(signalValue, originCable, operationType);
            if (!_circuit.TryAdd(destinationCableName, parsedOperation))
            {
                throw new Exception("Cable already has a different source.");
            }
        }

        internal void AddBinaryOperation(string originCableName1, string originCableName2, string operation, string destinationCableName)
        {
            Cable originCable1 = new(originCableName1);
            Cable originCable2 = new(originCableName2);

            if (!Enum.TryParse(operation, ignoreCase: true, out BinaryOperationType operationType))
            {
                throw new Exception("Invalid operation.");
            }
            BinaryOperation parsedOperation = new(originCable1, originCable2, operationType);
            if (!_circuit.TryAdd(destinationCableName, parsedOperation))
            {
                throw new Exception("Cable already has a different source.");
            }
        }

    }
}
