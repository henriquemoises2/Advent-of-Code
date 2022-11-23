using AdventOfCode.Constants;

namespace AdventOfCode.Code
{
    internal class UI
    {
        private static bool _debugMode = false;
        private static int _selectedYear = -1;

        internal static void ShowDialogueCycle()
        {
            string? input;

            while (true)
            {
                if (_selectedYear == -1)
                {
                    _selectedYear = SelectYear();
                    if (_selectedYear == -1)
                    {
                        continue;
                    }
                }

                Console.WriteLine("Please select the day (1-25) (or type \"back\" to return to year selection):");

                input = Console.ReadLine();
                int dayNumber = 0;
                try
                {
                    if (input != null && input.Equals("back", StringComparison.InvariantCultureIgnoreCase))
                    {
                        _selectedYear = -1;
                        Console.WriteLine();
                        continue;
                    }
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


                Problem? problem = ProblemFactory.GetProblem(_selectedYear, dayNumber);
                if (problem == null)
                {
                    Console.WriteLine($"No problem found for day {dayNumber} in {_selectedYear}.");
                    Console.WriteLine();
                    continue;
                }

                Console.WriteLine("Problem description:");
                Console.WriteLine();
                Console.WriteLine(problem.GetProblemDescription());

                //if(debugMode)
                //{
                //    Console.WriteLine("Input:");
                //    foreach(string line in problem.InputLines)
                //    {
                //        Console.WriteLine(line);
                //        Console.WriteLine();
                //    }
                //}

                Console.WriteLine("The solution for this problem is:");
                try
                {
                    string solution = problem.SolveWithStopWatch();
                    Console.WriteLine(solution);
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (_debugMode)
                {
                    Console.WriteLine("Time elapsed:");
                    Console.WriteLine($"{problem.StopWatch.ElapsedMilliseconds} ms");
                }

                Console.WriteLine();
                Console.ReadLine();

            }
        }

        private static int SelectYear()
        {

            Console.WriteLine("Welcome to Advent of Code. Please select the year (2015-2022):");
            string? input = Console.ReadLine();
            int year = 0;

            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    throw new Exception();

                }
                if (input.ToLowerInvariant() == "debug")
                {
                    _debugMode = true;
                    Console.WriteLine("Debug mode activated.");
                    return -1;
                }

                year = Convert.ToInt16(input);
                if (!Availability.AvailableYears.Contains(year))
                {
                    throw new Exception();
                }
                return year;
            }
            catch
            {
                Console.WriteLine("Invalid year.");
                Console.WriteLine();
                return -1;
            }
        }
    }
}
