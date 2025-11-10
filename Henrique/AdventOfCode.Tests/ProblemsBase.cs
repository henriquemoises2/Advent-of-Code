namespace AdventOfCode.Tests;

public class ProblemsBase()
{
    internal static void RunAndValidateAll(Problem problemSolver, List<string> expectedExampleSolutions, string expectedSolution)
    {
        Extensions.RunAndValidateExecutionTime(() =>
        {
            TestExamples(problemSolver, expectedExampleSolutions);
        });

        Extensions.RunAndValidateExecutionTime(() =>
        {
            TestReal(problemSolver, expectedSolution);
        });
    }

    private static void TestReal(Problem problemSolver, string expectedSolution)
    {
        string solution = problemSolver.Solve();
        Assert.True(expectedSolution == solution, Constants.IncorrectResultMessage);
    }

    private static void TestExamples(Problem problemSolver, List<string> expectedSolutions)
    {
        List<string> solutions = problemSolver.SolveExamples();

        for (int i = 0; i < solutions.Count; i++)
        {
            Assert.True(expectedSolutions[i] == solutions[i], Constants.IncorrectResultMessage);
        }
    }
}