namespace AdventOfCode._2015_15
{
    internal class RandomQuantityInitializerStrategy : IQuantityInitializerStrategy
    {
        IEnumerable<Ingredient> IQuantityInitializerStrategy.Initialize(IEnumerable<Ingredient> ingredients, int totalQuantity)
        {
            Random random = new Random();

            int maxAvailable = totalQuantity - ingredients.Count();
            Ingredient lastIngredient = ingredients.Last();

            foreach (Ingredient ingredient in ingredients)
            {
                if (ingredient == lastIngredient)
                {
                    ingredient.Quantity = maxAvailable + 1;
                }
                else
                {
                    int randomValue = random.Next(1, maxAvailable + 1);
                    ingredient.Quantity = randomValue;
                    maxAvailable = maxAvailable - randomValue + 1;
                }
            }
            return ingredients;
        }
    }
}
