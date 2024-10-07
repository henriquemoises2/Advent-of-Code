using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    public partial class Problem_2022_04 : Problem
    {
        private const string ElfsSectionsPattern = @"(?<elf1sectionstart>\d+)-(?<elf1sectionend>\d+),(?<elf2sectionstart>\d+)-(?<elf2sectionend>\d+)";


        public Problem_2022_04() : base()
        {
        }

        public override string Solve()
        {
            List<Tuple<Tuple<int, int>, Tuple<int, int>>> elfSections = [];

            try
            {
                Regex pattern = InputRegex();
                // TODO: Uniformise between the use of Match or Matches
                MatchCollection match = pattern.Matches(string.Join("/n", InputLines));
                for (int i = 0; i < match.Count; i++)
                {
                    Match elfSectionLineMatch = match[i];

                    int elf1SectionStart = int.Parse(elfSectionLineMatch.Groups["elf1sectionstart"].Value);
                    int elf1SectionEnd = int.Parse(elfSectionLineMatch.Groups["elf1sectionend"].Value);
                    Tuple<int, int> elf1Sections = new(elf1SectionStart, elf1SectionEnd);

                    int elf2SectionStart = int.Parse(elfSectionLineMatch.Groups["elf2sectionstart"].Value);
                    int elf2SectionEnd = int.Parse(elfSectionLineMatch.Groups["elf2sectionend"].Value);
                    Tuple<int, int> elf2Sections = new(elf2SectionStart, elf2SectionEnd);

                    elfSections.Add(new Tuple<Tuple<int, int>, Tuple<int, int>>(elf1Sections, elf2Sections));
                }
            }
            catch
            {
                throw new Exception("Invalid line in input.");
            }

            string part1 = SolvePart1(elfSections);
            string part2 = SolvePart2(elfSections);

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";

        }

        private static string SolvePart1(IEnumerable<Tuple<Tuple<int, int>, Tuple<int, int>>> elfSections)
        {
            try
            {
                int totalCount = 0;

                foreach (var elfSection in elfSections)
                {
                    Tuple<int, int> elf1 = elfSection.Item1;
                    Tuple<int, int> elf2 = elfSection.Item2;

                    if ((elf1.Item1 <= elf2.Item1 && elf1.Item2 >= elf2.Item2)
                        || elf2.Item1 <= elf1.Item1 && elf2.Item2 >= elf1.Item2)
                    {
                        totalCount++;
                    }
                }
                return totalCount.ToString();
            }
            catch
            {
                throw new Exception("Invalid line in input.");
            }
        }

        private static string SolvePart2(IEnumerable<Tuple<Tuple<int, int>, Tuple<int, int>>> elfSections)
        {
            try
            {
                int totalCount = 0;
                foreach (var elfSection in elfSections)
                {
                    Tuple<int, int> elf1 = elfSection.Item1;
                    Tuple<int, int> elf2 = elfSection.Item2;

                    if (!((elf1.Item1 < elf2.Item1 && elf1.Item2 < elf2.Item1) ||
                        (elf1.Item1 > elf2.Item2 && elf1.Item2 > elf2.Item2) ||
                        (elf2.Item1 < elf1.Item1 && elf2.Item2 < elf1.Item1) ||
                        (elf2.Item1 > elf1.Item2 && elf2.Item2 > elf2.Item2)))
                    {
                        totalCount++;
                    }
                }
                return totalCount.ToString();
            }
            catch
            {
                throw new Exception("Invalid line in input.");
            }
        }

        [GeneratedRegex(ElfsSectionsPattern, RegexOptions.Compiled)]
        private static partial Regex InputRegex();
    }
}
