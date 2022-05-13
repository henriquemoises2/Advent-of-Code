using System.Text.RegularExpressions;
using System.Linq;
using AdventOfCode._2015_7;

namespace AdventOfCode.Code
{
    internal class Problem_2015_7 : Problem
    {

        private const string PatternValue = "^(\\d+|[a-z]+) -> ([a-z]+)$";
        private const string PatternUnaryOperation = "^([A-Z]+) (\\d+|[a-z]+) -> ([a-z]+)$";
        private const string PatternBinaryOperation = "^(\\d+|[a-z]+) ([A-Z]+) (\\d+|[a-z]+) -> ([a-z]+)$";

        private const string CableToEvaluate = "a";

        internal Problem_2015_7() : base()
        {
        }

        internal override string Solve()
        {
            string part1 = SolvePart1();
            string part2 = SolvePart2();

            return $"Part 1 solution: " + part1 + "\n"
                + "Part 2 solution: " + part2;
        }

        private string SolvePart1()
        {
            Regex regexPatterValue = new Regex(PatternValue, RegexOptions.Compiled);
            Regex regexPatternUnaryOperation = new Regex(PatternUnaryOperation, RegexOptions.Compiled);
            Regex regexPatternBinaryOperation = new Regex(PatternBinaryOperation, RegexOptions.Compiled);
            Match match;
            IDictionary<string, ISource> circuit = new Dictionary<string, ISource>();
            OperationFactory factory = new OperationFactory(circuit); 

            foreach (string line in InputLines)
            {
                if(regexPatterValue.IsMatch(line))
                {
                    match = regexPatterValue.Match(line);

                    string originValue = match.Groups[1].Value;
                    string destinationCableName = match.Groups[2].Value;

                    // Signal value is assigned to destination cable
                    if (ushort.TryParse(originValue, out ushort signalValue))
                    {
                        factory.AddSignal(signalValue, destinationCableName);
                    }
                    // Cable reference is assigned to destination cable
                    else
                    {
                        factory.AddCableUnion(originValue, destinationCableName);
                    }

                }
                else if (regexPatternUnaryOperation.IsMatch(line))
                {
                    match = regexPatternUnaryOperation.Match(line);
                    string operation = match.Groups[1].Value;
                    string originValue = match.Groups[2].Value;
                    string destinationCableName = match.Groups[3].Value;

                    // Operation with signal value as input is assigned to destination cable
                    if (ushort.TryParse(originValue, out ushort signalValue))
                    {
                        factory.AddUnaryOperation(signalValue, operation, destinationCableName);
                    }
                    // Operation with reference cable as input is assigned to destination cable
                    else
                    {
                        factory.AddUnaryOperation(originValue, operation, destinationCableName);
                    }
                }
                else if (regexPatternBinaryOperation.IsMatch(line))
                {
                    match = regexPatternBinaryOperation.Match(line);
                    string originValue1 = match.Groups[1].Value;
                    string operation = match.Groups[2].Value;
                    string originValue2 = match.Groups[3].Value;
                    string destinationCableName = match.Groups[4].Value;

                    if (ushort.TryParse(originValue1, out ushort signalValue1))
                    {
                        // Operation with two signal values as input is assigned to destination cable
                        if (ushort.TryParse(originValue2, out ushort signalValue2))
                        {
                            factory.AddBinaryOperation(signalValue1, signalValue2, operation, destinationCableName);
                        }
                        // Operation with a signal and a cable reference as input is assigned to destination cable
                        else
                        {
                            factory.AddBinaryOperation(signalValue1, originValue2, operation, destinationCableName);
                        }
                    }                    
                    else
                    {
                        // Operation with a cable reference and a signal as input is assigned to destination cable
                        if (ushort.TryParse(originValue2, out ushort signalValue2))
                        {
                            factory.AddBinaryOperation(originValue1, signalValue2, operation, destinationCableName);
                        }
                        // Operation with two reference cables as input is assigned to destination cable
                        else
                        {
                            factory.AddBinaryOperation(originValue1, originValue2, operation, destinationCableName);
                        }
                    }


                }
                else
                {
                    throw new Exception("Invalid line in input file.");
                }

            }

            return circuit[CableToEvaluate].Evaluate(circuit).ToString();

        }

        private string SolvePart2()
        {
            return "";
        }


    }
}
