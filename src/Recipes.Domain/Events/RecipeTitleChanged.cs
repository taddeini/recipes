using System;

namespace Recipes.Domain.Events
{
    public class RecipeTitleChanged : Event
    {
        public RecipeTitleChanged(Guid id, string title) : base(id)
        {            
            Title = title;            
        }

        public string Title { get; }
    }
}
