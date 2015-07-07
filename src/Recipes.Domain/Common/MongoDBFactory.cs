using Microsoft.Framework.Configuration;
using MongoDB.Driver;

namespace Recipes.Domain.Common
{
    public class MongoDBFactory
    {        
        public static IMongoDatabase GetDatabase(IConfiguration settings)
        {
            var client = new MongoClient($"mongodb://{settings.Get("mongoDb:hostName")}:{int.Parse(settings.Get("mongoDb:port"))}");
            return client.GetDatabase(settings.Get("mongoDb:databaseName"));
        }
    }
}
