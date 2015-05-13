using MongoDB.Driver;

namespace Recipes.Domain.Common
{
    public class MongoDBFactory
    {
        public static IMongoDatabase GetDatabase(MongoDBSettings settings)
        {
            var client = new MongoClient($"mongodb://{settings.HostName}:{settings.Port}");
            return client.GetDatabase(settings.DatabaseName);
        }
    }
}
