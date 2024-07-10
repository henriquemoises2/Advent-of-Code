namespace AdventOfCode.Code
{
    public class Problem_2022_02 : Problem
    {
        public Problem_2022_02() : base()
        {
        }

        public override string Solve()
        {
            string part1 = SolvePart1(InputLines);
            string part2 = SolvePart2(InputLines);

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";

        }

        private static string SolvePart1(IEnumerable<string> InputLines)
        {
            // A, X = Rock
            // B, Y = Paper
            // C, Z = Scissors
            try
            {
                int totalPoints = 0;
                foreach (string line in InputLines)
                {
                    char opponentChoice = line[0];
                    char playerChoice = line[2];
                    totalPoints += PlayRockPaperScissors(opponentChoice, playerChoice);
                }
                return totalPoints.ToString();
            }
            catch
            {
                throw new Exception("Invalid line in input.");
            }
        }

        private static string SolvePart2(IEnumerable<string> InputLines)
        {
            // A = Rock
            // B = Paper
            // C = Scissors
            // X = Lose
            // Y = Draw
            // Z = Win
            try
            {
                int totalPoints = 0;
                foreach (string line in InputLines)
                {
                    char opponentChoice = line[0];
                    char playerChoice = line[2];
                    totalPoints += PlayRockPaperScissorsWithResult(opponentChoice, playerChoice);
                }
                return totalPoints.ToString();
            }
            catch
            {
                throw new Exception("Invalid line in input.");
            }
        }

        private static int PlayRockPaperScissors(char opponent, char player)
        {
            int points = 0;
            switch (opponent)
            {
                case 'A':
                    switch (player)
                    {
                        case 'X':
                            points = 1 + 3;
                            break;
                        case 'Y':
                            points = 2 + 6;
                            break;
                        case 'Z':
                            points = 3 + 0;
                            break;
                        default: break;
                    }
                    break;
                case 'B':
                    switch (player)
                    {
                        case 'X':
                            points = 1 + 0;
                            break;
                        case 'Y':
                            points = 2 + 3;
                            break;
                        case 'Z':
                            points = 3 + 6;
                            break;
                        default: break;
                    }
                    break;
                case 'C':
                    switch (player)
                    {
                        case 'X':
                            points = 1 + 6;
                            break;
                        case 'Y':
                            points = 2 + 0;
                            break;
                        case 'Z':
                            points = 3 + 3;
                            break;
                        default: break;
                    }
                    break;
                default: break;
            }
            return points;
        }

        private static int PlayRockPaperScissorsWithResult(char opponent, char result)
        {
            int points = 0;
            switch (opponent)
            {
                case 'A':
                    switch (result)
                    {
                        case 'X':
                            points = 3 + 0;
                            break;
                        case 'Y':
                            points = 1 + 3;
                            break;
                        case 'Z':
                            points = 2 + 6;
                            break;
                        default: break;
                    }
                    break;
                case 'B':
                    switch (result)
                    {
                        case 'X':
                            points = 1 + 0;
                            break;
                        case 'Y':
                            points = 2 + 3;
                            break;
                        case 'Z':
                            points = 3 + 6;
                            break;
                        default: break;
                    }
                    break;
                case 'C':
                    switch (result)
                    {
                        case 'X':
                            points = 2 + 0;
                            break;
                        case 'Y':
                            points = 3 + 3;
                            break;
                        case 'Z':
                            points = 1 + 6;
                            break;
                        default: break;
                    }
                    break;
                default: break;
            }
            return points;
        }

    }
}
