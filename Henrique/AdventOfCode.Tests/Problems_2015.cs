using AdventOfCode.Constants;

namespace AdventOfCode.Tests
{
    public class Problems_2015
    {
        [Fact]
        public void Test_2015_01()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 138, 1771), new Problem_2015_01().Solve());
        }

        [Fact]
        public void Test_2015_02()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 1598415, 3812909), new Problem_2015_02().Solve());
        }

        [Fact]
        public void Test_2015_03()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 2572, 2631), new Problem_2015_03().Solve());
        }

        [Fact]
        public void Test_2015_04()
        {

            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 254575, 1038736), new Problem_2015_04().Solve());
        }

        [Fact]
        public void Test_2015_05()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 258, 53), new Problem_2015_05().Solve());
        }

        [Fact]
        public void Test_2015_06()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 543903, 14687245), new Problem_2015_06().Solve());
        }

        [Fact]
        public void Test_2015_07()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 46065, 14134), new Problem_2015_07().Solve());
        }

        [Fact]
        public void Test_2015_08()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 1350, 2085), new Problem_2015_08().Solve());
        }

        [Fact]
        public void Test_2015_09()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 117, 909), new Problem_2015_09().Solve());
        }

        [Fact]
        public void Test_2015_10()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 492982, 6989950), new Problem_2015_10().Solve());
        }

        [Fact]
        public void Test_2015_11()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, "cqjxxyzz", "cqkaabcc"), new Problem_2015_11().Solve());
        }

        [Fact]
        public void Test_2015_12()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 119433, 68466), new Problem_2015_12().Solve());
        }

        [Fact]
        public void Test_2015_13()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 709, 668), new Problem_2015_13().Solve());
        }

        [Fact]
        public void Test_2015_14()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 2640, 1102), new Problem_2015_14().Solve());
        }

        [Fact]
        public void Test_2015_15()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 222870, 117936), new Problem_2015_15().Solve());
        }

        [Fact]
        public void Test_2015_16()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 40, 241), new Problem_2015_16().Solve());
        }

        [Fact]
        public void Test_2015_17()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 1638, 17), new Problem_2015_17().Solve());
        }

        [Fact]
        public void Test_2015_18()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 1061, 1006), new Problem_2015_18().Solve());
        }

        [Fact]
        public void Test_2015_19()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 535, 212), new Problem_2015_19().Solve());
        }

        [Fact]
        public void Test_2015_20()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 665280, 705600), new Problem_2015_20().Solve());
        }

        [Fact]
        public void Test_2015_21()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 78, 148), new Problem_2015_21().Solve());
        }

        [Fact]
        public void Test_2015_22()
        {
            // Due to usage of genetic algorithm, the solution might sometimes be wrong due to the fact that genetic algorithms do not guarantee the optimal solution.
            // There is a small change of getting the wrong result if the algorithm gets stuck on a local maximum
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 900, 1216), new Problem_2015_22().Solve());
        }

        [Fact]
        public void Test_2015_23()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 307, 160), new Problem_2015_23().Solve());
        }

        [Fact]
        public void Test_2015_24()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 11266889531, 77387711), new Problem_2015_24().Solve());
        }

        [Fact]
        public void Test_2015_25()
        {
            Assert.Equal(string.Format(Messages.ProblemSolutionFormat, 9132360, "Congratulations!"), new Problem_2015_25().Solve());
        }

    }
}