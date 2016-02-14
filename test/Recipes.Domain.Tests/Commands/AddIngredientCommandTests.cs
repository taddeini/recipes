using Recipes.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Recipes.Domain.Tests.Commands
{
    public class AddIngredientCommandTests
    {
        [Fact]
        public void CreatingAnAddIngredientCommand_WithOutRequiredParameters_ThrowsAnException()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new AddIngredientCommand(Guid.Empty, 1.0M, IngredientUnit.Cups, "foo"));
            Assert.Throws(typeof(ArgumentNullException), () => new AddIngredientCommand(Guid.NewGuid(), 0M, IngredientUnit.Cups, "foo"));            
            Assert.Throws(typeof(ArgumentNullException), () => new AddIngredientCommand(Guid.NewGuid(), 1.0M, IngredientUnit.Cups, string.Empty));            
        }

        [Fact]
        public void CreatingAnAddIngredientCommand_WithValidData_AddsTheIngredient()
        {
            // Arrange            
            var recipeId = Guid.NewGuid();
            var amount = 1.5M;
            var unit = IngredientUnit.Cups;
            var description = "crushed tomatoes";

            // Act
            var command = new AddIngredientCommand(recipeId, amount, unit, description);

            // Assert
            Assert.Equal(recipeId, command.RecipeId);
            Assert.Equal(amount, command.Amount);
            Assert.Equal(unit, command.Unit);
            Assert.Equal(description, command.Description);
        }
    }
}
