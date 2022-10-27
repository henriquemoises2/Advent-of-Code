using AdventOfCode._2015_15;
using AdventOfCode.Code._2015.Entities._2015_15;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    internal class Problem_2015_15 : Problem
    {

        private const string IngredientPattern = @"^(?<name>\w+): capacity (?<capacity>-*\d+), durability (?<durability>-*\d+), flavor (?<flavor>-*\d+), texture (?<texture>-*\d+), calories (?<calories>-*\d+)";
        private const int TotalQuantity = 100;

        internal Problem_2015_15() : base()
        { }

        internal override string Solve()
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
            string part1 = SolvePart1(ingredientsList);
            string part2 = SolvePart2();

            return $"Part 1 solution: " + part1 + "\n"
                + "Part 2 solution: " + part2;
        }

        private string SolvePart1(List<Ingredient> ingredientsList)
        {
            IngredientsQuantityInitializer ingredientsQuantityInitializer = new IngredientsQuantityInitializer(new RandomQuantityInitializerStrategy(), ingredientsList, TotalQuantity);

            IEnumerable<Ingredient> weightedIngredientsList;
            do
            {
                weightedIngredientsList = ingredientsQuantityInitializer.Initialize();
            }
            while (ComputeCookieValue(weightedIngredientsList) == 0);

            return ComputeHighestCookieTotalValue(weightedIngredientsList.ToList()).ToString();
        }

        private string SolvePart2()
        {
            return "";
        }

        private long ComputeHighestCookieTotalValue(List<Ingredient> ingredientsList)
        {
            List<Ingredient> tempIngredientsList = new List<Ingredient>(ingredientsList);
            List<Ingredient> finalIngredientsList = new List<Ingredient>();
            long highestCookieValue = ComputeCookieValue(ingredientsList);
            long oldHighestCookieValue = long.MinValue;

            while (tempIngredientsList.Count() > 1)
            {
                Ingredient ingredientToIncrease = FindIngredientToIncrease(tempIngredientsList, highestCookieValue);
                ingredientToIncrease.Quantity++;
                Ingredient ingredientToDecrease = FindIngredientToDecrease(tempIngredientsList.Where(ing => ing.ID != ingredientToIncrease.ID), highestCookieValue);
                ingredientToDecrease.Quantity--;

                oldHighestCookieValue = highestCookieValue;
                highestCookieValue = ComputeCookieValue(ingredientsList);

                if (highestCookieValue < oldHighestCookieValue)
                {
                    ingredientToIncrease.Quantity--;
                    finalIngredientsList.Add(ingredientToIncrease);
                    tempIngredientsList.Remove(ingredientToIncrease);
                    ingredientToDecrease.Quantity++;
                }

            }
            finalIngredientsList.Add(tempIngredientsList.Single());
            return oldHighestCookieValue;
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
                int ingredientCookieCapacity = ingredient.Capacity * ingredient.Quantity;
                int ingredientCookieDurability = ingredient.Durability * ingredient.Quantity;
                int ingredientCookieFlavor = ingredient.Flavor * ingredient.Quantity;
                int ingredientCookieTexture = ingredient.Texture * ingredient.Quantity;

                totalCookieCapacity += ingredientCookieCapacity;
                totalCookieDurability += ingredientCookieDurability;
                totalCookieFlavor += ingredientCookieFlavor;
                totalCookieTexture += ingredientCookieTexture;
            }

            if (totalCookieCapacity <= 0 || totalCookieDurability <= 0 || totalCookieFlavor <= 0 || totalCookieTexture <= 0)
                return 0;

            cookieValue = totalCookieCapacity *totalCookieDurability * totalCookieFlavor * totalCookieTexture;
            return cookieValue;
        }

        /// <summary>
        /// Find the ingredient which, when increasing the quantity by one, will maximize the total cookie value.
        /// </summary>
        /// <param name="ingredientsList"></param>
        /// <param name="currentHighestCookieValue"></param>
        /// <returns></returns>
        private Ingredient FindIngredientToIncrease(IEnumerable<Ingredient> ingredientsList, long currentHighestCookieValue)
        {
            Ingredient ingredientToIncrease = ingredientsList.First();
            long newHighestCookieValue = currentHighestCookieValue;
            long newCookieValue = 0;
            foreach (Ingredient ingredient in ingredientsList)
            {
                ingredient.Quantity++;
                newCookieValue = ComputeCookieValue(ingredientsList);

                if (newCookieValue > newHighestCookieValue)
                {
                    ingredientToIncrease = ingredient;
                    newHighestCookieValue = newCookieValue;
                }

                ingredient.Quantity--;

            }
            return ingredientToIncrease;
        }

        /// <summary>
        /// Find the ingredient which, when decreasing the quantity by one, will maximize the total cookie value. Works in pair with FindIngredientToIncrease since there will always be an increase before decreasing any other ingredient.
        /// </summary>
        /// <param name="ingredientsList"></param>
        /// <param name="currentHighestCookieValue"></param>
        /// <returns></returns>
        private Ingredient FindIngredientToDecrease(IEnumerable<Ingredient> ingredientsList, long currentHighestCookieValue)
        {
            Ingredient ingredientToDecrease = ingredientsList.First();
            long newHighestCookieValue = currentHighestCookieValue;
            long newCookieValue = 0;
            foreach (Ingredient ingredient in ingredientsList)
            {
                ingredient.Quantity--;
                newCookieValue = ComputeCookieValue(ingredientsList);

                if (newCookieValue > newHighestCookieValue)
                {
                    ingredientToDecrease = ingredient;
                    newHighestCookieValue = newCookieValue;
                }

                ingredient.Quantity++;

            }
            return ingredientToDecrease;
        }
    }
}
