using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Recipes.Contracts;
using System.Threading.Tasks;
using System.Xml;
using Recipes.App.Extensions;

namespace Recipes.App.Importing
{
    class Rzwde : IImport
    {
        private readonly IHtmlLoader uriLoader;
        private Regex _nameRegex = new Regex(@"<a href=""javascript:;"">(?<name>[a-zA-Z0-9\-\säöü]*)<\/a>", RegexOptions.Compiled);
        private Regex _ingredientRegex = new Regex(@"<li itemprop=""ingredients"">(?<name>[a-zA-Z0-9\-\s\/äöü(),%\*]*)<\/li>", RegexOptions.Compiled);
        private Regex _previewPath = new Regex(@"<img itemprop=""image"" class=""responsive-image recipe-main-image"" src=""(?<url>[a-zA-Z0-9\/\.\-\:]*)"" alt=""thumbnail image 1""\s*\/>", RegexOptions.Compiled);
            
        public Rzwde(IHtmlLoader uriLoader)
        {
            this.uriLoader = uriLoader;
        }

        public async Task<Recipe> Import(string url)
        {
            var html = await uriLoader.Load(url);
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            //var nameOfIngredientGroupOne = doc.DocumentNode.SelectNodes("//*[@id=\"ingredient-section\"]/div/p[2]")
            //    .FirstOrDefault()
            //    .InnerText;
            //var ingredientsOfGroupOne = doc.DocumentNode.SelectNodes("//*[@id=\"ingredient-section\"]/div/ul[1]")
            //    .FirstOrDefault()
            //    .InnerHtml;
            //var nameOfIngredientGroupTwo = doc.DocumentNode.SelectNodes("//*[@id=\"ingredient-section\"]/div/p[3]")
            //    .FirstOrDefault()
            //    .InnerText;
            //var ingredientsOfGroupTwo = doc.DocumentNode.SelectNodes("//*[@id=\"ingredient-section\"]/div/ul[2]")
            //    .FirstOrDefault()
            //    .InnerHtml;

            var names = doc.DocumentNode.SelectNodes("//*[@id=\"ingredient-section\"]/div/p").Skip(1).ToArray();
            var ingredient = doc.DocumentNode.SelectNodes("//*[@id=\"ingredient-section\"]/div/ul").ToArray();
            if (names.Length != ingredient.Length)
            {
                throw new Exception("Unable to import");
            }

           

            var name = _nameRegex.Match(html).Groups["name"].Value;
            var match1 = _previewPath.Match(html);
            var preview = match1.Groups["url"].Value;
            List<IngredientGroup> ingredientGroups = new List<IngredientGroup>();
            for (var index = 0; index < names.Length; index++)
            {
                var x = names[index];
                ingredientGroups.Add(Get(x.InnerHtml, ingredient[index].InnerHtml));
            }
            //var ingredientMatches = _ingredientRegex.Matches(html)
            //    .Select(match => match.Groups["name"].Value)
            //    .Select(value => value.Split("\r\n"))
            //    .Select(parts =>
            //    {
            //        if (parts.Length == 3)
            //        {
            //            return new Ingredient(parts[1].Trim().RemoveMoreThenOneSpace(), 0);
            //        }
            //        if (parts.Length == 4)
            //        {
            //            return new Ingredient(parts[3].Trim().RemoveMoreThenOneSpace(), double.Parse(parts[1]), Parse(parts[2]));
            //        }
            //        if (parts.Length == 5)
            //        {
            //            return new Ingredient(parts[3].Trim().RemoveMoreThenOneSpace(), double.Parse(parts[1]), Parse(parts[2]));
            //        }
            //        throw new ArgumentException();
            //    })
            //    .ToArray();

            return new Recipe(name, url, ingredientGroups.ToArray(), preview);
        }

        public IngredientGroup Get(string rawName, string content)
        {
            var ingredientMatches = _ingredientRegex.Matches(content)
                .Select(match => match.Groups["name"].Value)
                .Select(value => value.Replace("\r\n", "").RemoveMoreThenOneSpace().Trim())
                .ToArray();

            return new IngredientGroup(rawName.Trim(), ingredientMatches);
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
            if (value == "gehäufter Teelöffel") return Unit.TL;

            throw new Exception("Unkown value: "+value);
        }
    }

    public interface IImport
    {
        Task<Recipe> Import(string url);
    }
}
