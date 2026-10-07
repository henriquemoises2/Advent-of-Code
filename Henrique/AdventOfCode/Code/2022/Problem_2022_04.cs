using AdventOfCode.Constants;
using AdventOfCode.DataStructures;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code;

public partial class Problem_2022_04 : Problem
{
    private const string ElfsSectionsPattern = @"(?<elf1sectionstart>\d+)-(?<elf1sectionend>\d+),(?<elf2sectionstart>\d+)-(?<elf2sectionend>\d+)";


    public Problem_2022_04() : base()
    {
    }

    public override string Solve()
    {
        List<Tuple<Coordinates, Coordinates>> elfSections = [];

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
                Coordinates elf1Sections = new(elf1SectionStart, elf1SectionEnd);

                int elf2SectionStart = int.Parse(elfSectionLineMatch.Groups["elf2sectionstart"].Value);
                int elf2SectionEnd = int.Parse(elfSectionLineMatch.Groups["elf2sectionend"].Value);
                Coordinates elf2Sections = new(elf2SectionStart, elf2SectionEnd);

                elfSections.Add(new Tuple<Coordinates, Coordinates>(elf1Sections, elf2Sections));
            }
        }
        catch
        {
            throw new Exception(Messages.InvalidInputErrorMessage);
        }

        string part1 = SolvePart1(elfSections);
        string part2 = SolvePart2(elfSections);

        return string.Format(SolutionFormat, part1, part2);

    }

    private static string SolvePart1(IEnumerable<Tuple<Coordinates, Coordinates>> elfSections)
    {
        try
        {
            int totalCount = 0;

            foreach (var elfSection in elfSections)
            {
                Coordinates elf1 = elfSection.Item1;
                Coordinates elf2 = elfSection.Item2;

                if ((elf1.X <= elf2.X && elf1.Y >= elf2.Y)
                    || elf2.X <= elf1.X && elf2.Y >= elf1.Y)
                {
                    totalCount++;
                }
            }
            return totalCount.ToString();
        }
        catch
        {
            throw new Exception(Messages.InvalidInputErrorMessage);
        }
    }

    private static string SolvePart2(IEnumerable<Tuple<Coordinates, Coordinates>> elfSections)
    {
        try
        {
            int totalCount = 0;
            foreach (var elfSection in elfSections)
            {
                Coordinates elf1 = elfSection.Item1;
                Coordinates elf2 = elfSection.Item2;

                if (!((elf1.X < elf2.X && elf1.Y < elf2.X) ||
                    (elf1.X > elf2.Y && elf1.Y > elf2.Y) ||
                    (elf2.X < elf1.X && elf2.Y < elf1.X) ||
                    (elf2.X > elf1.Y && elf1.Y > elf2.Y)))
                {
                    totalCount++;
                }
            }
            return totalCount.ToString();
        }
        catch
        {
            throw new Exception(Messages.InvalidInputErrorMessage);
        }
    }

    [GeneratedRegex(ElfsSectionsPattern, RegexOptions.Compiled)]
    private static partial Regex InputRegex();
}
