using Recipes.Domain.Events;
using System;
using Xunit;

namespace Recipes.Domain.Tests.Events
{
    public class RecipeDescriptionUpdatedTests
    {
        [Fact]
        public void CreatingRecipeDescriptionUpdatedEvent_WithInvalidValues_ThrowsAnException()
        {            
            Assert.Throws(typeof(ArgumentNullException), () => new RecipeDescriptionUpdated(Guid.NewGuid(), string.Empty));
            Assert.Throws(typeof(ArgumentNullException), () => new RecipeDescriptionUpdated(Guid.Empty, "foo"));
        }

        [Fact]
        public void CreatingRecipeDescriptionUpdatedEvent_WithValidValues_SetsPropertiesCorrectly()
        {
            // Arrange    
            var id = Guid.NewGuid();
            var desc = "foo";
            
            // Act     
            var @event = new RecipeDescriptionUpdated(id, desc);
            
            // Assert            
            Assert.Equal(id, @event.Id);
            Assert.Equal(desc, @event.Description);            
        }
    }
}
