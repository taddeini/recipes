using Microsoft.Framework.OptionsModel;
using MongoDB.Driver;
using Recipes.Domain.Aggregates;
using Recipes.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Recipes.Domain.Queries
{
    public class RecipeQueryProvider : IQueryProvider<Recipe>
    {
        private readonly IMongoCollection<Recipe> _recipeCollection;

        public RecipeQueryProvider(IOptions<MongoDBSettings> settings)
        {
            _recipeCollection = MongoDBFactory
                .GetDatabase(settings.Options)
                .GetCollection<Recipe>("recipe");
        }

        public IEnumerable<Recipe> Find(Func<Recipe, bool> predicate) => 
            _recipeCollection.Find(rec => predicate.Invoke(rec)).ToListAsync().Result.AsEnumerable();

        public Recipe Get(Guid id) => 
            _recipeCollection.Find(rec => (rec.Id == id)).FirstOrDefaultAsync().Result;

        public IEnumerable<Recipe> GetAll() =>
            _recipeCollection.Find(rec => (rec.Id != Guid.Empty)).ToListAsync().Result.AsEnumerable();

    }
}
