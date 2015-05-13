using System;

namespace Recipes.Domain.Events
{
    public class RecipeTitleUpdated : Event
    {
        public RecipeTitleUpdated(Guid id, string title) : base(id)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException(nameof(title));
            Title = title;            
        }

        public string Title { get; }
    }
}
