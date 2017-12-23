using FakeItEasy;
using Recipes.App.Importing;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recipes.Contracts;

namespace Recipes.App.Tests.Importing.RzwdeSpecs
{
    public class RzwdeSpec : Spec
    {
        internal readonly Rzwde Sut;
        internal readonly IHtmlLoader Loader = A.Fake<IHtmlLoader>();


        protected RzwdeSpec()
        {
            Sut = new Rzwde(Loader);            
        }   
    }

    [TestClass]
    public class If_importing_simple_recipe : RzwdeSpec
    {
        private const string Source = @"https://www.rezeptwelt.de/saucendipsbrotaufstriche-rezepte/tomaten-oliven-feta-creme/cf40qq28-8b280-143865-cfcd2-hh3stpuw";
        private Recipe _recipe;
        protected override void EstablishContext()
        {
            var source = EmbeddedRessources.LoadString("Recipes.App.Tests.Importing.RzwdeSpecs.SimpleRecipe.txt");
            A.CallTo(() => Loader.Load(A<string>._))
                .Returns(Task.FromResult(source));
        }

        protected override void BecauseOf()
        {
            _recipe = Sut.Import(Source)
                .Result;
        }

        [TestMethod]
        public void Name_should_be_correct()
        {
            _recipe.Name.Should().Be("Tomaten Oliven Feta Creme");
        }

        [TestMethod]
        public void Source_should_be_correct()
        {
            _recipe.Source.Should().Be(Source);
        }

        [TestMethod]
        public void There_should_be_one_IngredientGroup()
        {
            _recipe.IngredientGroups.Length.Should().Be(1);

            _recipe.IngredientGroups[0].Name.Should().Be("CREME");
        }

        [TestMethod]
        public void There_should_be_7_Ingredients()
        {
            _recipe.IngredientGroups[0].Ingredients.Length.Should().Be(7);
        }

        [TestMethod]
        public void Should_PreviewPath_be_correct()
        {
            _recipe.PreviewPath.Should()
                .Be(
                    @"https://de.rc-cdn.community.thermomix.com/recipeimage/cf40qq28-8b280-143865-cfcd2-hh3stpuw/77165eb8-f1cd-4d09-a321-77dedd1c45c0/main/tomaten-oliven-feta-creme.jpg");
        }

        [TestMethod]
        public void Should_ingredient_one_be_correct()
        {
            _recipe.IngredientGroups[0].Ingredients[0].Should().Be("1 Knoblauchzehe");
        }
        [TestMethod]
        public void Should_ingredient_two_be_correct()
        {
            _recipe.IngredientGroups[0].Ingredients[1].Should().Be("7 getrocknete Tomaten, (eingelegte)");
        }
        [TestMethod]
        public void Should_ingredient_three_be_correct()
        {
            _recipe.IngredientGroups[0].Ingredients[2].Should().Be("8 güne Oliven");
        }
        [TestMethod]
        public void Should_ingredient_four_be_correct()
        {
            _recipe.IngredientGroups[0].Ingredients[3].Should().Be("200 g Fetakäse / Hirtenkäse");
        }
        [TestMethod]
        public void Should_ingredient_five_be_correct()
        {
            _recipe.IngredientGroups[0].Ingredients[4].Should().Be("100 g Frischkäse");
        }
        [TestMethod]
        public void Should_ingredient_six_be_correct()
        {
            _recipe.IngredientGroups[0].Ingredients[5].Should().Be("50 g Ajvar");;
        }
        [TestMethod]
        public void Should_ingredient_seven_be_correct()
        {
            _recipe.IngredientGroups[0].Ingredients[6].Should().Be("Kräuter");
        }
    }

    [TestClass]
    public class If_importing_recipe_with_multiple_ingredient_groups : RzwdeSpec
    {
        private const string Source = @"https://www.rezeptwelt.de/saucendipsbrotaufstriche-rezepte/tomaten-oliven-feta-creme/cf40qq28-8b280-143865-cfcd2-hh3stpuw";
        private Recipe _recipe;
        protected override void EstablishContext()
        {
            var source = EmbeddedRessources.LoadString("Recipes.App.Tests.Importing.RzwdeSpecs.RecipeWithMultipleIngredientGroups.txt");
            A.CallTo(() => Loader.Load(A<string>._))
                .Returns(Task.FromResult(source));
        }

        protected override void BecauseOf()
        {
            _recipe = Sut.Import(Source)
                .Result;
        }

        [TestMethod]
        public void Name_should_be_correct()
        {
            _recipe.Name.Should().Be("Königsberger Klopse");
        }

        [TestMethod]
        public void Source_should_be_correct()
        {
            _recipe.Source.Should().Be(Source);
        }

        [TestMethod]
        public void IngredientGroup_one_should_be_correct()
        {
            _recipe.IngredientGroups[0].Name.Should().Be("Klopse");
            _recipe.IngredientGroups[0].Ingredients.Should().ContainInOrder(
                "40 g Paniermehl, selbstgemacht",
                "110 g Zwiebeln, geviertelt",
                "1 Stück Ei, M",
                "1 gehäufter Teelöffel Salz",
                "1 gestrichener Teelöffel Pfeffer, schwarz, gemahlen",
                "600 g Hack, gemischt",
                "600 g Kartoffeln, Stücke",
                "800 g Wasser",
                "1 Päckchen Kn*** Boullion Huhn");
        }

        [TestMethod]
        public void IngredientGroup_two_should_be_correct()
        {
            _recipe.IngredientGroups[1].Name.Should().Be("Soße");
            _recipe.IngredientGroups[1].Ingredients.Should().ContainInOrder(
                "500 g Brühe, vom Klopse garen",
                "200 g Kochsahne, 15% Fett",
                "55 g Mehl",
                "10 g Kapernwasser",
                "0,25 TL Salz, vorsichtig, denn das Kapernwasser ist salzig",
                "0,5 TL Peffer, gemahlen",
                "40 g Kapern, abgetropft");
        }

        [TestMethod]
        public void Should_PreviewPath_be_correct()
        {
            _recipe.PreviewPath.Should()
                .Be(@"https://de.rc-cdn.community.thermomix.com/recipeimage/gezg3p9y-dd7cf-263159-cfcd2-3hpmf9v8/2af4d6a6-96a3-4f1e-a695-96f672da22d4/main/koenigsberger-klopse.jpg");
        }

        [TestMethod]
        public void There_should_be_two_IngredientsGroups()
        {
            _recipe.IngredientGroups.Length.Should().Be(2);
        }

    }
}
