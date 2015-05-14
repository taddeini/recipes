using System;

namespace Recipes.Domain.Commands
{
    public class AddRecipeCommand : Command
    {
        public AddRecipeCommand(Guid id, string title, string description)
        {
            // Id and Title are required, description is optional
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));
            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException(nameof(title));            

            Id = id;
            Title = title;
            Description = description;
        }

        public Guid Id { get; }

        public string Description { get; set; }

        public string Title { get; set; }
    }
}