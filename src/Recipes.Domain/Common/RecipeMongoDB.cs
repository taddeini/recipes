using Microsoft.Framework.OptionsModel;
using MongoDB.Driver;
using Recipes.Domain.Aggregates;
using System;
using System.Threading.Tasks;

namespace Recipes.Domain.Common
{
    public interface IMongoDB<TAggregate> where TAggregate : Aggregate
    {
        Task<TAggregate> Get(Guid id);
    }

    public class RecipeMongoDB: IMongoDB<Recipe>
    {
        private readonly IMongoCollection<Recipe> _recipeCollection;

        public RecipeMongoDB(IOptions<MongoDBSettings> settings)
        {
            _recipeCollection = MongoDBFactory
                .GetDatabase(settings.Options)
                .GetCollection<Recipe>("recipe");
        }

        public Task<Recipe> Get(Guid id) => 
            _recipeCollection.Find(rec => (rec.Id == id)).FirstOrDefaultAsync();
    }
}
