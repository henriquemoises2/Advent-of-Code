using System.Text;

namespace AdventOfCode.Code
{
    public class Problem_2015_11 : Problem
    {
        private const byte ByteValueA = 0x61;
        private const byte ByteValueI = 0x69;
        private const byte ByteValueL = 0x6c;
        private const byte ByteValueO = 0x6f;
        private const byte ByteValueZ = 0x7a;

        public Problem_2015_11() : base()
        {
        }

        public override string Solve()
        {
            string part1 = SolvePart1();
            string part2 = SolvePart2(part1);

            return string.Format(SolutionFormat, part1, part2);

        }

        private string SolvePart1()
        {
            return FindNextValidPassword(InputFirstLine).ToString();
        }

        private static string SolvePart2(string sequence)
        {
            return FindNextValidPassword(sequence).ToString();
        }

        private static string FindNextValidPassword(string currentPassword)
        {
            bool isValidPassword = false;
            while (!isValidPassword)
            {
                currentPassword = IncrementPassword(currentPassword);
                // There is no need to validate the second requirement because it is already guaranteed that there are no invalid characters by function SkipInvalidCharacters
                isValidPassword = ValidateFirstRequirement(currentPassword) /*&& ValidateSecondRequirement(currentPassword)*/ && ValidateThirdRequirement(currentPassword);
            }
            return currentPassword;
        }

        private static string IncrementPassword(string password)
        {
            byte[] passwordAsByteArray = Encoding.ASCII.GetBytes(password);
            int i = passwordAsByteArray.Length - 1;
            while (i >= 0)
            {
                byte currentLetter = passwordAsByteArray[i];
                if (currentLetter == ByteValueZ)
                {
                    currentLetter = ByteValueA;
                    passwordAsByteArray[i] = currentLetter;
                }
                else
                {
                    passwordAsByteArray[i]++;
                    // Skip invalid characters (Requirement #2)
                    passwordAsByteArray = SkipInvalidCharacters(passwordAsByteArray);
                    return Encoding.ASCII.GetString(passwordAsByteArray);
                }
                i--;
            }


            return Encoding.ASCII.GetString(passwordAsByteArray);
        }

        private static byte[] SkipInvalidCharacters(byte[] passwordAsByteArray)
        {
            List<int> indexesOfInvalidCharacters =
            [
                Array.IndexOf(passwordAsByteArray, ByteValueI),
                Array.IndexOf(passwordAsByteArray, ByteValueL),
                Array.IndexOf(passwordAsByteArray, ByteValueO)
            ];

            if (!indexesOfInvalidCharacters.Any(index => index > -1))
            {
                return passwordAsByteArray;
            }

            // Get the lowest index that is not -1, i.e. that exists
            int indexOfInvalidCharacter = indexesOfInvalidCharacters.Where(index => index != -1).Min();
            passwordAsByteArray[indexOfInvalidCharacter]++;

            for (int i = indexOfInvalidCharacter + 1; i < passwordAsByteArray.Length; i++)
            {
                passwordAsByteArray[i] = ByteValueA;
            }

            return passwordAsByteArray;
        }

        /// <summary>
        /// Validate if there are at least three consecutive letters
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private static bool ValidateFirstRequirement(string password)
        {
            byte[] passwordAsByteArray = Encoding.ASCII.GetBytes(password);
            for (int i = 0; i <= passwordAsByteArray.Length - 3; i++)
            {
                if (IsNextChar(passwordAsByteArray[i], passwordAsByteArray[i + 1]) && IsNextChar(passwordAsByteArray[i + 1], passwordAsByteArray[i + 2]))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Validate if there are at least two pairs of adjacent equal characters.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private static bool ValidateThirdRequirement(string password)
        {
            byte[] passwordAsByteArray = Encoding.ASCII.GetBytes(password);
            int nSequentialEqualPairs = 0;
            for (int i = 0; i <= passwordAsByteArray.Length - 2; i++)
            {
                if (passwordAsByteArray[i] == passwordAsByteArray[i + 1])
                {
                    nSequentialEqualPairs++;
                    // Skip adjacent letter
                    i++;
                    if (nSequentialEqualPairs == 2)
                    {
                        return true;
                    }
                }
            }
            return false;

        }

        /// <summary>
        /// Check if character2 is the next character after character1
        /// </summary>
        /// <param name="character1"></param>
        /// <param name="character2"></param>
        private static bool IsNextChar(byte character1, byte character2)
        {
            return character2 == character1 + 1;
        }


    }
}
