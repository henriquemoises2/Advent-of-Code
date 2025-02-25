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
                throw new ArgumentException(number1.GetType().Name);
            }
            if (string.IsNullOrWhiteSpace(number2))
            {
                throw new ArgumentException(number2.GetType().Name);
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
                throw new ArgumentException(multiplicand.GetType().Name);
            }
            if (string.IsNullOrWhiteSpace(multiplier))
            {
                throw new ArgumentException(multiplier.GetType().Name);
            }

            List<string> parts = [];
            for (int i = multiplier.Length - 1; i >= 0; i--)
            {
                int multiplierDigitValue = multiplier[i] - '0';
                if (multiplierDigitValue == 0)
                {
                    continue;
                }

                int carry = 0;
                StringBuilder productResultForMultiplierDigit = new();
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
                throw new ArgumentException(dividend.GetType().Name);
            }

            int result = 0;
            for (int i = 0; i < dividend.Length; i++)
            {
                int digit = dividend[i] - '0';
                result = (result * 10 + digit) % moduloValue;
            }

            return result;

        }

        static internal int GCD(int[] numbers)
        {
            if (numbers.Length < 2)
            {
                throw new ArgumentException("Value cannot have less than 2 elements.", numbers.GetType().Name);
            }
            int[] numbersAux = numbers;
            int gcd = numbers[0];
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                gcd = GCD(gcd, numbersAux[1]);
                numbersAux = numbersAux.Skip(1).ToArray();
            }
            return gcd;
        }

        static internal int GCD(int a, int b)
        {
            if (a == 0)
            {
                throw new ArgumentException("Value cannot be zero.", a.GetType().Name);
            }
            if (b == 0)
            {
                throw new ArgumentException("Value cannot be zero.", b.GetType().Name);
            }
            do
            {
                int smallerNumber = a;
                int largerNumber = b;
                a = Math.Min(smallerNumber, largerNumber);
                b = Math.Max(smallerNumber, largerNumber);

                b %= a;
            }
            while (b != 0);
            return a;

        }

        static internal int LCM(int[] numbers)
        {
            if (numbers.Length < 2)
            {
                throw new ArgumentException("Value cannot have less than 2 elements.", numbers.GetType().Name);
            }
            int[] numbersAux = numbers;
            int lcm = numbers[0];
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                lcm = LCM(lcm, numbersAux[1]);
                numbersAux = numbersAux.Skip(1).ToArray();
            }
            return lcm;
        }

        static internal int LCM(int a, int b)
        {
            int gcd = GCD(a, b);
            return Math.Abs(a * b) / gcd;
        }

    }
}
