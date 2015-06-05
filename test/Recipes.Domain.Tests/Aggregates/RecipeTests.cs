﻿using Recipes.Domain.Events;
using System;
using System.Linq;
using Xunit;

namespace Recipes.Domain.Aggregates.Tests
{
    public class RecipeTests
    {
        [Fact]
        public void CreatingARecipe_WithInvalidValues_ThrowsAnException()
        {
            Assert.Throws(typeof(ArgumentNullException), () => RecipeAggregate.Create(Guid.Empty, "foo", "bar"));
            Assert.Throws(typeof(ArgumentNullException), () => RecipeAggregate.Create(Guid.NewGuid(), string.Empty, "bar"));
        }

        [Fact]
        public void CreatingARecipe_WithValidValues_SetsPropertiesCorrectlyAndAppliesAddRecipeEvent()
        {
            // Arrange
            var id = Guid.NewGuid();
            var title = "foo";
            var description = "bar";

            // Act
            var recipe = RecipeAggregate.Create(id, title, description);

            // Assert
            Assert.Equal(1, recipe.PendingChanges.Count());
                        
            var addedEvent = recipe.PendingChanges.FirstOrDefault() as RecipeAdded;
            Assert.NotNull(addedEvent);
            Assert.IsType(typeof(RecipeAdded), addedEvent);            
        }

        [Fact]
        public void UpdatingARecipe_WithANewTitle_SetsTitleAndAppliesTitleUpdatedEvent()
        {
            // Arrange
            var newTitle = "fizz";
            var recipe = RecipeAggregate.Create(Guid.NewGuid(), "foo", "bar");
            recipe.MarkChangesCommitted();

            // Act
            recipe.Update(newTitle, "bar");

            // Assert
            Assert.Equal(1, recipe.PendingChanges.Count());                      

            var titleUpdatedEvent = recipe.PendingChanges.FirstOrDefault() as RecipeTitleUpdated;
            Assert.NotNull(titleUpdatedEvent);
            Assert.IsType(typeof(RecipeTitleUpdated), titleUpdatedEvent);
        }

        [Fact]
        public void UpdatingARecipe_WithANewDescription_SetsDescriptionAndAppliesTitleUpdatedEvent()
        {
            // Arrange
            var newDesc = "bin";
            var recipe = RecipeAggregate.Create(Guid.NewGuid(), "foo", "bar");
            recipe.MarkChangesCommitted();

            // Act
            recipe.Update("foo", newDesc);

            // Assert
            Assert.Equal(1, recipe.PendingChanges.Count());
            
            var descUpdatedEvent = recipe.PendingChanges.FirstOrDefault() as RecipeDescriptionUpdated;
            Assert.NotNull(descUpdatedEvent);
            Assert.IsType(typeof(RecipeDescriptionUpdated), descUpdatedEvent);            
        }

        [Fact]
        public void UpdatingARecipe_WithTheSameTitleAndDescription_DoesNotUpdateTheRecipeOrCreateEvents()
        {
            // Arrange            
            var recipe = RecipeAggregate.Create(Guid.NewGuid(), "foo", "bar");
            recipe.MarkChangesCommitted();

            // Act - call udate with the same values that are already assigned
            recipe.Update("foo", "bar");

            // Assert - no update event should occur
            Assert.Equal(0, recipe.PendingChanges.Count());
        }

        [Fact]
        public void DeletingARecipe_AppliesARecipeDeletedEvent()
        {
            // Arrange
            var id = Guid.NewGuid();
            var recipe = RecipeAggregate.Create(id, "foo", "bar");
            recipe.MarkChangesCommitted();

            // Act
            recipe.Delete();

            // Assert
            Assert.Equal(1, recipe.PendingChanges.Count());

            var recipeDeletedEvents = recipe.PendingChanges.FirstOrDefault() as RecipeDeleted;
            Assert.NotNull(recipeDeletedEvents);
            Assert.IsType(typeof(RecipeDeleted), recipeDeletedEvents);
            Assert.Equal(id, recipeDeletedEvents.Id);
        }
    }
}