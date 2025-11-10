using AdventOfCode.Constants;

namespace AdventOfCode.Tests;

public class Problems_2015 : ProblemsBase
{
    [Fact]
    public void Test_2015_01()
    {
        List<string> expectedExampleSolutions =
        [
            string.Format(Messages.ProblemSolutionFormat, 0, "Solution not found."),
            string.Format(Messages.ProblemSolutionFormat, 0, "Solution not found."),
            string.Format(Messages.ProblemSolutionFormat, 3, "Solution not found."),
            string.Format(Messages.ProblemSolutionFormat, 3, "Solution not found."),
            string.Format(Messages.ProblemSolutionFormat, 3, 1),
            string.Format(Messages.ProblemSolutionFormat, -1, 3),
            string.Format(Messages.ProblemSolutionFormat, -1, 1),
            string.Format(Messages.ProblemSolutionFormat, -3, 1),
            string.Format(Messages.ProblemSolutionFormat, -3, 1),
            string.Format(Messages.ProblemSolutionFormat, -1, 1),
            string.Format(Messages.ProblemSolutionFormat, -1, 5)
        ];
        string expectedSolution = string.Format(Messages.ProblemSolutionFormat, 138, 1771);

        Problem problemSolver = new Problem_2015_01();
        RunAndValidateAll(problemSolver, expectedExampleSolutions, expectedSolution);

    }
    
    [Fact]
    public void Test_2015_02()
    {
        List<string> expectedExampleSolutions =
        [
            string.Format(Messages.ProblemSolutionFormat, 58, 34),
            string.Format(Messages.ProblemSolutionFormat, 43, 14)
        ];
        string expectedSolution = string.Format(Messages.ProblemSolutionFormat, 1598415, 3812909);

        Problem problemSolver = new Problem_2015_02();
        RunAndValidateAll(problemSolver, expectedExampleSolutions, expectedSolution);
    }

    [Fact]
    public void Test_2015_03()
    {
        List<string> expectedExampleSolutions =
        [
            string.Format(Messages.ProblemSolutionFormat, 2, 2),
            string.Format(Messages.ProblemSolutionFormat, 4, 3),
            string.Format(Messages.ProblemSolutionFormat, 2, 11),
            string.Format(Messages.ProblemSolutionFormat, 2, 3)
        ];
        string expectedSolution = string.Format(Messages.ProblemSolutionFormat, 2572, 2631);

        Problem problemSolver = new Problem_2015_03();
        RunAndValidateAll(problemSolver, expectedExampleSolutions, expectedSolution);
    }

    [Fact]
    public void Test_2015_04()
    {
        List<string> expectedExampleSolutions =
        [
            string.Format(Messages.ProblemSolutionFormat, 609043, 6742839),
            string.Format(Messages.ProblemSolutionFormat, 1048970, 5714438)
        ];
        string expectedSolution = string.Format(Messages.ProblemSolutionFormat, 254575, 1038736);

        Problem problemSolver = new Problem_2015_04();
        RunAndValidateAll(problemSolver, expectedExampleSolutions, expectedSolution);
    }

    [Fact]
    public void Test_2015_05()
    {

        List<string> expectedExampleSolutions =
        [
            string.Format(Messages.ProblemSolutionFormat, 2, 0),
            string.Format(Messages.ProblemSolutionFormat, 0, 2)
        ];
        string expectedSolution = string.Format(Messages.ProblemSolutionFormat, 258, 53);

        Problem problemSolver = new Problem_2015_05();
        RunAndValidateAll(problemSolver, expectedExampleSolutions, expectedSolution);
    }

    [Fact]
    public void Test_2015_06()
    {
        List<string> expectedExampleSolutions =
        [
            string.Format(Messages.ProblemSolutionFormat, 998996, 1001996),
            string.Format(Messages.ProblemSolutionFormat, 999999, 2000001)
        ];
        string expectedSolution = string.Format(Messages.ProblemSolutionFormat, 543903, 14687245);

        Problem problemSolver = new Problem_2015_06();
        RunAndValidateAll(problemSolver, expectedExampleSolutions, expectedSolution);
    }

    [Fact]
    public void Test_2015_07()
    {

         List<string> expectedExampleSolutions =
        [
            string.Format(Messages.ProblemSolutionFormat, 492, 1968)
        ];
        string expectedSolution = string.Format(Messages.ProblemSolutionFormat, 46065, 14134);

        Problem problemSolver = new Problem_2015_07();
        RunAndValidateAll(problemSolver, expectedExampleSolutions, expectedSolution);
    }

    [Fact]
    public void Test_2015_08()
    {

        List<string> expectedExampleSolutions =
        [
            string.Format(Messages.ProblemSolutionFormat, 12, 19)
        ];
        string expectedSolution = string.Format(Messages.ProblemSolutionFormat, 1350, 2085);

        Problem problemSolver = new Problem_2015_08();
        RunAndValidateAll(problemSolver, expectedExampleSolutions, expectedSolution);
    }

    [Fact]
    public void Test_2015_09()
    {

        List<string> expectedExampleSolutions =
        [
            string.Format(Messages.ProblemSolutionFormat, 605, 982)
        ];
        string expectedSolution = string.Format(Messages.ProblemSolutionFormat, 117, 909);

        Problem problemSolver = new Problem_2015_09();
        RunAndValidateAll(problemSolver, expectedExampleSolutions, expectedSolution);
    }

    [Fact]
    public void Test_2015_10()
    {
        List<string> expectedExampleSolutions =
        [
            string.Format(Messages.ProblemSolutionFormat, 237746, 3369156)
        ];
        string expectedSolution = string.Format(Messages.ProblemSolutionFormat, 492982, 6989950);

        Problem problemSolver = new Problem_2015_10();
        RunAndValidateAll(problemSolver, expectedExampleSolutions, expectedSolution);
    }

