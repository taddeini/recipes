using Xunit;

namespace Recipes.Web.Tests
{
    public class RecipeUtilsTests
    {
        [Fact]
        public void ConvertUrlToTitle_GivenAUrlTitle_ConvertsToStandardTitle()
        {
            // Arrange
            var urlTitle = "chicken-foo-gumbo-bar";

            // Act
            var title = RecipeUtils.ConvertUrlToTitle(urlTitle);

            // Assert
            Assert.Equal("Chicken Foo Gumbo Bar", title);
        }

        [Fact]
        public void ConvertTitleToUrl_GivenATitle_ConvertsToAUrlTitle()
        {
            // Arrange
            var title = "Foo Bar Frank And Beans";

            // Act
            var urlTitle = RecipeUtils.ConvertTitleToUrl(title);

            // Assert
            Assert.Equal("foo-bar-frank-and-beans", urlTitle);
        }
    }
}
