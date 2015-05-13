using Recipes.Domain.Events;
using System;

namespace Recipes.Domain.Aggregates
{
    public class Recipe : Aggregate
    {
        public Recipe(Guid id, string title, string description)
        {
            // TODO: Move to factory method
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));
            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException(nameof(title));
            if (string.IsNullOrEmpty(description)) throw new ArgumentNullException(nameof(description));

            Id = id;
            Title = title;
            Description = description;

            Apply(new RecipeAdded(id, title, description));
        }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public void Update(string title, string description)
        {
            if (!string.IsNullOrEmpty(title) && (title != Title))
            {
                Title = title;
                Apply(new RecipeTitleUpdated(Id, title));
            }

            if (!string.IsNullOrEmpty(description) && (description != Description))
            {
                Description = description;
                Apply(new RecipeDescriptionUpdated(Id, description));
            }
        }
    }
}
