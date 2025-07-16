namespace AdventOfCode.Code
{
    public class Problem_2015_18 : Problem
    {
        private readonly int GridSize;
        private readonly char[,] LightsGrid;
        private readonly char LightOff = '.';
        private readonly char LightOn = '#';


        public Problem_2015_18() : base()
        {
            GridSize = InputFirstLine.Length;
            LightsGrid = new char[GridSize, GridSize];
        }

        public override string Solve()
        {
            int i = 0;
            foreach (string line in InputLines)
            {
                if (line.Length != GridSize)
                {
                    throw new Exception("Invalid line in input.");
                }
                else
                {
                    Buffer.BlockCopy(line.ToCharArray(), 0, LightsGrid, (i * GridSize) * sizeof(char), GridSize * sizeof(char));
                    i++;
                }
            }

            string part1 = SolvePart1();
            string part2 = SolvePart2();

            return string.Format(SolutionFormat, part1, part2);

        }

        private string SolvePart1()
        {
            char[,] newLightsGrid = (char[,])LightsGrid.Clone();
            newLightsGrid = AnimateLightsForNSteps(100, newLightsGrid, false);

            return newLightsGrid.Cast<char>().Count(light => light == LightOn).ToString();
        }

        private string SolvePart2()
        {
            char[,] newLightsGrid = (char[,])LightsGrid.Clone();
            newLightsGrid = UpdateLightsGridCorners(newLightsGrid);
            newLightsGrid = AnimateLightsForNSteps(100, newLightsGrid, true);

            return newLightsGrid.Cast<char>().Count(light => light == LightOn).ToString();
        }

        private char[,] AnimateLightsForNSteps(int totalSteps, char[,] lightsGrid, bool hasfixedcorners)
        {
            for (int step = 0; step < totalSteps; step++)
            {
                char[,] tempLightsGrid = (char[,])lightsGrid.Clone();
                for (int i = 0; i < lightsGrid.GetLength(0); i++)
                {
                    for (int j = 0; j < lightsGrid.GetLength(0); j++)
                    {
                        tempLightsGrid[i, j] = CheckNextLightState(i, j, lightsGrid);
                    }
                }
                lightsGrid = (char[,])tempLightsGrid.Clone();
                if (hasfixedcorners)
                {
                    lightsGrid = UpdateLightsGridCorners(lightsGrid);
                }
            }
            return lightsGrid;
        }

        private char CheckNextLightState(int i, int j, char[,] lightsGrid)
        {
            int numberOfSurroundingLightsOn = 0;
            char[] surroundingLights =
            [
                GetLightState(i-1,j-1,lightsGrid),
                GetLightState(i,j-1,lightsGrid),
                GetLightState(i+1,j-1,lightsGrid),

                GetLightState(i-1,j,lightsGrid),
                GetLightState(i+1,j,lightsGrid),

                GetLightState(i-1,j+1,lightsGrid),
                GetLightState(i,j+1,lightsGrid),
                GetLightState(i+1,j+1,lightsGrid)
            ];

            numberOfSurroundingLightsOn = surroundingLights.Count(light => light == LightOn);

            if (lightsGrid[i, j] == LightOff && numberOfSurroundingLightsOn == 3)
            {
                return LightOn;
            }

            if (lightsGrid[i, j] == LightOn && (numberOfSurroundingLightsOn == 2 || numberOfSurroundingLightsOn == 3))
            {
                return LightOn;
            }

            return LightOff;
        }

        private char GetLightState(int i, int j, char[,] lightsGrid)
        {
            if (i < 0 || i >= GridSize || j < 0 || j >= GridSize)
            {
                return LightOff;
            }
            else return lightsGrid[i, j];
        }

        private char[,] UpdateLightsGridCorners(char[,] lightsGrid)
        {
            lightsGrid[0, 0] = '#';
            lightsGrid[0, GridSize - 1] = '#';
            lightsGrid[GridSize - 1, 0] = '#';
            lightsGrid[GridSize - 1, GridSize - 1] = '#';

            return lightsGrid;
        }
    }
}