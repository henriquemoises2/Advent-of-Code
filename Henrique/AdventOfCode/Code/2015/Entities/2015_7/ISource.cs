
namespace AdventOfCode._2015_7
{
    interface ISource
    {
        internal ushort Evaluate(IDictionary<string, ISource> circuit);
    }
}
