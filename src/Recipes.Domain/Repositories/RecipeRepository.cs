using Recipes.Domain.Aggregates;
using System;

namespace Recipes.Domain.Repositories
{
    public class RecipeRepository : IRepository<Recipe>
    {
        IEventStore<Recipe> _recipeEventStore;
        IMongoDB<Recipe> _recipeMongoDB;

        public RecipeRepository(IEventStore<Recipe> recipeEventStore, IMongoDB<Recipe> recipeMongoDB)
        {
            _recipeEventStore = recipeEventStore;
            _recipeMongoDB = recipeMongoDB;
        }

        public Recipe Get(Guid id) => _recipeMongoDB.Get(id);

        public void Save(Recipe recipe)
        {
            _recipeEventStore.Save(recipe);
        }
    }
}
