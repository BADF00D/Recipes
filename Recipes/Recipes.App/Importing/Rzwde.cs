using System;
using System.Linq;
using System.Text.RegularExpressions;
using Recipes.Contracts;
using System.Threading.Tasks;
using Recipes.App.Extensions;

namespace Recipes.App.Importing
{
    class Rzwde : IImport
    {
        private readonly IHtmlLoader uriLoader;
        private Regex _nameRegex = new Regex(@"<a href=""javascript:;"">(?<name>[a-zA-Z0-9\-\s]*)<\/a>", RegexOptions.Compiled);
        private Regex _ingredientRegex = new Regex(@"<li itemprop=""ingredients"">(?<name>[a-zA-Z0-9\-\s\/äöü(),]*)<\/li>", RegexOptions.Compiled);
        private Regex _previewPath = new Regex(@"<img itemprop=""image"" class=""responsive-image recipe-main-image"" src=""(?<url>[a-zA-Z0-9\/\.\-\:]*)"" alt=""thumbnail image 1""\s*\/>", RegexOptions.Compiled);
            
        public Rzwde(IHtmlLoader uriLoader)
        {
            this.uriLoader = uriLoader;
        }

        public async Task<Recipe> Import(string url)
        {
            var html = await uriLoader.Load(url);

            var name= _nameRegex.Match(html).Groups["name"].Value;
            var match1 = _previewPath.Match(html);
            var preview = match1.Groups["url"].Value;
            var ingredientMatches = _ingredientRegex.Matches(html)
                .Select(match => match.Groups["name"].Value)
                .Select(value => value.Split("\r\n"))
                .Select(parts =>
                {
                    if (parts.Length == 3)
                    {
                        return new Ingredient(parts[1].Trim().RemoveMoreThenOneSpace(), 0);
                    }
                    if (parts.Length == 4)
                    {
                        return new Ingredient(parts[2].Trim().RemoveMoreThenOneSpace(), double.Parse(parts[1]), Unit.Stk);
                    }
                    if (parts.Length == 5)
                    {
                        return new Ingredient(parts[3].Trim().RemoveMoreThenOneSpace(), double.Parse(parts[1]), Parse(parts[2]));
                    }
                    throw new ArgumentException();
                })
                .ToArray();
            
            return new Recipe(name, url, new[]{new IngredientGroup("???", ingredientMatches), }, preview);
        }

        private static Unit Parse(string value)
        {
            value = value.Trim();
            if (Enum.TryParse(value, out Unit result))
            {
                return result;
            }

            if (value == "Stück") return Unit.Stk;
            if (value == "gestrichener Teelöffel") return Unit.TL;

            throw new Exception("Unkown value: "+value);
        }
    }

    public interface IImport
    {
        Task<Recipe> Import(string url);
    }
}
