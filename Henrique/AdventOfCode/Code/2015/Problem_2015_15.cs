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
            Regex pattern = new Regex(IngredientPattern, RegexOptions.Compiled);
            string ingredientName = string.Empty;
            int capacityValue = 0;
            int durabilityValue = 0;
            int flavorValue = 0;
            int textureValue = 0;
            int caloriesValue = 0;

            List<Ingredient> ingredientsList = new List<Ingredient>();

            foreach (string line in InputLines)
            {
                Match match = pattern.Match(line);
                if (!match.Success)
                {
                    throw new Exception("Invalid line in input.");
                }
                else
                {
                    ingredientName = match.Groups[1].Value;
                    capacityValue = int.Parse(match.Groups[2].Value);
                    durabilityValue = int.Parse(match.Groups[3].Value);
                    flavorValue = int.Parse(match.Groups[4].Value);
                    textureValue = int.Parse(match.Groups[5].Value);
                    caloriesValue = int.Parse(match.Groups[6].Value);

                    ingredientsList.Add(new Ingredient(ingredientName, capacityValue, durabilityValue, flavorValue, textureValue, caloriesValue));
                }
            }

            TotalQuantityPerIngredient = TotalIngredientsQuantity - ingredientsList.Count() + 1;

            IngredientsQuantityInitializer ingredientsQuantityInitializer = new IngredientsQuantityInitializer(new MinimumQuantityInitializerStrategy(), ingredientsList, TotalIngredientsQuantity);
            IEnumerable<Ingredient> weightedIngredientsList = ingredientsQuantityInitializer.Initialize();
            IEnumerable<Cookie> possibleCookies = GenerateAllPossibleCookies(weightedIngredientsList, new List<Cookie>());

            string part1 = SolvePart1(possibleCookies);
            string part2 = SolvePart2(possibleCookies);

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";

        }

        private string SolvePart1(IEnumerable<Cookie> possibleCookies)
        {
            return possibleCookies.OrderByDescending(cookie => cookie.TotalValue).First().TotalValue.ToString();
        }

        private string SolvePart2(IEnumerable<Cookie> possibleCookies)
        {
            possibleCookies = possibleCookies.Where(cookie => cookie.Calories == WantedCalories);
            return possibleCookies.OrderByDescending(cookie => cookie.TotalValue).First().TotalValue.ToString();
        }

        /// <summary>
        /// Not working properly in some situations. May be related to the fact that it is hitting a local maximum.
        /// </summary>
        /// <param name="ingredientsList"></param>
        /// <returns></returns>
        [Obsolete("SolveAlternativePart1 is deprecated, please use SolvePart1 instead.")]
        private string SolveAlternativePart1(List<Ingredient> ingredientsList)
        {
            IngredientsQuantityInitializer ingredientsQuantityInitializer = new IngredientsQuantityInitializer(new RandomQuantityInitializerStrategy(), ingredientsList, TotalIngredientsQuantity);

            IEnumerable<Ingredient> weightedIngredientsList;
            do
            {
                weightedIngredientsList = ingredientsQuantityInitializer.Initialize();
            }
            while (ComputeCookieValue(weightedIngredientsList) == 0);

            return ComputeHighestCookieTotalValue(weightedIngredientsList.ToList()).ToString();
        }

        private long ComputeHighestCookieTotalValue(List<Ingredient> ingredientsList)
        {
            long highestCookieValue = ComputeCookieValue(ingredientsList);
            long oldHighestCookieValue = long.MinValue;

            do
            {
                oldHighestCookieValue = highestCookieValue;

                IngredientsScale? ingredientsScale = FindIngredientsToIncreaseDecrease(ingredientsList, highestCookieValue);
                if (ingredientsScale == null)
                {
                    return oldHighestCookieValue;
                }
                ingredientsScale.ApplyScale();
                highestCookieValue = ingredientsScale.CookieValue;

            }
            while (highestCookieValue >= oldHighestCookieValue);

            return oldHighestCookieValue;
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
                    Cookie cookie = new Cookie(new List<Ingredient>(((List<Ingredient>)ingredientsList).ConvertAll(ing => ing.Clone())));
                    possibleCookies.Add(cookie);
                }
            }
            return possibleCookies;
        }

        /// <summary>
        /// Find the ingredients which, when increasing the quantity by one to one ingredient 
        /// and decreasing the quantity by one to another ingredient, will maximize the total cookie value.
        /// </summary>
        /// <param name="ingredientsList"></param>
        /// <param name="currentHighestCookieValue"></param>
        /// <returns></returns>
        private IngredientsScale? FindIngredientsToIncreaseDecrease(IEnumerable<Ingredient> ingredientsList, long currentHighestCookieValue)
        {
            Ingredient? ingredientToIncrease = null;
            Ingredient? ingredientToDecrease = null;
            long potentialCookieValue = long.MinValue;

            foreach (Ingredient ingredient in ingredientsList)
            {
                // Check if it is still possible to increase the quantity
                if (ingredient.Quantity == TotalIngredientsQuantity - (ingredientsList.Count() - 1))
                {
                    continue;
                }

                foreach (Ingredient otherIngredient in ingredientsList.Where(ing => ing != ingredient))
                {
                    // Check if it is still possible to decrease the quantity
                    if (ingredient.Quantity == 1)
                    {
                        continue;
                    }
                    potentialCookieValue = SimulateCookieValue(ingredientsList, ingredient, otherIngredient);
                    if (potentialCookieValue > currentHighestCookieValue)
                    {
                        currentHighestCookieValue = potentialCookieValue;
                        ingredientToIncrease = ingredient;
                        ingredientToDecrease = otherIngredient;
                    }
                }
            }

            if (ingredientToIncrease != null && ingredientToDecrease != null)
            {
                return new IngredientsScale(ingredientToIncrease, ingredientToDecrease, currentHighestCookieValue);
            }
            return null;
        }

        private long SimulateCookieValue(IEnumerable<Ingredient> ingredientsList, Ingredient ingredientToIncrease, Ingredient ingredientToDecrease)
        {
            ingredientToIncrease.Quantity++;
            ingredientToDecrease.Quantity--;

            long simulatedCookieValue = ComputeCookieValue(ingredientsList);

            ingredientToIncrease.Quantity--;
            ingredientToDecrease.Quantity++;

            return simulatedCookieValue;
        }

        private long ComputeCookieValue(IEnumerable<Ingredient> ingredientsList)
        {
            long cookieValue = 0;
            int totalCookieCapacity = 0;
            int totalCookieDurability = 0;
            int totalCookieFlavor = 0;
            int totalCookieTexture = 0;

            foreach (Ingredient ingredient in ingredientsList)
            {
                int ingredientQuantity = ingredient.Quantity;

                int ingredientCookieCapacity = ingredient.Capacity * ingredientQuantity;
                int ingredientCookieDurability = ingredient.Durability * ingredientQuantity;
                int ingredientCookieFlavor = ingredient.Flavor * ingredientQuantity;
                int ingredientCookieTexture = ingredient.Texture * ingredientQuantity;

                totalCookieCapacity += ingredientCookieCapacity;
                totalCookieDurability += ingredientCookieDurability;
                totalCookieFlavor += ingredientCookieFlavor;
                totalCookieTexture += ingredientCookieTexture;
            }

            if (totalCookieCapacity <= 0 || totalCookieDurability <= 0 || totalCookieFlavor <= 0 || totalCookieTexture <= 0)
                return 0;

            cookieValue = totalCookieCapacity * totalCookieDurability * totalCookieFlavor * totalCookieTexture;
            return cookieValue;
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

        private void PrintIngredients(IEnumerable<Ingredient> ingredientsList)
        {
            Console.WriteLine("------ Ingredients -------");
            foreach (var ingredient in ingredientsList)
            {
                Console.Write($"{ingredient.Name} {ingredient.Quantity} | ");
            }
            Console.WriteLine("--------------------------");
        }
    }
}