using Recipes.Domain.Events;
using System;
using Xunit;

namespace Recipes.Domain.Tests.Events
{
    public class RecipeDescriptionChangedTests
    {
        [Fact]
        public void CreatingRecipeDescriptionChangedEvent_WithInvalidValues_ThrowsAnException()
        {                        
            Assert.Throws(typeof(ArgumentNullException), () => new RecipeDescriptionChanged(Guid.Empty, "foo"));
        }

        [Fact]
        public void CreatingRecipeDescriptionChangedEvent_WithValidValues_SetsPropertiesCorrectly()
        {
            // Arrange    
            var id = Guid.NewGuid();
            var desc = "foo";
            
            // Act     
            var @event = new RecipeDescriptionChanged(id, desc);
            
            // Assert            
            Assert.Equal(id, @event.Id);
            Assert.Equal(desc, @event.Description);            
        }
    }
}
