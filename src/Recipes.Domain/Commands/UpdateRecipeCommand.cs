using System;

namespace Recipes.Domain.Commands
{
    public class UpdateRecipeCommand : Command
    {
        public UpdateRecipeCommand(Guid id, string title, string description)
        {            
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));
            
            Id = id;
            Title = title;
            Description = description;
        }

        public Guid Id { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }        
    }
}
