using System;

namespace Recipes.Domain.Events
{
    public class RecipeDeleted : Event
    {
        public RecipeDeleted(Guid id) : base(id)
        {
        }
    }
}
