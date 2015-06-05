using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Recipes.Domain.Queries
{
    public class MongoDBRecipeQueryProvider : IQueryProvider<RecipeQuery>
    {
        private readonly IMongoCollection<RecipeQuery> _recipeCollection;

        public MongoDBRecipeQueryProvider(IMongoDatabase database)
        {
            _recipeCollection = database.GetCollection<RecipeQuery>("recipe");
        }

        public async Task<IEnumerable<RecipeQuery>> Find(Expression<Func<RecipeQuery, bool>> predicate) =>
            await _recipeCollection.Find(predicate).ToListAsync();

        public async Task<RecipeQuery> Get(Guid id) =>
            await _recipeCollection.Find(rec => (rec.Id == id)).FirstOrDefaultAsync();

        public async Task<IEnumerable<RecipeQuery>> GetAll() =>
            await _recipeCollection.Find(rec => (rec.Id != Guid.Empty)).ToListAsync();
    }
}
