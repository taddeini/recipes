using Recipes.Domain.Events;
using System;
using Xunit;

namespace Recipes.Domain.Tests.Events
{
    public class RecipeTitleChangedTests
    {
        [Fact]
        public void CreatingRecipeTitleChangedEvent_WithInvalidValues_ThrowsAnException()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new RecipeTitleChanged(Guid.Empty, "foo"));
        }

        [Fact]
        public void CreatingRecipeTitleChangedEvent_WithValidValues_SetsPropertiesCorrectly()
        {
            // Arrange    
            var id = Guid.NewGuid();
            var title = "foo";

            // Act
            var @event = new RecipeTitleChanged(id, title);

            // Assert            
            Assert.Equal(id, @event.Id);
            Assert.Equal(title, @event.Title);
        }
    }
}
