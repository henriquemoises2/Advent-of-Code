namespace AdventOfCode._2015_7
{
    internal class Cable : ISource
    {
        private string _cableName;

        internal Cable(string cableName)
        {
            _cableName = cableName;
        }

        public ushort Evaluate(IDictionary<string, ISource> circuit)
        {
            return circuit[_cableName].Evaluate(circuit);
        }
    }
}
