namespace AdventOfCode.Helpers
{
    static internal class MathOperations
    {
        static internal IEnumerable<int> GetDivisors(int number)
        {
            List<int> result = new List<int>();

            result.Add(1);
            result.Add(number);

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
    }
}
