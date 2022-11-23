namespace AdventOfCode.Code
{
    internal class Problem_2015_01 : Problem
    {
        internal Problem_2015_01() : base()
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
            int upStairs, downStaris = 0;

            upStairs = InputFirstLine.Count(inpt => inpt == '(');
            downStaris = InputFirstLine.Count(inpt => inpt == ')');

            return (upStairs - downStaris).ToString();
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
