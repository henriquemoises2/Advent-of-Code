namespace AdventOfCode.Code
{
    public class Problem_2022_08 : Problem
    {

        public Problem_2022_08() : base()
        { }

        public override string Solve()
        {
            int[,] treeSizes = new int[InputLines.Count(), InputLines.First().Length];
            for (int i = 0; i < InputLines.Count(); i++)
            {
                char[] treeSizesInRow = InputLines.ToArray()[i].ToCharArray();
                for (int j = 0; j < treeSizesInRow.Length; j++)
                {
                    treeSizes[i, j] = int.Parse(treeSizesInRow[j].ToString());
                }
            }

            string part1 = SolvePart1(treeSizes);
            string part2 = SolvePart2(treeSizes);

            return string.Format(SolutionFormat, part1, part2);

        }

        private static string SolvePart1(int[,] treeSizes)
        {
            int visibleTrees = 0;
            int rows = treeSizes.GetLength(0);
            int columns = treeSizes.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (IsTreeVisible(i, j, treeSizes))
                    {
                        visibleTrees++;
                    }
                }
            }
            return visibleTrees.ToString();
        }

        private static string SolvePart2(int[,] treeSizes)
        {
            int rows = treeSizes.GetLength(0);
            int columns = treeSizes.GetLength(1);
            double scenicScore = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    scenicScore = Math.Max(scenicScore, ComputeScenicScore(i, j, treeSizes));
                }
            }
            return scenicScore.ToString();
        }

        private static bool IsTreeVisible(int i, int j, int[,] treeSizes)
        {
            return IsTreeBorder(i, j, treeSizes) || IsTreeVisibleFromOutside(i, j, treeSizes);
        }

        private static bool IsTreeBorder(int row, int column, int[,] treeSizes)
        {
            int rowSize = treeSizes.GetLength(0);
            int columnSize = treeSizes.GetLength(1);
            return row == 0 || column == 0 || row == rowSize - 1 || column == columnSize - 1;
        }

        private static bool IsTreeVisibleFromOutside(int row, int column, int[,] treeSizes)
        {
            int rowSize = treeSizes.GetLength(0);
            int columnSize = treeSizes.GetLength(1);

            bool isVisibleFromLeft = true, isVisibleFromRight = true, isVisibleFromTop = true, isVisibleFromBottom = true;

            // Left 
            for (int j = column - 1; j >= 0; j--)
            {
                if (treeSizes[row, j] >= treeSizes[row, column])
                {
                    isVisibleFromLeft = false;
                    break;
                }
            }
            // Right 
            for (int j = column + 1; j < columnSize; j++)
            {
                if (treeSizes[row, j] >= treeSizes[row, column])
                {
                    isVisibleFromRight = false;
                    break;
                }
            }
            // Top
            for (int i = row - 1; i >= 0; i--)
            {
                if (treeSizes[i, column] >= treeSizes[row, column])
                {
                    isVisibleFromTop = false;
                    break;
                }
            }
            // Bottom
            for (int i = row + 1; i < rowSize; i++)
            {
                if (treeSizes[i, column] >= treeSizes[row, column])
                {
                    isVisibleFromBottom = false;
                    break;
                }
            }
            return isVisibleFromLeft || isVisibleFromRight || isVisibleFromTop || isVisibleFromBottom;
        }

        private static double ComputeScenicScore(int row, int column, int[,] treeSizes)
        {
            int rowSize = treeSizes.GetLength(0);
            int columnSize = treeSizes.GetLength(1);
            double scenicScore = 1;
            int numberOfTrees = 0;

            // Left 
            for (int j = column - 1; j >= 0; j--)
            {
                if (treeSizes[row, j] >= treeSizes[row, column])
                {
                    numberOfTrees++;
                    break;
                }
                numberOfTrees++;
            }
            scenicScore *= numberOfTrees;
            numberOfTrees = 0;

            // Right 
            for (int j = column + 1; j < columnSize; j++)
            {
                if (treeSizes[row, j] >= treeSizes[row, column])
                {
                    numberOfTrees++;
                    break;
                }
                numberOfTrees++;
            }
            scenicScore *= numberOfTrees;
            numberOfTrees = 0;

            // Top
            for (int i = row - 1; i >= 0; i--)
            {
                if (treeSizes[i, column] >= treeSizes[row, column])
                {
                    numberOfTrees++;
                    break;
                }
                numberOfTrees++;
            }
            scenicScore *= numberOfTrees;
            numberOfTrees = 0;

            // Bottom
            for (int i = row + 1; i < rowSize; i++)
            {
                if (treeSizes[i, column] >= treeSizes[row, column])
                {
                    numberOfTrees++;
                    break;
                }
                numberOfTrees++;
            }
            scenicScore *= numberOfTrees;

            return scenicScore;
        }
    }
}