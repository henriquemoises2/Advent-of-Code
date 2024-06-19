namespace AdventOfCode.Code
{
    public class Problem_2022_06 : Problem
    {
        private const int StartOfPacketLength = 4;
        private const int StartOfMessageLength = 14;


        public Problem_2022_06() : base()
        { }

        public override string Solve()
        {
            string part1 = SolvePart1(InputFirstLine);
            string part2 = SolvePart2(InputFirstLine);

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";
        }

        private string SolvePart1(string buffer)
        {
            int index = 0;
            do
            {
                string sample = buffer.Substring(index, StartOfPacketLength);
                if(sample.Distinct().Count() == StartOfPacketLength)
                {
                    return (index + StartOfPacketLength).ToString();
                }
                index++;
            }
            while (index + 4 < buffer.Length);
            throw new Exception("Unable to find start-of-packet"); 
        }

        private string SolvePart2(string buffer)
        {
            int index = 0;
            do
            {
                string sample = buffer.Substring(index, StartOfMessageLength);
                if (sample.Distinct().Count() == StartOfMessageLength)
                {
                    return (index + StartOfMessageLength).ToString();
                }
                index++;
            }
            while (index + StartOfMessageLength < buffer.Length);
            throw new Exception("Unable to find start-of-message");
        }
    }
}