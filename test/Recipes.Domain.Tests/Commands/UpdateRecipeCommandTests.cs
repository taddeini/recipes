using System;
using Xunit;

namespace Recipes.Domain.Commands
{
    public class UpdateRecipeCommandTests
    {
        [Fact]
        public void CreatingUpdateRecipeCommand_WithNoId_ThrowsAnException()
        {            
            Assert.Throws(typeof(ArgumentNullException), () => new UpdateRecipeCommand(Guid.Empty, "foo", "bar"));            
        }

        [Fact]
        public void CreatingUpdateRecipeCommand_WithValidValues_SetsPropertiesCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var title = "foo";
            var description = "bar";

            // Act
            var command = new UpdateRecipeCommand(id, title, description);

            // Assert
            Assert.Equal(id, command.Id);
            Assert.Equal(title, command.Title);
            Assert.Equal(description, command.Description);
        }
    }
}
