using System.Text;

namespace AdventOfCode.Helpers
{
    static internal class MathOperations
    {
        static internal IEnumerable<int> GetDivisors(int number)
        {
            List<int> result =
            [
                1,
                number
            ];

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

        static internal string SumStrings(string number1, string number2)
        {
            if (string.IsNullOrWhiteSpace(number1))
            {
                throw new ArgumentException(nameof(number1));
            }
            if (string.IsNullOrWhiteSpace(number2))
            {
                throw new ArgumentException(nameof(number2));
            }

            int number1Index = number1.Length - 1;
            int number2Index = number2.Length - 1;
            StringBuilder newNumberStringBuilder = new();
            int carry = 0;

            while (number1Index >= 0 || number2Index >= 0 || carry > 0)
            {
                int number1Value = number1Index >= 0 ? number1[number1Index] - '0' : 0;
                int number2Value = number2Index >= 0 ? number2[number2Index] - '0' : 0;

                int result = number1Value + number2Value + carry;
                carry = result >= 10 ? 1 : 0;
                newNumberStringBuilder.Append(result % 10);
                number1Index--;
                number2Index--;
            }
            return new(newNumberStringBuilder.ToString().Reverse().ToArray());
        }

        static internal string MultiplyStrings(string multiplicand, string multiplier)
        {
            if (string.IsNullOrWhiteSpace(multiplicand))
            {
                throw new ArgumentException(nameof(multiplicand));
            }
            if (string.IsNullOrWhiteSpace(multiplier))
            {
                throw new ArgumentException(nameof(multiplier));
            }

            List<string> parts = new List<string>();
            for (int i = multiplier.Length - 1; i >= 0; i--)
            {
                int multiplierDigitValue = multiplier[i] - '0';
                if (multiplierDigitValue == 0)
                {
                    continue;
                }

                int carry = 0;
                StringBuilder productResultForMultiplierDigit = new StringBuilder();
                for (int zIndex = 0; zIndex < multiplier.Length - 1 - i; zIndex++)
                {
                    productResultForMultiplierDigit.Append('0');
                }
                for (int j = multiplicand.Length - 1; j >= 0; j--)
                {
                    int result = ((multiplier[i] - '0') * (multiplicand[j] - '0')) + carry;
                    productResultForMultiplierDigit.Append(result % 10);
                    carry = result >= 10 ? result / 10 : 0;
                }
                if (carry > 0)
                {
                    productResultForMultiplierDigit.Append(carry);
                }
                parts.Add(new(productResultForMultiplierDigit.ToString().Reverse().ToArray()));
            }

            if (parts.Count == 1)
            {
                return parts[0];
            }

            string productResult = parts[0];
            for (int i = 1; i < parts.Count; i++)
            {
                productResult = SumStrings(productResult, parts[i]);
            }
            return productResult;

        }

        static internal int ModuloOfString(string dividend, int moduloValue)
        {
            if (string.IsNullOrWhiteSpace(dividend))
            {
                throw new ArgumentException(nameof(dividend));
            }

            int result = 0;
            for (int i = 0; i < dividend.Length; i++)
            {
                int digit = dividend[i] - '0';
                result = (result * 10 + digit) % moduloValue;
            }

            return result;

        }
    }
}
