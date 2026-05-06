using System.Diagnostics;
using AdventOfCode.Constants;

namespace AdventOfCode.Code;

/// <summary>
/// Problem must have the following:
/// Class name should be Problem_{YEAR}_{DAY_NUMBER}
/// </summary>
public abstract class Problem
{
    internal int Year { get; }
    internal int DayNumber { get; }
    internal IEnumerable<string> InputLines { get; private set; }
    internal IEnumerable<string> ExampleInputLines { get; }
    internal string InputFirstLine
    {
        get
        {
            if (InputLines != null && InputLines.Any())
            {
                return InputLines.First();
            }
            return string.Empty;
        }
    }
    internal string InputLastLine
    {
        get
        {
            if (InputLines != null && InputLines.Any())
            {
                return InputLines.Last();
            }
            return string.Empty;
        }
    }
    internal readonly Stopwatch StopWatch = new();
    protected bool IsDebugActive { get; set; }
    protected const string SolutionFormat = Messages.ProblemSolutionFormat;


    internal Problem()
    {
        string className = GetType().Name;

        string[] problemName = className.Split('_');

        Year = int.Parse(problemName[1]);
        DayNumber = int.Parse(problemName[2]);
        InputLines = GetProblemInputAllLines();
        ExampleInputLines = GetProblemExampleInputAllLines();
    }

    /// <summary>
    /// Description text file should be in folder "Problems\{YEAR}\{DAY_NUMBER} and should have the following format "ProblemDescription.txt"  
    /// </summary>
    /// <returns>The problem description</returns>
    internal string GetProblemDescription()
    {
        return File.ReadAllText($"Problems/{Year}/Day{DayNumber:00}/ProblemDescription.txt");
    }

    /// <summary>
    /// Input text file should be in folder "Problems\{YEAR}\{DAY_NUMBER} and should have the following format "Input.txt"
    /// </summary>
    /// <returns>Returns the input as a single string</returns>
    internal string GetProblemInputString()
    {
        return File.ReadAllText($"Problems/{Year}/Day{DayNumber:00}/Input.txt");
    }

    /// <summary>
    /// Input text file should be in folder "Problems\{YEAR}\{DAY_NUMBER} and should have the following format "Input.txt"
    /// </summary>
    /// <returns>Returns the input as a collection of lines as strings</returns>
    internal IEnumerable<string> GetProblemInputAllLines()
    {
        return File.ReadAllLines($"Problems/{Year}/Day{DayNumber:00}/Input.txt");
    }

    /// <summary>
    /// Input Example text file should be in folder "Problems\{YEAR}\{DAY_NUMBER} and should have the following format "Example.txt"
    /// </summary>
    /// <returns>Returns the input as a collection of lines as strings</returns>
    internal IEnumerable<string> GetProblemExampleInputAllLines()
    {
        return File.ReadAllLines($"Problems/{Year}/Day{DayNumber:00}/InputExample.txt");
    }

    internal string SolveInDebugMode()
    {
        IsDebugActive = true;
        StopWatch.Start();
        string result = Solve();
        StopWatch.Stop();
        return result;
    }

    public abstract string Solve();

    public virtual List<string> SolveExamples()
    {
        List<string> solutions = [];
        List<string> exampleInput = [];
        // Store real inputs
        List<string> realInputs = [.. InputLines];
        InputLines = [];
        foreach (string line in ExampleInputLines)
        {
            if (line == "<END_OF_EXAMPLE>")
            {
                InputLines = exampleInput;
                solutions.Add(Solve());
                exampleInput = [];
            }
            else
            {
                exampleInput.Add(line);
            }
        }
        InputLines = realInputs;
        return solutions;
    }

}
