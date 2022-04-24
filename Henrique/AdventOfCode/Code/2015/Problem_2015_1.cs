namespace AdventOfCode.Code
{
    internal class Problem_2015_1 : Problem
    {
        internal Problem_2015_1() : base()
        {
        }

        internal override string Solve()
        {

            int upStairs, downStaris = 0;

            upStairs = Input.Count(inpt => inpt == '(');
            downStaris = Input.Count(inpt => inpt == ')');

            return (upStairs - downStaris).ToString();

        }

    }
}
