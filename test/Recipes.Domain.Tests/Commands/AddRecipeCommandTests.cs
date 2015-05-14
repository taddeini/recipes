using System;
using Xunit;

namespace Recipes.Domain.Commands
{
	public class AddRecipeCommandTests
	{		
		[Fact]
		public void CreatingAddRecipeCommand_WithInvalidValues_ThrowsAnException()
		{            
            Assert.Throws(typeof(ArgumentNullException), () => new AddRecipeCommand(Guid.Empty, "foo", string.Empty));            
			Assert.Throws(typeof(ArgumentNullException), () => new AddRecipeCommand(Guid.NewGuid(), string.Empty, null));            
        }

        [Fact]
        public void CreatingAddRecipeCommand_WithValidValues_SetsPropertiesCorrectly()
		{
			// Arrange
			var id = Guid.NewGuid();
			var title = "foo";
            var description = "bar";

			// Act
			var command = new AddRecipeCommand(id, title, description);

			// Assert
			Assert.Equal(id, command.Id);
			Assert.Equal(title, command.Title);
            Assert.Equal(description, command.Description);
		}
	}
}
