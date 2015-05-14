using Recipes.Domain.Events;
using System;
using Xunit;

namespace Recipes.Domain.Tests.Events
{
    public class RecipeDeletedTests
    {
        [Fact]
        public void CreatingRecipeDeletedEvent_WithInvalidValues_ThrowsAnException()
        {                        
            Assert.Throws(typeof(ArgumentNullException), () => new RecipeDeleted(Guid.Empty));
        }

        [Fact]
        public void CreatingRecipeDeletedEvent_WithValidValues_SetsPropertiesCorrectly()
        {            
            var id = Guid.NewGuid();                                    
            var @event = new RecipeDeleted(id);                        
            Assert.Equal(id, @event.Id);            
        }
    }
}
