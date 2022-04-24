namespace AdventOfCode.Code
{
    /// <summary>
    /// Problem must have the following:
    /// Class name should be Problem_{YEAR}_{DAY_NUMBER}
    /// </summary>
    internal abstract class Problem
    {
        internal int Year { get; }
        internal int DayNumber { get; }
        internal string Input { get; }

        internal Problem()
        {
            string className = GetType().Name;

            string[] problemName = className.Split('_');

            Year = int.Parse(problemName[1]);
            DayNumber = int.Parse(problemName[2]);
            Input = GetProblemInput();
        }

        /// <summary>
        /// Description text file should be in folder "Problems\{YEAR} and should have the following format "Day{DAY_NUMBER}.txt"  
        /// </summary>
        /// <returns>The problem description</returns>
        internal string GetProblemDescription()
        {
            return File.ReadAllText($"Problems/{Year}/Day{DayNumber}.txt");
        }

        /// <summary>
        /// Input text file should be in folder "Problems\{YEAR} and should have the following format "Day{DAY_NUMBER}_Input.txt"
        /// </summary>
        /// <returns>The problem input</returns>
        internal string GetProblemInput()
        {
            return File.ReadAllText($"Problems/{Year}/Day{DayNumber}_Input.txt");
        }

        internal abstract string Solve();


    }
}
