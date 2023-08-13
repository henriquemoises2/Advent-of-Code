using AdventOfCode.Code._2015.Entities._2015_23;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    internal class Problem_2015_23 : Problem
    {
        private const string InstructionPattern = @"^(?<instruction>\w+) (?<register>\w+)*(, (?<sign1>[+-])(?<value1>\d+))*((?<sign2>[+-])(?<value2>\d+))*$";

        internal Problem_2015_23() : base()
        {
        }

        internal override string Solve()
        {
            Regex pattern = new Regex(InstructionPattern, RegexOptions.Compiled);
            List<IInstruction> instructions = new List<IInstruction>();
            List<Register> registers = new List<Register>();
            try
            {

                foreach (string line in InputLines)
                {
                    Match match = pattern.Match(line);
                    string instruction = match.Groups["instruction"].Value;
                    char.TryParse(match.Groups["sign1"].Value, out char sign1);
                    int.TryParse(match.Groups["value1"].Value, out int value1);
                    char.TryParse(match.Groups["sign2"].Value, out char sign2);
                    int.TryParse(match.Groups["value2"].Value, out int value2);

                    Register? register = null;
                    if (char.TryParse(match.Groups["register"].Value, out char registerId))
                    {
                        register = registers.SingleOrDefault(reg => reg.Id == registerId);
                        if (register == null)
                        {
                            register = new Register(registerId);
                            registers.Add(register);
                        }
                    }
                    InstructionParameters parameters = new InstructionParameters(instruction, register, sign1, value1, sign2, value2);
                    IInstruction intruction = InstructionsFactory.GetInstruction(parameters);
                    instructions.Add(intruction);

                }
            }
            catch
            {
                throw new Exception("Invalid line in input.");
            }

            string part1 = SolvePart1(instructions, registers);
            string part2 = SolvePart2(instructions, registers);

            return $"Part 1 solution: " + part1 + "\n"
                + "Part 2 solution: " + part2;
        }

        private string SolvePart1(List<IInstruction> instructions, IEnumerable<Register> registers)
        {
            int currentIndex = 0, totalInstructions = instructions.Count;

            while(currentIndex < totalInstructions)
            {
                currentIndex = instructions[currentIndex].Apply(currentIndex);
            }

            return registers.Single(reg => reg.Id == 'b').StoredValue.ToString();
        }

        private string SolvePart2(List<IInstruction> instructions, IEnumerable<Register> registers)
        {

            foreach(Register register in registers)
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
    }
}