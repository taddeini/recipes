using Microsoft.Framework.OptionsModel;
using MongoDB.Driver;
using Recipes.Domain.Aggregates;
using Recipes.Domain.Common;
using System;

namespace Recipes.Domain.Repositories
{
    public interface IMongoDB<TAggregate> where TAggregate : Aggregate
    {
        TAggregate Get(Guid id);
    }

    public class RecipeMongoDB: IMongoDB<Recipe>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public RecipeMongoDB(IOptions<MongoDBSettings> mongoSettings)
        {
            _mongoDatabase = MongoDBFactory.GetDatabase(mongoSettings.Options);
        }
        
        public Recipe Get(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
