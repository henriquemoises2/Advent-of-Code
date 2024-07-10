namespace AdventOfCode._2015_15
{
    internal class Cookie
    {
        internal IEnumerable<Ingredient> Ingredients { get; set; }
        internal int Calories { get; }
        internal long TotalValue { get; }
        internal long TotalIngredients
        {
            get
            {
                return Ingredients.Sum(ing => ing.Quantity);
            }
        }


        internal Cookie(IEnumerable<Ingredient> ingredients)
        {
            Ingredients = ingredients;
            Calories = ComputeCaloriesValue();
            TotalValue = ComputeCookieValue();
        }

        private int ComputeCaloriesValue()
        {
            int totalCookieCalories = 0;

            foreach (Ingredient ingredient in Ingredients)
            {
                int ingredientQuantity = ingredient.Quantity;
                int ingredientCookieCalories = ingredient.Calories * ingredientQuantity;

                totalCookieCalories += ingredientCookieCalories;
            }

            if (totalCookieCalories <= 0)
                return 0;

            return totalCookieCalories;
        }

        private long ComputeCookieValue()
        {
            int totalCookieCapacity = 0;
            int totalCookieDurability = 0;
            int totalCookieFlavor = 0;
            int totalCookieTexture = 0;

            foreach (Ingredient ingredient in Ingredients)
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

            long cookieValue = totalCookieCapacity * totalCookieDurability * totalCookieFlavor * totalCookieTexture;
            return cookieValue;
        }

    }
}
