namespace AdventOfCode._2015_15
{
    internal class IngredientsQuantityInitializer
    {
        private readonly IQuantityInitializerStrategy _strategy;
        private readonly IEnumerable<Ingredient> _ingredients;
        private readonly int _totalQuantity;


        internal IngredientsQuantityInitializer(IQuantityInitializerStrategy strategy, IEnumerable<Ingredient> ingredients, int totalQuantity)
        {
            _strategy = strategy;
            _ingredients = ingredients;
            _totalQuantity = totalQuantity;
        }

        internal IEnumerable<Ingredient> Initialize()
        {
            return _strategy.Initialize(_ingredients, _totalQuantity);
        }

    }
}
