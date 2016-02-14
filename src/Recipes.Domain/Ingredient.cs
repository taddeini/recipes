namespace Recipes.Domain
{
    public class Ingredient
    {
        public Ingredient(decimal amount, string description, IngredientUnit unit, IngredientState state)
        {
            Amount = amount;
            Description = description;
            Unit = unit;
            State = state;
        }

        public decimal Amount { get; }

        public string Description { get; }

        public IngredientUnit Unit { get; }

        public IngredientState State { get; }

    }
}