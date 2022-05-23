namespace AdventOfCode._2015_7
{
    internal class Value : ISource
    {
        private ushort _signal;

        internal Value(ushort value)
        {
            _signal = value;
        }

        ushort ISource.Evaluate(IDictionary<string, ISource> circuit)
        {
            return _signal;
        }
    }
}
