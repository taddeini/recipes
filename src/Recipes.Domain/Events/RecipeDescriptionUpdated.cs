using System;

namespace Recipes.Domain.Events
{
    public class RecipeDescriptionUpdated : Event
    {
        public RecipeDescriptionUpdated(Guid id, string description) : base(id)
        {
            if (string.IsNullOrEmpty(description)) throw new ArgumentNullException(nameof(description));
            Description = description;
        }

        public string Description { get; }
    }
}
