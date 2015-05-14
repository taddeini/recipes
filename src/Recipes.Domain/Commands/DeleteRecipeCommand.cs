using System;

namespace Recipes.Domain.Commands
{
    public class DeleteRecipeCommand : Command
    {
        public DeleteRecipeCommand(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));

            Id = id;
        }

        public Guid Id { get; }
    }
}
