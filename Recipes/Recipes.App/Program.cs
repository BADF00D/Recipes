using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Nancy.Owin;
using Nancy;

namespace Recipes.App
{
    class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }

    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseOwin(x => x.UseNancy());
        }
    }

    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get("/", args => "Hello World, it's Nancy on .NET Core");
        }
    }

    public class RecipeModule : NancyModule
    {
        private readonly Recipe[] Recipes = new Recipe[]
        {
            new Recipe(
                "Tomaten Oliven Feta Creme",
                string.Empty,
                new []{
                    new Ingedient("Knoblauchzehe", 1,Unit.Stk),
                    new Ingedient("getrocknete Tomaten", 7,Unit.Stk),
                    new Ingedient("grüne Oliven", 8, Unit.Stk),
                    new Ingedient("Fetakäse/Hirtenkäse", 200, Unit.g),
                    new Ingedient("Frischkäse", 100, Unit.g),
                    new Ingedient("Ajvar", 50, Unit.g),
                    new Ingedient("Kräuter", 0, Unit.none),
                },
                @"https://de.rc-cdn.community.thermomix.com/recipeimage/cf40qq28-8b280-143865-cfcd2-hh3stpuw/77165eb8-f1cd-4d09-a321-77dedd1c45c0/main/tomaten-oliven-feta-creme.jpg"),
             new Recipe(
                "KARTOFFEL - GEMÜSECREMESUPPE",
                string.Empty,
                new []{
                    new Ingedient("Zwiebel", .5,Unit.Stk),
                    new Ingedient("Margarine", 60,Unit.g),
                    new Ingedient("Kartoffeln, geschält, in Stücken", 400, Unit.g),
                    new Ingedient("Brokkoli, in Stücken", 200, Unit.g),
                    new Ingedient("kleine Karotte", 1, Unit.Stk),
                    new Ingedient("Zucchini", 250, Unit.g),
                },
                @"https://de.rc-cdn.community.thermomix.com/recipeimage/k5e3qv1w-c4972-931574-cfcd2-gxjislmf/072d2993-24cf-446d-8db7-3d09315196eb/main/kartoffel-gemuesecremesuppe.jpg"),

        };

        public RecipeModule()         {
            Get("/recipe/list", _ => Response.AsJson(Recipes));

        }
    }

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

    public class Ingedient
    {
        public double Amount { get; }
        public Unit Unit { get; }
        public string Name { get; }
        public Ingedient(string name, double amount, Unit unit = Unit.none)
        {
            Amount = amount;
            Unit = unit;
            Name = name;
        }

        public override string ToString()
        {
            if(Unit == Unit.none)
            {
                if(Amount == 0)
                {
                    return $"{Name}";
                }
                return $"{Amount} {Name}";
            }
            return $"{Amount}{Unit.ToString()} {Name}";
        }
    }

    public enum Unit
    {
        ml,
        l,
        g,
        kg,
        Pk,
        EL,
        TL,
        Stk,
        none,
    }
}
