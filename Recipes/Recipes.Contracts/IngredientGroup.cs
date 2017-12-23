namespace Recipes.Contracts
{
    public class IngredientGroup
    {
        public string Name { get; }
        public Ingredient[] Ingredients { get; }

        public IngredientGroup(string name, Ingredient[] ingredients)
        {
            Name = name;
            Ingredients = ingredients;
        }
    }
}