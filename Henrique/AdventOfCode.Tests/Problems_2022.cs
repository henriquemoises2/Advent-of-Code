using AdventOfCode.Constants;

namespace AdventOfCode.Tests
{
    public class Problems_2022
    {
        [Fact]
        public void Test_2022_01()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 64929, 193697), new Problem_2022_01().Solve());
        }

        [Fact]
        public void Test_2022_02()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 9651, 10560), new Problem_2022_02().Solve());
        }

        [Fact]
        public void Test_2022_03()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 8515, 2434), new Problem_2022_03().Solve());
        }


        [Fact]
        public void Test_2022_04()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 528, 881), new Problem_2022_04().Solve());
        }

        [Fact]
        public void Test_2022_05()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, "LJSVLTWQM", "BRQWDBBJM"), new Problem_2022_05().Solve());
        }

        [Fact]
        public void Test_2022_06()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 1848, 2308), new Problem_2022_06().Solve());
        }
    }
}