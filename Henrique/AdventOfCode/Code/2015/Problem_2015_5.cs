using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    internal class Problem_2015_5 : Problem
    {
        private const string RULE_1 = "^(.*a.*|.*e.*|.*i.*|.*o.*|.*u.*){3,}$";
        private const string RULE_2 = "^.*(?<letter>[a-z])\\k<letter>.*$";
        private const string RULE_3 = "^((?!ab|cd|pq|xy).)+$";
        private const string RULE_4 = "^.*(?<sequence>[a-z][a-z]).*\\k<sequence>.*$";
        private const string RULE_5 = "^.*(?<letter>[a-z])[a-z]\\k<letter>.*$";


        internal Problem_2015_5() : base()
        {
        }

        internal override string Solve()
        {
            string part1 = SolvePart1();
            string part2 = SolvePart2();

            return $"Part 1 solution: " + part1 + "\n"
                + "Part 2 solution: " + part2;
        }

        private string SolvePart1()
        {
            Regex expression1 = new Regex(RULE_1, RegexOptions.Compiled);
            Regex expression2 = new Regex(RULE_2, RegexOptions.Compiled);
            Regex expression3 = new Regex(RULE_3, RegexOptions.Compiled);

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
            Regex expression4 = new Regex(RULE_4, RegexOptions.Compiled);
            Regex expression5 = new Regex(RULE_5, RegexOptions.Compiled);

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
    }
}
