using AdventOfCode._2015_15;
using System.Text.RegularExpressions;
using Cookie = AdventOfCode._2015_15.Cookie;

namespace AdventOfCode.Code
{
    public class Problem_2015_15 : Problem
    {

        private const string IngredientPattern = @"^(?<name>\w+): capacity (?<capacity>-*\d+), durability (?<durability>-*\d+), flavor (?<flavor>-*\d+), texture (?<texture>-*\d+), calories (?<calories>-*\d+)";
        private const int TotalIngredientsQuantity = 100;
        private const int WantedCalories = 500;
        private int TotalQuantityPerIngredient;


        public Problem_2015_15() : base()
        { }

        public override string Solve()
        {
            Regex pattern = new(IngredientPattern, RegexOptions.Compiled);
            List<Ingredient> ingredientsList = new();

            foreach (string line in InputLines)
            {
                Match match = pattern.Match(line);
                if (!match.Success)
                {
                    throw new Exception("Invalid line in input.");
                }
                else
                {
                    string ingredientName = match.Groups[1].Value;
                    int capacityValue = int.Parse(match.Groups[2].Value);
                    int durabilityValue = int.Parse(match.Groups[3].Value);
                    int flavorValue = int.Parse(match.Groups[4].Value);
                    int textureValue = int.Parse(match.Groups[5].Value);
                    int caloriesValue = int.Parse(match.Groups[6].Value);
                    ingredientsList.Add(new Ingredient(ingredientName, capacityValue, durabilityValue, flavorValue, textureValue, caloriesValue));
                }
            }

            TotalQuantityPerIngredient = TotalIngredientsQuantity - ingredientsList.Count + 1;

            IngredientsQuantityInitializer ingredientsQuantityInitializer = new(new MinimumQuantityInitializerStrategy(), ingredientsList, TotalIngredientsQuantity);
            IEnumerable<Ingredient> weightedIngredientsList = ingredientsQuantityInitializer.Initialize();
            IEnumerable<Cookie> possibleCookies = GenerateAllPossibleCookies(weightedIngredientsList, new List<Cookie>());

            string part1 = SolvePart1(possibleCookies);
            string part2 = SolvePart2(possibleCookies);

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";

        }

        private static string SolvePart1(IEnumerable<Cookie> possibleCookies)
        {
            return possibleCookies.OrderByDescending(cookie => cookie.TotalValue).First().TotalValue.ToString();
        }

        private static string SolvePart2(IEnumerable<Cookie> possibleCookies)
        {
            possibleCookies = possibleCookies.Where(cookie => cookie.Calories == WantedCalories);
            return possibleCookies.OrderByDescending(cookie => cookie.TotalValue).First().TotalValue.ToString();
        }

        private List<Cookie> GenerateAllPossibleCookies(IEnumerable<Ingredient> ingredientsList, List<Cookie> possibleCookies)
        {
            Ingredient? ingredientToIncrease;

            while ((ingredientToIncrease = UpdateIngredientsQuantity(ingredientsList)) != null)
            {
                ingredientToIncrease.Quantity++;
                int totalQuantity = ingredientsList.Sum(ing => ing.Quantity);
                if (TotalIngredientsQuantity == totalQuantity)
                {
                    Cookie cookie = new(new List<Ingredient>(((List<Ingredient>)ingredientsList).ConvertAll(ing => ing.Clone())));
                    possibleCookies.Add(cookie);
                }
            }
            return possibleCookies;
        }

        private Ingredient? UpdateIngredientsQuantity(IEnumerable<Ingredient> ingredientsList)
        {
            for (int i = ingredientsList.Count() - 1; i > 0; i--)
            {
                if (ingredientsList.ElementAt(i).Quantity == TotalQuantityPerIngredient ||
                    ingredientsList.Sum(ing => ing.Quantity) >= TotalIngredientsQuantity)
                {
                    if (i == 0)
                    {
                        return null;
                    }
                    if (ingredientsList.ElementAt(i - 1).Quantity == TotalQuantityPerIngredient)
                    {
                        ingredientsList.ElementAt(i).Quantity = 1;
                        continue;
                    }
                    else
                    {
                        ingredientsList.ElementAt(i).Quantity = 1;
                        return ingredientsList.ElementAt(i - 1);
                    }
                }
                else
                {
                    return ingredientsList.ElementAt(i);
                }

            }
            return null;
        }
    }
}