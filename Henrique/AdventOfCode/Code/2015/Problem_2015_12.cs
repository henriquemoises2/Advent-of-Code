﻿using System.Text.Json;

namespace AdventOfCode.Code
{
    public class Problem_2015_12 : Problem
    {
        public Problem_2015_12() : base()
        {
        }

        public override string Solve()
        {
            JsonDocument json = JsonDocument.Parse(InputFirstLine);
            JsonElement baseElement = json.RootElement;

            string part1 = SolvePart1(baseElement);
            string part2 = SolvePart2(baseElement);

            return string.Format(SolutionFormat, part1, part2);

        }

        private string SolvePart1(JsonElement baseElement)
        {
            return RecursiveSearchJson(baseElement, 0).ToString();
        }

        private string SolvePart2(JsonElement baseElement)
        {
            return RecursiveSearchJsonSkipReds(baseElement, 0).ToString();
        }


        private int RecursiveSearchJson(JsonElement currentElement, int totalSum)
        {
            if (currentElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var elem in currentElement.EnumerateArray())
                {
                    totalSum = ProcessElement(elem, totalSum);
                }
            }
            else if (currentElement.ValueKind == JsonValueKind.Object)
            {
                foreach (var elem in currentElement.EnumerateObject())
                {
                    totalSum = ProcessElement(elem.Value, totalSum);
                }
            }
            else if (currentElement.ValueKind == JsonValueKind.Number)
            {
                totalSum += currentElement.GetInt32();
            }
            return totalSum;
        }

        private int RecursiveSearchJsonSkipReds(JsonElement currentElement, int totalSum)
        {

            if (currentElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var elem in currentElement.EnumerateArray())
                {
                    totalSum = ProcessElementSkipReds(elem, totalSum);
                }
            }
            else if (currentElement.ValueKind == JsonValueKind.Object)
            {
                var objectElements = currentElement.EnumerateObject();

                if (!objectElements.Where(e => e.Value.ValueKind == JsonValueKind.String).Any(e => e.Value.GetString()?.ToLower() == "red"))
                {
                    foreach (var elem in objectElements)
                    {
                        totalSum = ProcessElementSkipReds(elem.Value, totalSum);
                    }
                }
            }
            else if (currentElement.ValueKind == JsonValueKind.Number)
            {
                totalSum += currentElement.GetInt32();
            }
            return totalSum;
        }

        private int ProcessElement(JsonElement elem, int totalSum)
        {
            if (elem.ValueKind == JsonValueKind.Number)
            {
                return totalSum + elem.GetInt32();
            }
            else if (elem.ValueKind == JsonValueKind.Object || elem.ValueKind == JsonValueKind.Array)
            {
                totalSum = RecursiveSearchJson(elem, totalSum);
            }
            return totalSum;
        }

        private int ProcessElementSkipReds(JsonElement elem, int totalSum)
        {
            if (elem.ValueKind == JsonValueKind.Number)
            {
                return totalSum + elem.GetInt32();
            }
            else if (elem.ValueKind == JsonValueKind.Object || elem.ValueKind == JsonValueKind.Array)
            {
                totalSum = RecursiveSearchJsonSkipReds(elem, totalSum);
            }
            return totalSum;
        }

    }
}
