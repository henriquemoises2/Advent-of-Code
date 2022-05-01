using AdventOfCode.Constants;

namespace AdventOfCode.Code
{
    internal class UI
    {
        internal static void ShowDialogueCycle()
        {
            bool debugMode = false;
            while (true)
            {
                Console.WriteLine("Welcome to Advent of Code. Please select the year (2015-2022):");
                string? input = Console.ReadLine();
                int year = 0;
                try
                {
                    if(string.IsNullOrEmpty(input))
                    {
                        throw new Exception();

                    }
                    if (input.ToLowerInvariant() == "debug")
                    {
                        debugMode = true;
                        Console.WriteLine("Debug mode activated.");
                        continue;
                    }

                    year = Convert.ToInt16(input);
                    if (!Availability.AvailableYears.Contains(year))
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid year.");
                    Console.WriteLine();
                    continue;
                }


                Console.WriteLine("Please select the day (1-25):");

                input = Console.ReadLine();
                int dayNumber = 0;
                try
                {
                    dayNumber = Convert.ToInt16(input);
                    if (dayNumber < 1 || dayNumber > 25)
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid day.");
                    Console.WriteLine();
                    continue;
                }


                Problem? problem = ProblemFactory.GetProblem(year, dayNumber);
                if (problem == null)
                {
                    Console.WriteLine($"No problem found for day {dayNumber} in {year}.");
                    Console.WriteLine();
                    continue;
                }

                Console.WriteLine("Problem description:");
                Console.WriteLine();
                Console.WriteLine(problem.GetProblemDescription());

                if(debugMode)
                {
                    Console.WriteLine("Input:");
                    Console.WriteLine();
                    foreach(string line in problem.InputLines)
                    {
                        Console.WriteLine(line);
                    }
                }

                Console.WriteLine("The solution for this problem is:");
                try
                {
                    string solution = problem.Solve();
                    Console.WriteLine(solution);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
                Console.WriteLine();
                Console.ReadLine();

            }
        }
    }
}
