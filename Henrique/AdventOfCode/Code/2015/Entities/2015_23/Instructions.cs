namespace AdventOfCode.Code._2015.Entities._2015_23
{

    internal enum InstructionType
    {
        Half = 1,
        Triple = 2,
        Increment = 3,
        Jump = 4,
        JumpIfEven = 5,
        JumpIfOne = 6
    }

    internal static class InstructionsFactory
    {
        internal static IInstruction GetSpell(InstructionType instruction, Register? register = null, int? offset = null)
        {
            switch (instruction)
            {
                case InstructionType.Half:
                    return new Half(register.Value);
                case InstructionType.Triple:
                    return new Triple(register.Value);
                case InstructionType.Increment:
                    return new Increment(register.Value);
                case InstructionType.Jump:
                    return new Jump(offset.Value);
                case InstructionType.JumpIfEven:
                    return new JumpIfEven(offset);
                case InstructionType.JumpIfOne:
                    return new JumpIfOne(offset);
                default: throw new ArgumentOutOfRangeException(nameof(instruction));
            }
        }
    }

    public interface IInstruction { }

    public interface IAirthmeticInstruction : IInstruction
    {
        void Apply();
    }

    internal class Half : IAirthmeticInstruction
    {
        internal Register Register { get; set; }
        internal Half(Register register)
        {
            Register = register;
        }

        public void Apply ()
        {
            Register.StoredValue = Register.StoredValue / 2;
        }
    }

    internal class Triple : IAirthmeticInstruction
    {
        internal Register Register { get; set; }
        internal Triple(Register register)
        {
            Register = register;
        }

        public void Apply()
        {
            Register.StoredValue = Register.StoredValue * 3;
        }
    }

    internal class Increment : IAirthmeticInstruction
    {
        internal Register Register { get; set; }
        internal Increment(Register register)
        {
            Register = register;
        }

        public void Apply()
        {
            Register.StoredValue = Register.StoredValue + 1;
        }
    }

    public interface IControlFlowInstruction : IInstruction
    {
        int Apply(int currentIndex);
    }

    internal class Jump : IControlFlowInstruction
    {
        internal int Offset { get; set; }

        internal Jump (int offset)
        {
            Offset = offset;
        }

        public int Apply(int currentIndex)
        {
            return currentIndex + Offset;
        }
    }

    public interface IConditionalFlowInstruction : IInstruction
    {
        int Apply(Register register, int currentIndex);
    }

    internal class JumpIfEven : IConditionalFlowInstruction
    {
        internal Register Register { get; set;}
        internal int Offset { get; set; }

        internal JumpIfEven(Register register, int offset)
        {
            Register = register;
            Offset = offset;
        }

        public int Apply(Register register, int currentIndex)
        {
            if(register.StoredValue % 2 == 0)
            {
                return currentIndex + Offset;
            }
            else
            {
                return currentIndex;
            } 
                
        }
    }

    internal class JumpIfOne : IConditionalFlowInstruction
    {
        internal Register Register { get; set; }
        internal int Offset { get; set; }

        internal JumpIfOne(Register register, int offset)
        {
            Register = register;
            Offset = offset;
        }

        public int Apply(Register register, int currentIndex)
        {
            if (register.StoredValue == 1)
            {
                return currentIndex + Offset;
            }
            else
            {
                return currentIndex;
            }

        }
    }
}
