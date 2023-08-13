using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    public class Problem_2015_08 : Problem
    {

        private const string PatternNonLetterCharacters = @"(\\\\)|(\\"")|(\\x[0-f][0-f])|([a-z])";

        public Problem_2015_08() : base()
        {

        }

        public override string Solve()
        {
            int totalCodeCharacters = 0;
            int totalEncodedCharacters = 0;
            int totalInMemoryCharacters = 0;

            Regex regexNonLetterCharacters = new Regex(PatternNonLetterCharacters, RegexOptions.Compiled);
            MatchCollection matchCollection;
            foreach (string line in InputLines)
            {
                totalCodeCharacters += line.Length;
                matchCollection = regexNonLetterCharacters.Matches(line);
                totalInMemoryCharacters += matchCollection.Count();
                totalEncodedCharacters += EncodeString(line).Length;
            }
            string part1 = (totalCodeCharacters - totalInMemoryCharacters).ToString();

            string part2 = (totalEncodedCharacters - totalCodeCharacters).ToString();

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";

        }

        private string EncodeString(string decodedString)
        {
            string encodedString = decodedString.Replace(@"\", @"\\").Replace(@"""", @"\""");
            return @"""" + encodedString + @"""";
        }
    }
}