    [Fact]
    public void Test_2015_11()
    {
        List<string> expectedExampleSolutions =
        [
            string.Format(Messages.ProblemSolutionFormat, "abcdffaa", "abcdffbb"),
            string.Format(Messages.ProblemSolutionFormat, "ghjaabcc", "ghjbbcdd")

        ];
        string expectedSolution = string.Format(Messages.ProblemSolutionFormat, "cqjxxyzz", "cqkaabcc");

        Problem problemSolver = new Problem_2015_11();
        RunAndValidateAll(problemSolver, expectedExampleSolutions, expectedSolution);
    }

    [Fact]
    public void Test_2015_12()
    {
        List<string> expectedExampleSolutions =
        [
            string.Format(Messages.ProblemSolutionFormat, 6, 6),
            string.Format(Messages.ProblemSolutionFormat, 6, 4),
            string.Format(Messages.ProblemSolutionFormat, 15, 0),
            string.Format(Messages.ProblemSolutionFormat, 6, 6)
        ];
        string expectedSolution = string.Format(Messages.ProblemSolutionFormat, 119433,68466 );

        Problem problemSolver = new Problem_2015_12();
        RunAndValidateAll(problemSolver, expectedExampleSolutions, expectedSolution);
    }

    [Fact]
    public void Test_2015_13()
    {
        List<string> expectedExampleSolutions =
        [
            string.Format(Messages.ProblemSolutionFormat, 330, 286)
        ];
        string expectedSolution = string.Format(Messages.ProblemSolutionFormat, 709, 668);

        Problem problemSolver = new Problem_2015_13();
        RunAndValidateAll(problemSolver, expectedExampleSolutions, expectedSolution);
    }

    [Fact]
    public void Test_2015_14()
    {
        Extensions.RunAndValidateExecutionTime(() =>
        Assert.True(string.Format(Messages.ProblemSolutionFormat, 2640, 1102) == new Problem_2015_14().Solve(),
                Constants.IncorrectResultMessage
                ));
    }

    [Fact]
    public void Test_2015_15()
    {
        Extensions.RunAndValidateExecutionTime(() =>
        Assert.True(string.Format(Messages.ProblemSolutionFormat, 222870, 117936) == new Problem_2015_15().Solve(),
                Constants.IncorrectResultMessage
                ));
    }

    [Fact]
    public void Test_2015_16()
    {
        Extensions.RunAndValidateExecutionTime(() =>
        Assert.True(string.Format(Messages.ProblemSolutionFormat, 40, 241) == new Problem_2015_16().Solve(),
                Constants.IncorrectResultMessage
                ));
    }

    [Fact]
    public void Test_2015_17()
    {
        Extensions.RunAndValidateExecutionTime(() =>
        Assert.True(string.Format(Messages.ProblemSolutionFormat, 1638, 17) == new Problem_2015_17().Solve(),
                Constants.IncorrectResultMessage
                ));
    }

    [Fact]
    public void Test_2015_18()
    {
        Extensions.RunAndValidateExecutionTime(() =>
        Assert.True(string.Format(Messages.ProblemSolutionFormat, 1061, 1006) == new Problem_2015_18().Solve(),
                Constants.IncorrectResultMessage
                ));
    }

    [Fact]
    public void Test_2015_19()
    {
        Extensions.RunAndValidateExecutionTime(() =>
        Assert.True(string.Format(Messages.ProblemSolutionFormat, 535, 212) == new Problem_2015_19().Solve(),
                Constants.IncorrectResultMessage
                ));
    }

    [Fact]
    public void Test_2015_20()
    {
        Extensions.RunAndValidateExecutionTime(() =>
        Assert.True(string.Format(Messages.ProblemSolutionFormat, 665280, 705600) == new Problem_2015_20().Solve(),
                Constants.IncorrectResultMessage
                ));
    }

    [Fact]
    public void Test_2015_21()
    {
        Extensions.RunAndValidateExecutionTime(() =>
        Assert.True(string.Format(Messages.ProblemSolutionFormat, 78, 148) == new Problem_2015_21().Solve(),
                Constants.IncorrectResultMessage
                ));
    }

    [Fact]
    public void Test_2015_22()
    {
        // Due to usage of genetic algorithm, the solution might sometimes be wrong due to the fact that genetic algorithms do not guarantee the optimal solution.
        // There is a small change of getting the wrong result if the algorithm gets stuck on a local maximum
        Extensions.RunAndValidateExecutionTime(() =>
        Assert.True(string.Format(Messages.ProblemSolutionFormat, 900, 1216) == new Problem_2015_22().Solve(),
                Constants.IncorrectResultMessage
                ));
    }

    [Fact]
    public void Test_2015_23()
    {
        Extensions.RunAndValidateExecutionTime(() =>
        Assert.True(string.Format(Messages.ProblemSolutionFormat, 307, 160) == new Problem_2015_23().Solve(),
                Constants.IncorrectResultMessage
                ));
    }

    [Fact]
    public void Test_2015_24()
    {
        Extensions.RunAndValidateExecutionTime(() =>
        Assert.True(string.Format(Messages.ProblemSolutionFormat, 11266889531, 77387711) == new Problem_2015_24().Solve(),
                Constants.IncorrectResultMessage
                ));
    }

    [Fact]
    public void Test_2015_25()
    {
        Extensions.RunAndValidateExecutionTime(() =>
        Assert.True(string.Format(Messages.ProblemSolutionFormat, 9132360, "Congratulations!") == new Problem_2015_25().Solve(),
                Constants.IncorrectResultMessage
                ));
    }

}