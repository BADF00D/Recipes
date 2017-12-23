namespace Recipes.Contracts
{
    public class Recipe
    {
        public string Name { get; }
        public string Source { get; }
        public IngredientGroup[] IngredientGroups { get; }
        public string PreviewPath { get; }

        public Recipe(string name, string source, IngredientGroup[] ingredientGroups, string previewPath)
        {
            Name = name;
            Source = source;
            IngredientGroups = ingredientGroups;
            PreviewPath = previewPath;
        }
    }
}
