using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Domain.Commands
{
    public class AddIngredientCommand : Command
    {
        public AddIngredientCommand(Guid recipeId, decimal amount, IngredientUnit unit, string description)
        {            
            if (recipeId == Guid.Empty) throw new ArgumentNullException(nameof(recipeId));
            if (amount == 0) throw new ArgumentNullException(nameof(amount));
            if (string.IsNullOrEmpty(description)) throw new ArgumentNullException(nameof(description));

            RecipeId = recipeId;
            Amount = amount;
            Unit = unit;
            Description = description;
        }

        public Guid RecipeId { get; }

        public decimal Amount { get; set; }

        public IngredientUnit Unit { get; set; }

        public string Description { get; set; }        
    }
}
