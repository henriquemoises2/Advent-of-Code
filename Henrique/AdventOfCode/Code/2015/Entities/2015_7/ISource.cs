
namespace AdventOfCode._2015_7
{
    interface ISource
    {
        public ushort Evaluate(IDictionary<string, ISource> circuit);
    }
}
