using System;

namespace Recipes.Domain.Events
{
    public class RecipeTitleUpdated : Event
    {
        public RecipeTitleUpdated(Guid id, string title) : base(id)
        {            
            Title = title;            
        }

        public string Title { get; }
    }
}
