namespace Recipes.Contracts
{
    public class IngredientGroup
    {
        public string Name { get; }
        public string[] Ingredients { get; }

        public IngredientGroup(string name, string[] ingredients)
        {
            Name = name;
            Ingredients = ingredients;
        }
    }
}