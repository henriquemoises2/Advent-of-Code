using AdventOfCode._2015_7;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    public class Problem_2015_07 : Problem
    {

        private const string PatternValue = "^(\\d+|[a-z]+) -> ([a-z]+)$";
        private const string PatternUnaryOperation = "^([A-Z]+) (\\d+|[a-z]+) -> ([a-z]+)$";
        private const string PatternBinaryOperation = "^(\\d+|[a-z]+) ([A-Z]+) (\\d+|[a-z]+) -> ([a-z]+)$";

        private const string CableToEvaluate = "a";
        private const string CableToOverride = "b";

        public Problem_2015_07() : base()
        {

        }

        public override string Solve()
        {
            Regex regexPatterValue = new Regex(PatternValue, RegexOptions.Compiled);
            Regex regexPatternUnaryOperation = new Regex(PatternUnaryOperation, RegexOptions.Compiled);
            Regex regexPatternBinaryOperation = new Regex(PatternBinaryOperation, RegexOptions.Compiled);
            Match match;

            IDictionary<string, ISource> originalCircuit = new Dictionary<string, ISource>();
            OperationFactory factory = new OperationFactory(originalCircuit);

            foreach (string line in InputLines)
            {
                if (regexPatterValue.IsMatch(line))
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

            IDictionary<string, ISource> circuit1 = new Dictionary<string, ISource>(originalCircuit);
            IDictionary<string, ISource> circuit2 = new Dictionary<string, ISource>(originalCircuit);

            string part1 = SolvePart1(circuit1);
            string part2 = SolvePart2(circuit2, part1);

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";

        }

        private string SolvePart1(IDictionary<string, ISource> circuit)
        {
            return circuit[CableToEvaluate].Evaluate(circuit).ToString();
        }

        private string SolvePart2(IDictionary<string, ISource> circuit, string resultSignal)
        {
            circuit[CableToOverride] = new Value(ushort.Parse(resultSignal));
            return circuit[CableToEvaluate].Evaluate(circuit).ToString();
        }


    }
}
