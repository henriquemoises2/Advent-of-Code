namespace AdventOfCode.Tests
{
    public class Problems_2015
    {
        [Fact]
        public void Test_2015_01()
        {
            Assert.Equal("Part 1 solution: 138\nPart 2 solution: 1771", new Problem_2015_01().Solve());
        }

        [Fact]
        public void Test_2015_02()
        {
            Assert.Equal("Part 1 solution: 1598415\nPart 2 solution: 3812909", new Problem_2015_02().Solve());
        }

        [Fact]
        public void Test_2015_03()
        {
            Assert.Equal("Part 1 solution: 2572\nPart 2 solution: 2631", new Problem_2015_03().Solve());
        }

        [Fact]
        public void Test_2015_04()
        {
            Assert.Equal("Part 1 solution: 254575\nPart 2 solution: 1038736", new Problem_2015_04().Solve());
        }

        [Fact]
        public void Test_2015_05()
        {
            Assert.Equal("Part 1 solution: 258\nPart 2 solution: 53", new Problem_2015_05().Solve());
        }

        [Fact]
        public void Test_2015_06()
        {
            Assert.Equal("Part 1 solution: 543903\nPart 2 solution: 14687245", new Problem_2015_06().Solve());
        }

        [Fact]
        public void Test_2015_07()
        {
            Assert.Equal("Part 1 solution: 46065\nPart 2 solution: 14134", new Problem_2015_07().Solve());
        }

        [Fact]
        public void Test_2015_08()
        {
            Assert.Equal("Part 1 solution: 1350\nPart 2 solution: 2085", new Problem_2015_08().Solve());
        }

        [Fact]
        public void Test_2015_09()
        {
            Assert.Equal("Part 1 solution: 117\nPart 2 solution: 909", new Problem_2015_09().Solve());
        }

        [Fact]
        public void Test_2015_10()
        {
            Assert.Equal("Part 1 solution: 492982\nPart 2 solution: 6989950", new Problem_2015_10().Solve());
        }

        [Fact]
        public void Test_2015_11()
        {
            Assert.Equal("Part 1 solution: cqjxxyzz\nPart 2 solution: cqkaabcc", new Problem_2015_11().Solve());
        }

        [Fact]
        public void Test_2015_12()
        {
            Assert.Equal("Part 1 solution: 119433\nPart 2 solution: 68466", new Problem_2015_12().Solve());
        }

        [Fact]
        public void Test_2015_13()
        {
            Assert.Equal("Part 1 solution: 709\nPart 2 solution: 668", new Problem_2015_13().Solve());
        }

        [Fact]
        public void Test_2015_14()
        {
            Assert.Equal("Part 1 solution: 2640\nPart 2 solution: 1102", new Problem_2015_14().Solve());
        }

        [Fact]
        public void Test_2015_15()
        {
            Assert.Equal("Part 1 solution: 222870\nPart 2 solution: 117936", new Problem_2015_15().Solve());
        }

        [Fact]
        public void Test_2015_16()
        {
            Assert.Equal("Part 1 solution: 40\nPart 2 solution: 241", new Problem_2015_16().Solve());
        }

        [Fact]
        public void Test_2015_17()
        {
            Assert.Equal("Part 1 solution: 1638\nPart 2 solution: 17", new Problem_2015_17().Solve());
        }

        [Fact]
        public void Test_2015_18()
        {
            Assert.Equal("Part 1 solution: 1061\nPart 2 solution: 1006", new Problem_2015_18().Solve());
        }

        [Fact]
        public void Test_2015_19()
        {
            Assert.Equal("Part 1 solution: 535\nPart 2 solution: 212", new Problem_2015_19().Solve());
        }

        [Fact]
        public void Test_2015_20()
        {
            Assert.Equal("Part 1 solution: 665280\nPart 2 solution: 705600", new Problem_2015_20().Solve());
        }

        [Fact]
        public void Test_2015_21()
        {
            Assert.Equal("Part 1 solution: 78\nPart 2 solution: 148", new Problem_2015_21().Solve());
        }

        [Fact]
        public void Test_2015_22()
        {
            // Due to usage of genetic algorithm, the solution might sometimes be wrong due to the fact that genetic algorithms do not guarantee the optimal solution.
            // There is a small change of getting the wrong result if the algorithm gets stuck on a local maximum
            Assert.Equal("Part 1 solution: 900\nPart 2 solution: 1216", new Problem_2015_22().Solve());
        }

        [Fact]
        public void Test_2015_23()
        {
            Assert.Equal("Part 1 solution: 307\nPart 2 solution: 160", new Problem_2015_23().Solve());
        }

        [Fact]
        public void Test_2015_24()
        {
            Assert.Equal("Part 1 solution: 11266889531\nPart 2 solution: 77387711", new Problem_2015_24().Solve());
        }

        [Fact]
        public void Test_2015_25()
        {
            Assert.Equal("Part 1 solution: 9132360\nPart 2 solution: Congratulations!", new Problem_2015_25().Solve());
        }

    }
}