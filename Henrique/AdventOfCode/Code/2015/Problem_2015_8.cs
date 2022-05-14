using System.Text.RegularExpressions;
using System.Linq;
using AdventOfCode._2015_7;

namespace AdventOfCode.Code
{
    internal class Problem_2015_8 : Problem
    {

        private const string PatternNonLetterCharacters = @"(\\\\)|(\\"")|(\\x[0-f][0-f])|([a-z])";

        internal Problem_2015_8() : base()
        {

        }

        internal override string Solve()
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

            return $"Part 1 solution: " + part1 + "\n"
                + "Part 2 solution: " + part2;
        }

        private string EncodeString(string decodedString)
        {
            string encodedString = decodedString.Replace(@"\", @"\\").Replace(@"""", @"\""");
            return @"""" + encodedString + @"""";
        }
    }
}
