﻿namespace AdventOfCode.Code
{
    public class Problem_2015_04 : Problem
    {
        public Problem_2015_04() : base()
        {
        }

        public override string Solve()
        {
            string part1 = SolvePart1();
            string part2 = SolvePart2();

            return string.Format(SolutionFormat, part1, part2);

        }

        private string SolvePart1()
        {
            byte[] zeroArray = [0x00, 0x00];
            byte[] inputBytes, hashBytes;
            string input;
            for (int i = 0; i < int.MaxValue; i++)
            {
                input = InputFirstLine + i;
                inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
                hashBytes = System.Security.Cryptography.MD5.HashData(inputBytes);

                // Compare in hexadecimal instead of converting to string for performance reasons
                // The first 2 bytes have to be 0x00 (which is equal to 0000 in hex)
                if (hashBytes.Take(2).SequenceEqual(zeroArray))
                {
                    // Then the next byte have to start with a 0, which is any byte lower than or equal to 0x15 (which is 0- in hex)
                    if (hashBytes[2] <= 0x15)
                    {
                        return i.ToString();
                    }
                }
            }
            return "Solution not found";
        }

        private string SolvePart2()
        {
            byte[] zeroArray = [0x00, 0x00, 0x00];
            byte[] inputBytes, hashBytes;
            string input;
            for (int i = 0; i < int.MaxValue; i++)
            {
                input = InputFirstLine + i;
                inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
                hashBytes = System.Security.Cryptography.MD5.HashData(inputBytes);

                // Compare in hexadecimal instead of converting to string for performance reasons
                // The first 3 bytes have to be 0x00 (which is equal to 000000 in hex)
                if (hashBytes.Take(3).SequenceEqual(zeroArray))
                {
                    return i.ToString();
                }
            }
            return "Solution not found";
        }
    }
}
