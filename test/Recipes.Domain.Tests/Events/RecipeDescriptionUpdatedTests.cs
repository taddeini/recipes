using Recipes.Domain.Events;
using System;
using Xunit;

namespace Recipes.Domain.Tests.Events
{
    public class RecipeTitleUpdatedTests
    {
        [Fact]
        public void CreatingRecipeTitleUpdatedEvent_WithInvalidValues_ThrowsAnException()
        {            
            Assert.Throws(typeof(ArgumentNullException), () => new RecipeTitleUpdated(Guid.NewGuid(),string.Empty));
            Assert.Throws(typeof(ArgumentNullException), () => new RecipeTitleUpdated(Guid.Empty, "foo"));
        }

        [Fact]
        public void CreatingRecipeTitleUpdatedEvent_WithValidValues_SetsPropertiesCorrectly()
        {
            // Arrange    
            var id = Guid.NewGuid();
            var title = "foo";

            // Act
            var @event = new RecipeTitleUpdated(id, title);

            // Assert            
            Assert.Equal(id, @event.Id);
            Assert.Equal(title, @event.Title);            
        }
    }
}
