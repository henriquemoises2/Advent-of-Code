namespace AdventOfCode.Code
{
    public class Problem_2015_01 : Problem
    {
        public Problem_2015_01() : base()
        {
        }

        public override string Solve()
        {
            string part1 = SolvePart1();
            string part2 = SolvePart2();

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";
        }

        private string SolvePart1()
        {
            int upStairs, downStairs = 0;

            upStairs = InputFirstLine.Count(inpt => inpt == '(');
            downStairs = InputFirstLine.Count(inpt => inpt == ')');

            return (upStairs - downStairs).ToString();
        }

        private string SolvePart2()
        {
            int level = 0;
            for (int i = 0; i < InputFirstLine.Length; i++)
            {
                if (InputFirstLine[i] == '(')
                {
                    level++;
                }
                if (InputFirstLine[i] == ')')
                {
                    level--;
                }
                if (level < 0)
                {
                    return (i + 1).ToString();
                }
            }
            return "Solution not found.";
        }

    }
}
