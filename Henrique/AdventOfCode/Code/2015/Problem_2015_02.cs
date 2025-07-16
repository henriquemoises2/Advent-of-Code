using System.IO;

namespace AdventOfCode.Code
{
    public class Problem_2015_02 : Problem
    {
        public Problem_2015_02() : base()
        {
        }

        public override string Solve()
        {
            int part1 = 0, part2 = 0;
            // Process each file line
            foreach (string line in GetProblemInputAllLines())
            {
                string[] dimensions = line.Split("x");
                if (dimensions.Length != 3
                    || !int.TryParse(dimensions[0], out int length) || length < 1
                    || !int.TryParse(dimensions[1], out int width) || width < 1
                    || !int.TryParse(dimensions[2], out int height) || height < 1
                )
                {
                    throw new Exception("Invalid line in input file.");
                }
                part1 += CalculateArea(length, width, height);
                part2 += CalculateRibbon(length, width, height);
            }
            return string.Format(SolutionFormat, part1, part2);
        }

        /// <summary>
        /// Compute the surface area of the required paper to wrap the present
        /// </summary>
        /// <param name="length"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private static int CalculateArea(int length, int width, int height)
        {
            int area1 = length * width;
            int area2 = width * height;
            int area3 = height * length;

            // Find the smallest value for the margin
            int margin = Math.Min(area1, Math.Min(area2, area3));

            // Calculate area with margin
            int area = (2 * area1) + (2 * area2) + (2 * area3) + margin;
            return area;
        }

        /// <summary>
        /// Compute the total length of ribbon needed to wrap the present
        /// </summary>
        /// <param name="length"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private static int CalculateRibbon(int length, int width, int height)
        {
            // Find the two smallest value to use in bow calculation
            Tuple<int, int> smallestSides = TwoMin(length, width, height);

            int ribbonSize = smallestSides.Item1 + smallestSides.Item1 + smallestSides.Item2 + smallestSides.Item2;
            int bowSize = length * width * height;

            // return total ribbon size
            return ribbonSize + bowSize;
        }

        /// <summary>
        /// Compute the two smallest values
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns>Returns the 2 min values</returns>
        private static Tuple<int, int> TwoMin(int a, int b, int c)
        {
            int maxValue = Math.Max(a, Math.Max(b, c));
            if (a == maxValue)
            {
                return Tuple.Create(b, c);
            }
            else if (b == maxValue)
            {
                return Tuple.Create(a, c);
            }
            else
            {
                return Tuple.Create(a, b);
            }
        }

    }
}
