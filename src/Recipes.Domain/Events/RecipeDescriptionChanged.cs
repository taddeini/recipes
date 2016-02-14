using System;

namespace Recipes.Domain.Events
{
    public class RecipeDescriptionChanged : Event
    {
        public RecipeDescriptionChanged(Guid id, string description) : base(id)
        {            
            Description = description;
        }

        public string Description { get; }
    }
}
