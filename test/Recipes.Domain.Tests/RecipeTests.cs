using Recipes.Domain.Aggregates;
using Recipes.Domain.Events;
using System;
using System.Linq;
using Xunit;

namespace Recipes.Domain.Tests
{        
    public class RecipeTests
    {
        [Fact]
        public void CreatingARecipe_WithInvalidValues_ThrowsAnException()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new Recipe(Guid.Empty, "foo", "bar"));
            Assert.Throws(typeof(ArgumentNullException), () => new Recipe(Guid.NewGuid(), "foo", string.Empty));
            Assert.Throws(typeof(ArgumentNullException), () => new Recipe(Guid.NewGuid(), string.Empty, "bar"));
        }

        [Fact]
        public void CreatingARecipe_WithValidValues_SetsPropertiesCorrectlyAndAppliesAddRecipeEvent()
        {
            // Arrange
            var id = Guid.NewGuid();
            var title = "foo";
            var description = "bar";

            // Act
            var recipe = new Recipe(id, title, description);

            // Assert - verify aggregate and event creation
            Assert.Equal(id, recipe.Id);
            Assert.Equal(title, recipe.Title);
            Assert.Equal(description, recipe.Description);
            Assert.Equal(1, recipe.PendingChanges.Count());

            var addedEvent = recipe.PendingChanges.FirstOrDefault() as RecipeAdded;
            Assert.NotNull(addedEvent);
            Assert.IsType(typeof(RecipeAdded), addedEvent);            
            Assert.Equal(title, addedEvent.Title);
            Assert.Equal(description, addedEvent.Description);
        }

        [Fact]
        public void UpdatingARecipe_WithANewTitle_SetsTitleAndAppliesTitleUpdatedEvent()
        {
            // Arrange
            var newTitle = "fizz";
            var recipe = new Recipe(Guid.NewGuid(), "foo", "bar");
            recipe.MarkChangesCommitted();

            // Act
            recipe.Update(newTitle, string.Empty);

            // Assert - verify title update and event creation
            Assert.Equal(newTitle, recipe.Title);
            Assert.Equal(1, recipe.PendingChanges.Count());

            var titleUpdatedEvent = recipe.PendingChanges.FirstOrDefault() as RecipeTitleUpdated;
            Assert.NotNull(titleUpdatedEvent);
            Assert.IsType(typeof(RecipeTitleUpdated), titleUpdatedEvent);
            Assert.Equal(newTitle, titleUpdatedEvent.Title);
        }

        [Fact]
        public void UpdatingARecipe_WithANewDescription_SetsDescriptionAndAppliesTitleUpdatedEvent()
        {
            // Arrange
            var newDesc = "bin";
            var recipe = new Recipe(Guid.NewGuid(), "foo", "bar");
            recipe.MarkChangesCommitted();

            // Act
            recipe.Update(string.Empty, newDesc);

            // Assert - verify description update and event creation
            Assert.Equal(newDesc, recipe.Description);
            Assert.Equal(1, recipe.PendingChanges.Count());

            var descUpdatedEvent = recipe.PendingChanges.FirstOrDefault() as RecipeDescriptionUpdated;
            Assert.NotNull(descUpdatedEvent);
            Assert.IsType(typeof(RecipeDescriptionUpdated), descUpdatedEvent);
            Assert.Equal(newDesc, descUpdatedEvent.Description);
        }

        [Fact]
        public void UpdatingARecipe_WithTheSameTitleAndDescription_DoesNotUpdateTheRecipeOrCreateEvents()
        {
            // Arrange            
            var recipe = new Recipe(Guid.NewGuid(), "foo", "bar");
            recipe.MarkChangesCommitted();

            // Act - call udate with the same values that are already assigned
            recipe.Update("foo", "bar");

            // Assert - no update event should occur
            Assert.Equal(0, recipe.PendingChanges.Count());            
        }
    }
}
