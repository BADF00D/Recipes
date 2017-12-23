using Nancy;
using Recipes.Contracts;

namespace Recipes.App.Modules
{
    public class RecipeModule : NancyModule
    {
        //private readonly Recipe[] Recipes =
        //{
        //    new Recipe(
        //        "Tomaten Oliven Feta Creme",
        //        "@https://www.rezeptwelt.de/saucendipsbrotaufstriche-rezepte/tomaten-oliven-feta-creme/cf40qq28-8b280-143865-cfcd2-hh3stpuw",
        //        new[]
        //        {
        //            new IngredientGroup("Chreme", new[]
        //            {
        //                new Ingredient("Knoblauchzehe", 1, Unit.Stk),
        //                new Ingredient("getrocknete Tomaten", 7, Unit.Stk),
        //                new Ingredient("grüne Oliven", 8, Unit.Stk),
        //                new Ingredient("Fetakäse/Hirtenkäse", 200, Unit.g),
        //                new Ingredient("Frischkäse", 100, Unit.g),
        //                new Ingredient("Ajvar", 50, Unit.g),
        //                new Ingredient("Kräuter", 0, Unit.none)
        //            })
        //        },
        //        @"https://de.rc-cdn.community.thermomix.com/recipeimage/cf40qq28-8b280-143865-cfcd2-hh3stpuw/77165eb8-f1cd-4d09-a321-77dedd1c45c0/main/tomaten-oliven-feta-creme.jpg"),
        //    new Recipe(
        //        "KARTOFFEL - GEMÜSECREMESUPPE",
        //        @"https://www.rezeptwelt.de/suppen-rezepte/kartoffel-gemuesecremesuppe/k5e3qv1w-c4972-931574-cfcd2-gxjislmf",
        //        new[]
        //        {
        //            new IngredientGroup("", new[]
        //            {
        //                new Ingredient("Zwiebel", .5, Unit.Stk),
        //                new Ingredient("Margarine", 60, Unit.g),
        //                new Ingredient("Kartoffeln, geschält, in Stücken", 400, Unit.g),
        //                new Ingredient("Brokkoli, in Stücken", 200, Unit.g),
        //                new Ingredient("kleine Karotte", 1, Unit.Stk),
        //                new Ingredient("Zucchini", 250, Unit.g)
        //            })
        //        },
        //        @"https://de.rc-cdn.community.thermomix.com/recipeimage/k5e3qv1w-c4972-931574-cfcd2-gxjislmf/072d2993-24cf-446d-8db7-3d09315196eb/main/kartoffel-gemuesecremesuppe.jpg")
        //};

        public RecipeModule()
        {
            //Get("/recipe/list", _ => Response.AsJson(Recipes));
        }
    }
}