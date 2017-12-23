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

            _recipe.IngredientGroups[0].Name.Should().Be("Chreme");
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
            _recipe.IngredientGroups[0].Ingredients[0].Amount.Should().Be(1);
            _recipe.IngredientGroups[0].Ingredients[0].Name.Should().Be("Knoblauchzehe");
            _recipe.IngredientGroups[0].Ingredients[0].Unit.Should().Be(Unit.Stk);
        }
        [TestMethod]
        public void Should_ingredient_two_be_correct()
        {
            _recipe.IngredientGroups[0].Ingredients[1].Amount.Should().Be(7);
            _recipe.IngredientGroups[0].Ingredients[1].Name.Should().Be("getrocknete Tomaten, (eingelegte)");
            _recipe.IngredientGroups[0].Ingredients[1].Unit.Should().Be(Unit.Stk);
        }
        [TestMethod]
        public void Should_ingredient_three_be_correct()
        {
            _recipe.IngredientGroups[0].Ingredients[2].Amount.Should().Be(8);
            _recipe.IngredientGroups[0].Ingredients[2].Name.Should().Be("güne Oliven");
            _recipe.IngredientGroups[0].Ingredients[2].Unit.Should().Be(Unit.Stk);
        }
        [TestMethod]
        public void Should_ingredient_four_be_correct()
        {
            _recipe.IngredientGroups[0].Ingredients[3].Amount.Should().Be(200);
            _recipe.IngredientGroups[0].Ingredients[3].Name.Should().Be("Fetakäse / Hirtenkäse");
            _recipe.IngredientGroups[0].Ingredients[3].Unit.Should().Be(Unit.g);
        }
        [TestMethod]
        public void Should_ingredient_five_be_correct()
        {
            _recipe.IngredientGroups[0].Ingredients[4].Amount.Should().Be(100);
            _recipe.IngredientGroups[0].Ingredients[4].Name.Should().Be("Frischkäse");
            _recipe.IngredientGroups[0].Ingredients[4].Unit.Should().Be(Unit.g);
        }
        [TestMethod]
        public void Should_ingredient_six_be_correct()
        {
            _recipe.IngredientGroups[0].Ingredients[5].Amount.Should().Be(50);
            _recipe.IngredientGroups[0].Ingredients[5].Name.Should().Be("Ajvar");
            _recipe.IngredientGroups[0].Ingredients[5].Unit.Should().Be(Unit.g);
        }
        [TestMethod]
        public void Should_ingredient_seven_be_correct()
        {
            _recipe.IngredientGroups[0].Ingredients[6].Amount.Should().Be(0);
            _recipe.IngredientGroups[0].Ingredients[6].Name.Should().Be("Kräuter");
            _recipe.IngredientGroups[0].Ingredients[6].Unit.Should().Be(Unit.none);
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
            _recipe.Name.Should().Be("Tomaten Oliven Feta Creme");
        }

        [TestMethod]
        public void Source_should_be_correct()
        {
            _recipe.Source.Should().Be(Source);
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
            _recipe.IngredientGroups[0].Ingredients[0].Amount.Should().Be(1);
            _recipe.IngredientGroups[0].Ingredients[0].Name.Should().Be("Knoblauchzehe");
            _recipe.IngredientGroups[0].Ingredients[0].Unit.Should().Be(Unit.Stk);
        }
        [TestMethod]
        public void Should_ingredient_two_be_correct()
        {
            _recipe.IngredientGroups[0].Ingredients[1].Amount.Should().Be(7);
            _recipe.IngredientGroups[0].Ingredients[1].Name.Should().Be("getrocknete Tomaten, (eingelegte)");
            _recipe.IngredientGroups[0].Ingredients[1].Unit.Should().Be(Unit.Stk);
        }
        [TestMethod]
        public void Should_ingredient_three_be_correct()
        {
            _recipe.IngredientGroups[0].Ingredients[2].Amount.Should().Be(8);
            _recipe.IngredientGroups[0].Ingredients[2].Name.Should().Be("güne Oliven");
            _recipe.IngredientGroups[0].Ingredients[2].Unit.Should().Be(Unit.Stk);
        }
        [TestMethod]
        public void Should_ingredient_four_be_correct()
        {
            _recipe.IngredientGroups[0].Ingredients[3].Amount.Should().Be(200);
            _recipe.IngredientGroups[0].Ingredients[3].Name.Should().Be("Fetakäse / Hirtenkäse");
            _recipe.IngredientGroups[0].Ingredients[3].Unit.Should().Be(Unit.g);
        }
        [TestMethod]
        public void Should_ingredient_five_be_correct()
        {
            _recipe.IngredientGroups[0].Ingredients[4].Amount.Should().Be(100);
            _recipe.IngredientGroups[0].Ingredients[4].Name.Should().Be("Frischkäse");
            _recipe.IngredientGroups[0].Ingredients[4].Unit.Should().Be(Unit.g);
        }
        [TestMethod]
        public void Should_ingredient_six_be_correct()
        {
            _recipe.IngredientGroups[0].Ingredients[5].Amount.Should().Be(50);
            _recipe.IngredientGroups[0].Ingredients[5].Name.Should().Be("Ajvar");
            _recipe.IngredientGroups[0].Ingredients[5].Unit.Should().Be(Unit.g);
        }
        [TestMethod]
        public void Should_ingredient_seven_be_correct()
        {
            _recipe.IngredientGroups[0].Ingredients[6].Amount.Should().Be(0);
            _recipe.IngredientGroups[0].Ingredients[6].Name.Should().Be("Kräuter");
            _recipe.IngredientGroups[0].Ingredients[6].Unit.Should().Be(Unit.none);
        }
    }
}
