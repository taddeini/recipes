using System;

namespace Recipes.Domain.Events
{
    public class RecipeDescriptionUpdated : Event
    {
        public RecipeDescriptionUpdated(Guid id, string description) : base(id)
        {            
            Description = description;
        }

        public string Description { get; }
    }
}
