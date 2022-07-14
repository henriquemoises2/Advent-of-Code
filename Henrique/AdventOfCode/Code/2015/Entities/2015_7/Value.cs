namespace AdventOfCode._2015_7
{
    internal class Value : ISource
    {
        private ushort _signal;

        internal Value(ushort value)
        {
            _signal = value;
        }

        public ushort Evaluate(IDictionary<string, ISource> circuit)
        {
            return _signal;
        }
    }
}
