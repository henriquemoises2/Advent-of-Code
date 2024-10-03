namespace AdventOfCode._2015_23
{
    public class Register(char id, int storedValue = 0)
    {
        internal char Id { get; set; } = id;
        internal int StoredValue { get; set; } = storedValue;
    }
}
