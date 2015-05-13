using System;

namespace Recipes.Domain.Events
{
    public class RecipeAdded : Event
    {
        public RecipeAdded(Guid id, string title, string description) : base(id)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException(nameof(title));
            if (string.IsNullOrEmpty(description)) throw new ArgumentNullException(nameof(description));

            Description = description;
            Title = title;
        }

        public string Description { get; }

        public string Title { get; }
    }
}
