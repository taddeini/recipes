using System;

namespace Recipes.Domain.Commands
{
    public class UpdateRecipeCommand : Command
    {
        public UpdateRecipeCommand(Guid id, string title, string description)
        {
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));
            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException(nameof(title));
            if (string.IsNullOrEmpty(description)) throw new ArgumentNullException(nameof(description));

            Id = id;
            Title = title;
            Description = description;
        }

        public Guid Id { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }        
    }
}
