namespace AdventOfCode._2015_7
{
    internal class Cable : ISource
    {
        private string _cableName;

        internal Cable(string cableName)
        {
            _cableName = cableName;
        }

        ushort ISource.Evaluate(IDictionary<string, ISource> circuit)
        {
            ushort result = circuit[_cableName].Evaluate(circuit);
            circuit[_cableName] = new Value(result);
            return result;
        }
    }
}
