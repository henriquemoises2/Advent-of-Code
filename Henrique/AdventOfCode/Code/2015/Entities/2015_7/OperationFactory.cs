
namespace AdventOfCode._2015_7
{
    internal class OperationFactory
    {
        private IDictionary<string, ISource> _circuit;

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
            Value signalValue = new Value(signal);
            if(!_circuit.TryAdd(destinationCableName, signalValue))
            {
                throw new Exception("Cable already has a different source.");
            }
        }

        internal void AddCableUnion(string originCableName, string destinationCableName)
        {
            Cable originCable = new Cable(originCableName);
            if (!_circuit.TryAdd(destinationCableName, originCable))
            {
                throw new Exception("Cable already has a different source.");
            }
        }

        internal void AddUnaryOperation(ushort signal, string operation ,string destinationCableName)
        {
            Value signalValue = new Value(signal);

            if(!Enum.TryParse(operation, ignoreCase:true, out UnaryOperationType operationType))
            {
                throw new Exception("Invalid operation.");
            }
            UnaryOperation parsedOperation = new UnaryOperation(signalValue, operationType);
            if (!_circuit.TryAdd(destinationCableName, parsedOperation))
            {
                throw new Exception("Cable already has a different source.");
            }
        }

        internal void AddUnaryOperation(string originCableName, string operation, string destinationCableName)
        {
            Cable originCable = new Cable(originCableName);

            if (!Enum.TryParse(operation, ignoreCase: true, out UnaryOperationType operationType))
            {
                throw new Exception("Invalid operation.");
            }
            UnaryOperation parsedOperation = new UnaryOperation(originCable, operationType);
            if (!_circuit.TryAdd(destinationCableName, parsedOperation))
            {
                throw new Exception("Cable already has a different source.");
            }
        }

        internal void AddBinaryOperation(ushort signal1, ushort signal2, string operation, string destinationCableName)
        {
            Value signalValue1 = new Value(signal1);
            Value signalValue2 = new Value(signal2);

            if (!Enum.TryParse(operation, ignoreCase: true, out BinaryOperationType operationType))
            {
                throw new Exception("Invalid operation.");
            }
            BinaryOperation parsedOperation = new BinaryOperation(signalValue1, signalValue2, operationType);
            if (!_circuit.TryAdd(destinationCableName, parsedOperation))
            {
                throw new Exception("Cable already has a different source.");
            }
        }

        internal void AddBinaryOperation(string originCableName, ushort signal, string operation, string destinationCableName)
        {
            Cable originCable = new Cable(originCableName);
            Value signalValue = new Value(signal);

            if (!Enum.TryParse(operation, ignoreCase: true, out BinaryOperationType operationType))
            {
                throw new Exception("Invalid operation.");
            }
            BinaryOperation parsedOperation = new BinaryOperation(originCable, signalValue, operationType);
            if (!_circuit.TryAdd(destinationCableName, parsedOperation))
            {
                throw new Exception("Cable already has a different source.");
            }
        }

        internal void AddBinaryOperation(ushort signal, string originCableName,  string operation, string destinationCableName)
        {

            Value signalValue = new Value(signal);
            Cable originCable = new Cable(originCableName);

            if (!Enum.TryParse(operation, ignoreCase: true, out BinaryOperationType operationType))
            {
                throw new Exception("Invalid operation.");
            }
            BinaryOperation parsedOperation = new BinaryOperation(signalValue, originCable, operationType);
            if (!_circuit.TryAdd(destinationCableName, parsedOperation))
            {
                throw new Exception("Cable already has a different source.");
            }
        }

        internal void AddBinaryOperation(string originCableName1, string originCableName2, string operation, string destinationCableName)
        {
            Cable originCable1 = new Cable(originCableName1);
            Cable originCable2 = new Cable(originCableName2);

            if (!Enum.TryParse(operation, ignoreCase: true, out BinaryOperationType operationType))
            {
                throw new Exception("Invalid operation.");
            }
            BinaryOperation parsedOperation = new BinaryOperation(originCable1, originCable2, operationType);
            if (!_circuit.TryAdd(destinationCableName, parsedOperation))
            {
                throw new Exception("Cable already has a different source.");
            }
        }

    }
}
