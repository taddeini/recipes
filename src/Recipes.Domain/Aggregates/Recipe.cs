using Recipes.Domain.Events;
using System;

namespace Recipes.Domain.Aggregates
{
    public class Recipe : Aggregate
    {
        public Recipe(Guid id, string title, string description) : base()
        {
            // Id and Title are required
            // TODO: Move to factory method
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));
            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException(nameof(title));
            if (description == null) throw new ArgumentNullException(nameof(description));

            Id = id;
            Title = title;
            Description = description;

            ApplyEvent(new RecipeAdded(id, title, description));
        }
        
        public string Title { get; private set; }

        public string Description { get; private set; }

        public void Update(string title, string description)
        {
            if (title != Title)
            {
                Title = title;
                ApplyEvent(new RecipeTitleUpdated(Id, title));
            }

            if (description != Description)
            {
                Description = description;
                ApplyEvent(new RecipeDescriptionUpdated(Id, description));
            }
        }

        public void Delete()
        {
            ApplyEvent(new RecipeDeleted(Id));
        }
    }
}
