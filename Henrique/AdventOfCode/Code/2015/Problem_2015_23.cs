using AdventOfCode._2015_23;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    public partial class Problem_2015_23 : Problem
    {
        private const string InstructionPattern = @"^(?<instruction>\w+) (?<register>\w+)*(, (?<sign1>[+-])(?<value1>\d+))*((?<sign2>[+-])(?<value2>\d+))*$";

        public Problem_2015_23() : base()
        {
        }

        public override string Solve()
        {
            Regex pattern = InputRegex();
            List<IInstruction> instructions = [];
            List<Register> registers = [];
            try
            {

                foreach (string line in InputLines)
                {
                    Match match = pattern.Match(line);
                    string instruction = match.Groups["instruction"].Value;
                    bool parsedSign1 = char.TryParse(match.Groups["sign1"].Value, out char sign1);
                    bool parsedValue1 = int.TryParse(match.Groups["value1"].Value, out int value1);
                    bool parsedSign2 = char.TryParse(match.Groups["sign2"].Value, out char sign2);
                    bool parsedValue2 = int.TryParse(match.Groups["value2"].Value, out int value2);
                    Register? register = null;
                    bool parsedRegister = char.TryParse(match.Groups["register"].Value, out char registerId);
                    if (!parsedSign1 && !parsedValue1 && !parsedSign2 && !parsedValue2 && !parsedRegister)
                    {
                        throw new Exception("Invalid line in input.");
                    }
                    if (parsedRegister)
                    {
                        register = registers.SingleOrDefault(reg => reg.Id == registerId);
                        if (register == null)
                        {
                            register = new Register(registerId);
                            registers.Add(register);
                        }
                    }
                    InstructionParameters parameters = new(instruction, register, sign1, value1, sign2, value2);
                    IInstruction intruction = InstructionsFactory.GetInstruction(parameters);
                    instructions.Add(intruction);

                }
            }
            catch (Exception)
            {
                throw new Exception("Invalid line in input.");
            }

            string part1 = SolvePart1(instructions, registers);
            string part2 = SolvePart2(instructions, registers);

            return string.Format(SolutionFormat, part1, part2);

        }

        private static string SolvePart1(List<IInstruction> instructions, IEnumerable<Register> registers)
        {
            int currentIndex = 0, totalInstructions = instructions.Count;

            while (currentIndex < totalInstructions)
            {
                currentIndex = instructions[currentIndex].Apply(currentIndex);
            }

            return registers.Single(reg => reg.Id == 'b').StoredValue.ToString();
        }

        private static string SolvePart2(List<IInstruction> instructions, IEnumerable<Register> registers)
        {

            foreach (Register register in registers)
            {
                if (register.Id == 'a')
                {
                    register.StoredValue = 1;
                }
                else
                {
                    register.StoredValue = 0;
                }
            }

            return SolvePart1(instructions, registers);
        }

        [GeneratedRegex(InstructionPattern, RegexOptions.Compiled)]
        private static partial Regex InputRegex();
    }
}