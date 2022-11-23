namespace AdventOfCode._2015_15
{
    internal class MaximizeLastQuantityInitializerStrategy : IQuantityInitializerStrategy
    {
        IEnumerable<Ingredient> IQuantityInitializerStrategy.Initialize(IEnumerable<Ingredient> ingredients, int totalQuantity)
        {
            if (ingredients.Count() <= 1)
            {
                return ingredients;
            }

            // Highest factoring ingredient will receive the max quantity amount while all other ingredients will receive just 1;
            int maxedBalancedQuantity = totalQuantity - (ingredients.Count() - 1);
            ingredients.Last().Quantity = maxedBalancedQuantity;

            for (int i = 0; i < ingredients.Count() - 1; i++)
            {
                ingredients.ElementAt(i).Quantity = 1;
            }
            return ingredients;
        }
    }
}
