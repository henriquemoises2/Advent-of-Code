using AdventOfCode._2015_15;

namespace AdventOfCode.Code._2015.Entities._2015_15
{
    internal class IngredientsScale
    {
        internal Ingredient IngredientToIncrease { get; set; }
        internal Ingredient IngredientToDecrease { get; set; }
        internal long CookieValue { get; set; }
        internal int DeltaToCaloriesValue { get; set; }

        internal IngredientsScale(Ingredient ingredientToIncrease, Ingredient ingredientToDecrease, long cookieValue, int deltaToCaloriesValue = 0)
        {
            IngredientToIncrease = ingredientToIncrease;
            IngredientToDecrease = ingredientToDecrease;
            CookieValue = cookieValue;
            DeltaToCaloriesValue = deltaToCaloriesValue;
        }

        internal void ApplyScale()
        {
            IngredientToIncrease.Quantity++;
            IngredientToDecrease.Quantity--;
        }
    }
}
