using FakeItEasy;
using Recipes.App.Importing;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recipes.Contracts;

namespace Recipes.App.Tests.Importing.RzwdeSpecs
{
    [TestClass]
    public class RzwdeSpec : Spec
    {
        internal Rzwde Sut;
        internal IHtmlLoader Loader = A.Fake<IHtmlLoader>();
        private Recipe _recipe;

        public RzwdeSpec()
        {
            Sut = new Rzwde(Loader);
            var source = EmbeddedRessources.LoadString("Recipes.App.Tests.Importing.RzwdeSpecs.Recipe1.txt");
            A.CallTo(() => Loader.Load(A<string>._))
                .Returns(Task.FromResult(source));
        }
        protected override void  BecauseOf()
        {
            _recipe = Sut.Import(@"https://www.rezeptwelt.de/saucendipsbrotaufstriche-rezepte/tomaten-oliven-feta-creme/cf40qq28-8b280-143865-cfcd2-hh3stpuw")
                .Result;
        }

        [TestMethod]
        public void Name_should_be_correct()
        {
            _recipe.Name.Should().Be("Tomaten Oliven Feta Creme");
        }

        [TestMethod]
        public void Description_should_be_null()
        {
            _recipe.Description.Should().Be(null);
        }

        [TestMethod]
        public void There_should_be_7_Ingredients()
        {
            _recipe.Ingredients.Length.Should().Be(7);
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
            _recipe.Ingredients[0].Amount.Should().Be(1);
            _recipe.Ingredients[0].Name.Should().Be("Knoblauchzehe");
            _recipe.Ingredients[0].Unit.Should().Be(Unit.Stk);
        }
        [TestMethod]
        public void Should_ingredient_two_be_correct()
        {
            _recipe.Ingredients[1].Amount.Should().Be(7);
            _recipe.Ingredients[1].Name.Should().Be("getrocknete Tomaten, (eingelegte)");
            _recipe.Ingredients[1].Unit.Should().Be(Unit.Stk);
        }
        [TestMethod]
        public void Should_ingredient_three_be_correct()
        {
            _recipe.Ingredients[2].Amount.Should().Be(8);
            _recipe.Ingredients[2].Name.Should().Be("güne Oliven");
            _recipe.Ingredients[2].Unit.Should().Be(Unit.Stk);
        }
        [TestMethod]
        public void Should_ingredient_four_be_correct()
        {
            _recipe.Ingredients[3].Amount.Should().Be(200);
            _recipe.Ingredients[3].Name.Should().Be("Fetakäse / Hirtenkäse");
            _recipe.Ingredients[3].Unit.Should().Be(Unit.g);
        }
        [TestMethod]
        public void Should_ingredient_five_be_correct()
        {
            _recipe.Ingredients[4].Amount.Should().Be(100);
            _recipe.Ingredients[4].Name.Should().Be("Frischkäse");
            _recipe.Ingredients[4].Unit.Should().Be(Unit.g);
        }
        [TestMethod]
        public void Should_ingredient_six_be_correct()
        {
            _recipe.Ingredients[5].Amount.Should().Be(50);
            _recipe.Ingredients[5].Name.Should().Be("Ajvar");
            _recipe.Ingredients[5].Unit.Should().Be(Unit.g);
        }
        [TestMethod]
        public void Should_ingredient_seven_be_correct()
        {
            _recipe.Ingredients[6].Amount.Should().Be(0);
            _recipe.Ingredients[6].Name.Should().Be("Kräuter");
            _recipe.Ingredients[6].Unit.Should().Be(Unit.none);
        }
    }
}
