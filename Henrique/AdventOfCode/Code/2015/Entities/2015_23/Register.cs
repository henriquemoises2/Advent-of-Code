namespace AdventOfCode._2015_23
{
    public class Register
    {
        public Register(char id, int storedValue = 0)
        {
            Id = id;
            StoredValue = storedValue;
        }

        internal char Id { get; set; }
        internal int StoredValue { get; set; }
    }
}
