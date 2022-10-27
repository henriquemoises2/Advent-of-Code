namespace AdventOfCode._2015_15
{
    internal class MaximizeMostRelevantFactorQuantityInitializerStrategy : IQuantityInitializerStrategy
    {
        IEnumerable<Ingredient> IQuantityInitializerStrategy.Initialize(IEnumerable<Ingredient> ingredients, int totalQuantity)
        {
            if (ingredients.Count() <= 1)
            {
                return ingredients;
            }

            // Highest factoring ingredient will receive the max quantity amount while all other ingredients will receive just 1;
            int maxedBalancedQuantity = totalQuantity - (ingredients.Count() - 1);
            ingredients.ToList().Sort();
            var highestFactorIngredient = ingredients.ToList().ElementAt(0);
            highestFactorIngredient.Quantity = maxedBalancedQuantity;

            for (int i = 1; i < ingredients.Count(); i++)
            {
                ingredients.ElementAt(i).Quantity = 1;
            }
            return ingredients;
        }
    }
}
