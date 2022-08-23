namespace AdventOfCode.Code._2015_13
{
    internal class DispositionChange
    {
        internal string Person1 { get; set; }
        internal string Person2 { get; set; }
        internal int Signal { get; set; }
        internal int Value { get; set; }

        public DispositionChange(string person1, string person2, int signal, int value)
        {
            Person1 = person1;
            Person2 = person2;
            Signal = signal;
            Value = value;
        }
    }
}
