using AdventOfCode.Constants;
using AdventOfCode.Helpers;

namespace AdventOfCode.Code;

public class Problem_2015_17 : Problem
{
    private const int ProblemTotalEggnogLitres = 150;
    private const int ExampleTotalEggnogLitres = 25;
    private static int EggnogLitres = ProblemTotalEggnogLitres;

    public Problem_2015_17() : base()
    { }

    public override string Solve()
    {
        List<int> ContainerSizes = [];
        try
        {
            ContainerSizes.AddRange(InputLines.Select(line => int.Parse(line)));
        }
        catch
        {
            throw new Exception(Messages.InvalidInputErrorMessage);
        }

        var allContainerPossibilities = SetsGenerator<int>.GenerateAllIntSetsWithLimit(InputLines.Count(), ContainerSizes, EggnogLitres);

        string part1 = SolvePart1(allContainerPossibilities);
        string part2 = SolvePart2(allContainerPossibilities);

        return string.Format(SolutionFormat, part1, part2);

    }

    public override List<string> SolveExamples()
    {
        EggnogLitres = ExampleTotalEggnogLitres;
        List<string> result = base.SolveExamples();
        EggnogLitres = ProblemTotalEggnogLitres;
        return result;
    }

    private static string SolvePart1(IEnumerable<IEnumerable<int>> allContainerPossibilities)
    {
        // Check which container sets can be completely filled with EggnogLitres
        IEnumerable<IEnumerable<int>> realContainerPossibilities = allContainerPossibilities.Where(set => set.Sum() == EggnogLitres);
        return realContainerPossibilities.Count().ToString();
    }

    private static string SolvePart2(IEnumerable<IEnumerable<int>> allContainerPossibilities)
    {
        // Check which container sets can be completely filled with EggnogLitres
        IEnumerable<IEnumerable<int>> realContainerPossibilities = allContainerPossibilities.Where(set => set.Sum() == EggnogLitres);
        // Group all possibilities by the set size, i.e. how many containers are used to fill EggnogLitres 
        IEnumerable<IGrouping<int, IEnumerable<int>>> groupedContainerPossibilities = realContainerPossibilities.GroupBy(set => set.Count());
        // Select first group element, i.e. the grouping containing the set size and the associated sets. Then count all sets.
        return groupedContainerPossibilities.First().Count().ToString();
    }
}