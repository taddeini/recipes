using System;
using Xunit;

namespace Recipes.Domain.Commands
{
    public class DeleteRecipeCommandTests
    {
        [Fact]
        public void CreatingDeleteRecipeCommand_WithNoId_ThrowsAnException()
        {            
            Assert.Throws(typeof(ArgumentNullException), () => new DeleteRecipeCommand(Guid.Empty));            
        }

        [Fact]
        public void CreatingDeleteRecipeCommand_WithValidValues_SetsPropertiesCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            
            // Act
            var command = new DeleteRecipeCommand(id);

            // Assert
            Assert.Equal(id, command.Id);            
        }
    }
}
