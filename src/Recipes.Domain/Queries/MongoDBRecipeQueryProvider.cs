using MongoDB.Driver;
using Recipes.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Recipes.Domain.Queries
{
    public class MongoDBRecipeQueryProvider : IQueryProvider<Aggregates.RecipeAggregate>
    {
        private readonly IMongoCollection<Aggregates.RecipeAggregate> _recipeCollection;

        public MongoDBRecipeQueryProvider(IMongoDatabase database)
        {
            _recipeCollection = database.GetCollection<Aggregates.RecipeAggregate>("recipe");
        }

        public async Task<IEnumerable<Aggregates.RecipeAggregate>> Find(Expression<Func<Aggregates.RecipeAggregate, bool>> predicate) =>
            await _recipeCollection.Find(predicate).ToListAsync();

        public async Task<Aggregates.RecipeAggregate> Get(Guid id) =>
            await _recipeCollection.Find(rec => (rec.Id == id)).FirstOrDefaultAsync();

        public async Task<IEnumerable<Aggregates.RecipeAggregate>> GetAll() =>
            await _recipeCollection.Find(rec => (rec.Id != Guid.Empty)).ToListAsync();
    }
}
