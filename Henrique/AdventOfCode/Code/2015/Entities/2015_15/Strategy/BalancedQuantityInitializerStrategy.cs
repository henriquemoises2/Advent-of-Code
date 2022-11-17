namespace AdventOfCode._2015_15
{
    internal class BalancedQuantityInitializerStrategy : IQuantityInitializerStrategy
    {
        IEnumerable<Ingredient> IQuantityInitializerStrategy.Initialize(IEnumerable<Ingredient> ingredients, int totalQuantity)
        {
            int balancedQuantity = totalQuantity / ingredients.Count();
            foreach (Ingredient ingredient in ingredients)
            {
                ingredient.Quantity = balancedQuantity;
            }
            return ingredients;
        }
    }
}
