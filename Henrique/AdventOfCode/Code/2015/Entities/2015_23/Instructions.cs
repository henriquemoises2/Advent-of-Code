﻿namespace AdventOfCode._2015_23
{
    internal class InstructionParameters
    {
        internal string Code { get; set; }
        internal Register? Register { get; set; }
        internal char? Sign1 { get; set; }
        internal int? Value1 { get; set; }
        internal char? Sign2 { get; set; }
        internal int? Value2 { get; set; }

        public InstructionParameters(string code, Register? register, char? sign1, int? value1, char? sign2, int? value2)
        {
            Code = code;
            Register = register;
            Sign1 = sign1;
            Value1 = value1;
            Sign2 = sign2;
            Value2 = value2;
        }
    }

    internal static class InstructionsFactory
    {
        internal static IInstruction GetInstruction(InstructionParameters instructionParameters)
        {
            if (instructionParameters.Register != null)
            {
                return instructionParameters.Code switch
                {
                    "hlf" => new Half(instructionParameters.Register),
                    "tpl" => new Triple(instructionParameters.Register),
                    "inc" => new Increment(instructionParameters.Register),
                    "jie" => new JumpIfEven(instructionParameters.Register, (instructionParameters.Sign1 == '+' ? 1 : -1) * instructionParameters.Value1.GetValueOrDefault()),
                    "jio" => new JumpIfOne(instructionParameters.Register, (instructionParameters.Sign1 == '+' ? 1 : -1) * instructionParameters.Value1.GetValueOrDefault()),
                    _ => throw new ArgumentException(nameof(instructionParameters.Code)),
                };
            }
            else
            {
                return instructionParameters.Code switch
                {
                    "jmp" => new Jump((instructionParameters.Sign2 == '+' ? 1 : -1) * instructionParameters.Value2.GetValueOrDefault()),
                    _ => throw new ArgumentException(nameof(instructionParameters.Code)),
                };
            }

        }
    }

    public interface IInstruction
    {
        int Apply(int currentIndex);
    }

    internal class Half : IInstruction
    {
        internal Register Register { get; set; }
        internal Half(Register register)
        {
            Register = register;
        }

        public int Apply(int currentIndex)
        {
            Register.StoredValue = Register.StoredValue / 2;
            return currentIndex + 1;
        }
    }

    internal class Triple : IInstruction
    {
        internal Register Register { get; set; }
        internal Triple(Register register)
        {
            Register = register;
        }

        public int Apply(int currentIndex)
        {
            Register.StoredValue = Register.StoredValue * 3;
            return currentIndex + 1;
        }
    }

    internal class Increment : IInstruction
    {
        internal Register Register { get; set; }
        internal Increment(Register register)
        {
            Register = register;
        }

        public int Apply(int currentIndex)
        {
            Register.StoredValue = Register.StoredValue + 1;
            return currentIndex + 1;
        }
    }

    internal class Jump : IInstruction
    {
        internal int Offset { get; set; }

        internal Jump(int offset)
        {
            Offset = offset;
        }

        public int Apply(int currentIndex)
        {
            return currentIndex + Offset;
        }
    }

    internal class JumpIfEven : IInstruction
    {
        internal Register Register { get; set; }
        internal int Offset { get; set; }

        internal JumpIfEven(Register register, int offset)
        {
            Register = register;
            Offset = offset;
        }

        public int Apply(int currentIndex)
        {
            if (Register.StoredValue % 2 == 0)
            {
                return currentIndex + Offset;
            }
            else
            {
                return currentIndex + 1;
            }

        }
    }

    internal class JumpIfOne : IInstruction
    {
        internal Register Register { get; set; }
        internal int Offset { get; set; }

        internal JumpIfOne(Register register, int offset)
        {
            Register = register;
            Offset = offset;
        }

        public int Apply(int currentIndex)
        {
            if (Register.StoredValue == 1)
            {
                return currentIndex + Offset;
            }
            else
            {
                return currentIndex + 1;
            }

        }
    }
}
