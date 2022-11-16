namespace AdventOfCode._2015_15
{
    internal class MinimumQuantityInitializerStrategy : IQuantityInitializerStrategy
    {
        IEnumerable<Ingredient> IQuantityInitializerStrategy.Initialize(IEnumerable<Ingredient> ingredients, int totalQuantity)
        {
            if (ingredients.Count() <= 1)
            {
                return ingredients;
            }

            for (int i = 0; i < ingredients.Count(); i++)
            {
                ingredients.ElementAt(i).Quantity = 1;
            }
            return ingredients;
        }
    }
}
