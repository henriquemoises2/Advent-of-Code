namespace AdventOfCode.Helpers
{
    static internal class MathOperations
    {
        static internal IEnumerable<int> GetDivisors(int number)
        {
            List<int> result = new()
            {
                1,
                number
            };

            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                {
                    result.Add(i);
                    result.Add(number / i);
                }
            }
            return result.Distinct().OrderBy(number => number);
        }

        static internal long SumOfMultiplications(IEnumerable<int> numbers, int factor)
        {
            return numbers.Sum(number => factor * number);
        }

        static internal long MultiplyElements(IEnumerable<int> numbers)
        {
            long result = 1;
            foreach (int number in numbers)
            {
                result *= number;
            }
            return result;
        }
    }
}
