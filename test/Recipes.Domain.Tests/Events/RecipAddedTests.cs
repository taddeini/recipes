using Recipes.Domain.Events;
using System;
using Xunit;

namespace Recipes.Domain.Tests.Events
{
    public class RecipAddedEventTests
    {
        [Fact]
        public void CreatingRecipeAddedEvent_WithInvalidValues_ThrowsAnException()
        {            
            Assert.Throws(typeof(ArgumentNullException), () => new RecipeAdded(Guid.Empty, "foo", "bar"));
            Assert.Throws(typeof(ArgumentNullException), () => new RecipeAdded(Guid.NewGuid(), "foo", string.Empty));
            Assert.Throws(typeof(ArgumentNullException), () => new RecipeAdded(Guid.NewGuid(), string.Empty, "bar"));
        }

        [Fact]
        public void CreatingRecipeAddedEvent_WithValidValues_SetsPropertiesCorrectly()
        {
            // Arrange            
            var id = Guid.NewGuid();
            var title = "foo";
            var description = "bar";

            // Act
            var @event = new RecipeAdded(id, title, description);

            // Assert            
            Assert.Equal(id, @event.Id);
            Assert.Equal(title, @event.Title);
            Assert.Equal(description, @event.Description);
        }
    }
}
