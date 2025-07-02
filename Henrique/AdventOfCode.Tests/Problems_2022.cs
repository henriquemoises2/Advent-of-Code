using AdventOfCode.Constants;

namespace AdventOfCode.Tests
{
    public class Problems_2022
    {
        [Fact]
        public void Test_2022_01()
        {
            Extensions.RunAndValidateExecutionTime(() =>
                Assert.True(
                    string.Format(Messages.ProblemSolutionFormat, 64929, 193697) == new Problem_2022_01().Solve(),
                    Constants.IncorrectResultMessage
                    )
            );
        }

        [Fact]
        public void Test_2022_02()
        {
            Extensions.RunAndValidateExecutionTime(() =>
            Assert.True(string.Format(Messages.ProblemSolutionFormat, 9651, 10560) == new Problem_2022_02().Solve(),
                    Constants.IncorrectResultMessage
                    )
            );
        }

        [Fact]
        public void Test_2022_03()
        {
            Extensions.RunAndValidateExecutionTime(() =>
                Assert.True(string.Format(Messages.ProblemSolutionFormat, 8515, 2434) == new Problem_2022_03().Solve(),
                    Constants.IncorrectResultMessage));
        }


        [Fact]
        public void Test_2022_04()
        {
            Extensions.RunAndValidateExecutionTime(() =>
                Assert.True(string.Format(Messages.ProblemSolutionFormat, 528, 881) == new Problem_2022_04().Solve(),
                    Constants.IncorrectResultMessage));
        }

        [Fact]
        public void Test_2022_05()
        {
            Extensions.RunAndValidateExecutionTime(() =>
                Assert.True(string.Format(Messages.ProblemSolutionFormat, "LJSVLTWQM", "BRQWDBBJM") == new Problem_2022_05().Solve(),
                    Constants.IncorrectResultMessage));
        }

        [Fact]
        public void Test_2022_06()
        {
            Extensions.RunAndValidateExecutionTime(() =>
                Assert.True(string.Format(Messages.ProblemSolutionFormat, 1848, 2308) == new Problem_2022_06().Solve(),
                    Constants.IncorrectResultMessage));
        }

        [Fact]
        public void Test_2022_07()
        {
            Extensions.RunAndValidateExecutionTime(() =>
                Assert.True(string.Format(Messages.ProblemSolutionFormat, 1477771, 3579501) == new Problem_2022_07().Solve(),
                    Constants.IncorrectResultMessage));
        }

        [Fact]
        public void Test_2022_08()
        {
            Extensions.RunAndValidateExecutionTime(() =>
                Assert.True(string.Format(Messages.ProblemSolutionFormat, 1690, 535680) == new Problem_2022_08().Solve(),
                    Constants.IncorrectResultMessage));
        }

        [Fact]
        public void Test_2022_09()
        {
            Extensions.RunAndValidateExecutionTime(() =>
                Assert.True(string.Format(Messages.ProblemSolutionFormat, 5779, 2331) == new Problem_2022_09().Solve(),
                    Constants.IncorrectResultMessage));
        }

        [Fact]
        public void Test_2022_10()
        {
            Extensions.RunAndValidateExecutionTime(() =>
                Assert.True(string.Format(Messages.ProblemSolutionFormat, 17380, "FGCUZREC") == new Problem_2022_10().Solve(),
                    Constants.IncorrectResultMessage));
        }

        [Fact]
        public void Test_2022_11()
        {
            Extensions.RunAndValidateExecutionTime(() =>
                Assert.True(string.Format(Messages.ProblemSolutionFormat, 66124, 19309892877) == new Problem_2022_11().Solve(),
                    Constants.IncorrectResultMessage));
        }
    }
}