using System.Text;

namespace AdventOfCode.Code
{
    public class Problem_2015_10 : Problem
    {
        private const int SequenceIterations = 40;
        private const int SequenceExtraIterations = 10;

        public Problem_2015_10() : base()
        {
        }

        public override string Solve()
        {
            string part1 = SolvePart1();
            string part2 = SolvePart2(part1);

            return $"Part 1 solution: " + part1.Length.ToString() + "\n"
                + "Part 2 solution: " + part2.Length.ToString();
        }

        private string SolvePart1()
        {
            string sequence = InputFirstLine;

            // Generate the sequence 40 times
            for (int i = 0; i < SequenceIterations; i++)
            {
                sequence = LookAndSay(sequence);
            }

            return sequence;
        }

        private string SolvePart2(string sequence)
        {
            // Generate the sequence 10 more times for a total of 50 times
            for (int i = 0; i < SequenceExtraIterations; i++)
            {
                sequence = LookAndSay(sequence);
            }

            return sequence;
        }

        private string LookAndSay(string input)
        {
            StringBuilder sb = new StringBuilder();

            int currentNumber = int.Parse(input[0].ToString());
            int numberOfOccurences = 1;
            int nextNumber = 0;

            for (int i = 1; i < input.Length; i++)
            {
                nextNumber = int.Parse(input[i].ToString());
                if (nextNumber == currentNumber)
                {
                    numberOfOccurences++;
                }
                else
                {
                    sb.Append(numberOfOccurences);
                    sb.Append(currentNumber);
                    currentNumber = nextNumber;
                    numberOfOccurences = 1;
                }
            }
            // Append last digit because it is not caught in the cycle
            sb.Append(numberOfOccurences);
            sb.Append(nextNumber);
            return sb.ToString();
        }

    }
}
