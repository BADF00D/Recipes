namespace Recipes.Contracts
{
    public class Recipe
    {
        public string Name { get; }
        public string Description { get; }
        public Ingedient[] Ingredients { get; }
        public string PreviewPath { get; }

        public Recipe(string name, string description, Ingedient[] ingredients, string previewPath)
        {
            Name = name;
            Description = description;
            Ingredients = ingredients;
            PreviewPath = previewPath;
        }
    }
}
