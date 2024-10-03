using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    public partial class Problem_2015_05 : Problem
    {
        private const string RULE_1 = @"^(.*a.*|.*e.*|.*i.*|.*o.*|.*u.*){3,}$";
        private const string RULE_2 = @$"^.*(?<letter>[a-z])\k<letter>.*$";
        private const string RULE_3 = @"^((?!ab|cd|pq|xy).)+$";
        private const string RULE_4 = @"^.*(?<sequence>[a-z][a-z]).*\k<sequence>.*$";
        private const string RULE_5 = @"^.*(?<letter>[a-z])[a-z]\k<letter>.*$";


        public Problem_2015_05() : base()
        {
        }

        public override string Solve()
        {
            string part1 = SolvePart1();
            string part2 = SolvePart2();

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";

        }

        private string SolvePart1()
        {
            Regex expression1 = MyRegex();
            Regex expression2 = MyRegex1();
            Regex expression3 = MyRegex2();

            int niceSentences = 0;
            int naughtySentences = 0;

            foreach (string line in InputLines)
            {
                if (expression1.IsMatch(line) && expression2.IsMatch(line) && expression3.IsMatch(line))
                {
                    niceSentences++;
                }
                else
                {
                    naughtySentences++;
                }
            }

            return niceSentences.ToString();
        }

        private string SolvePart2()
        {
            Regex expression4 = new(RULE_4, RegexOptions.Compiled);
            Regex expression5 = new(RULE_5, RegexOptions.Compiled);

            int niceSentences = 0;
            int naughtySentences = 0;

            foreach (string line in InputLines)
            {
                if (expression4.IsMatch(line) && expression5.IsMatch(line))
                {
                    niceSentences++;
                }
                else
                {
                    naughtySentences++;
                }
            }

            return niceSentences.ToString();
        }

        [GeneratedRegex(RULE_1, RegexOptions.Compiled)]
        private static partial Regex MyRegex();
        [GeneratedRegex(RULE_2, RegexOptions.Compiled)]
        private static partial Regex MyRegex1();
        [GeneratedRegex(RULE_3, RegexOptions.Compiled)]
        private static partial Regex MyRegex2();
    }
}
