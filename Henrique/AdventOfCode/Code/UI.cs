using AdventOfCode.Constants;

namespace AdventOfCode.Code
{
    internal class UI
    {
        private static bool IsDebugMode = false;

        internal static void ShowDialogueCycle()
        {
            string? input;
            int SelectedYear = -1;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Welcome to the Advent of Code problems solver!");
            Console.WriteLine("----------------------------------------------");

            Console.ResetColor();
            Console.WriteLine("Select a year and day to solve the associated problem. Type 'back' to return or 'debug' to toggle debug mode.");
            Console.WriteLine("");

            while (true)
            {
                if (SelectedYear == -1)
                {
                    SelectedYear = SelectYear();
                    if (SelectedYear == -1)
                    {
                        continue;
                    }
                }

                Console.WriteLine("Please select the day (1-25):");

                input = Console.ReadLine();
                int dayNumber;
                try
                {
                    if (input != null && input.Equals("back", StringComparison.InvariantCultureIgnoreCase))
                    {
                        SelectedYear = -1;
                        Console.WriteLine();
                        continue;
                    }
                    if (CheckDebugMode(input))
                    {
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


                Problem? problem = ProblemFactory.GetProblem(SelectedYear, dayNumber);
                if (problem == null)
                {
                    Console.WriteLine($"No problem found for day {dayNumber} in {SelectedYear}.");
                    Console.WriteLine();
                    continue;
                }

                Console.WriteLine("Problem description:");
                Console.WriteLine();
                Console.WriteLine(problem.GetProblemDescription());

                Console.WriteLine("The solution for this problem is:");
                try
                {
                    string solution;
                    if (IsDebugMode)
                    {
                        solution = problem.SolveInDebugMode();
                    }
                    else
                    {
                        solution = problem.Solve();
                    }
                    Console.WriteLine(solution);
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (IsDebugMode)
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

            Console.WriteLine("Please select the year (2015-2022):");
            string? input = Console.ReadLine();
            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    throw new Exception();

                }
                if (CheckDebugMode(input))
                {
                    return -1;
                }

                int year = Convert.ToInt16(input);
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

        private static bool CheckDebugMode(string? input)
        {
            if (input != null)
            {
                if (input.Equals("debug", StringComparison.InvariantCultureIgnoreCase))
                {
                    IsDebugMode = !IsDebugMode;

                    if (IsDebugMode)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Debug mode activated.");
                    }
                    else
                    {
                        Console.ResetColor();
                        Console.WriteLine("Debug mode deactivated.");
                    }
                    return true;
                }
            }
            return false;
        }

    }
}
