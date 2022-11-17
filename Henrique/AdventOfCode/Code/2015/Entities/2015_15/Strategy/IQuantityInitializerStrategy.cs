namespace AdventOfCode._2015_15
{
    internal interface IQuantityInitializerStrategy
    {
        internal IEnumerable<Ingredient> Initialize(IEnumerable<Ingredient> ingredients, int totalQuantity);
    }
}
