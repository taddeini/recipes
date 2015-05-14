using Microsoft.Framework.ConfigurationModel;
using Recipes.Domain.Common;

namespace Recipes.Projections.Host
{
    public class ProjectorSettings
    {
        public ProjectorSettings()
        {
            var config = new Configuration().AddJsonFile("config.json");

            EventStoreSettings = new EventStoreSettings
            {
                HostName = config.Get("eventStore:hostName"),
                Port = int.Parse(config.Get("eventStore:port"))
            };

            MongoDBSettings = new MongoDBSettings
            {
                HostName = config.Get("mongoDb:hostName"),
                Port = int.Parse(config.Get("mongoDb:port")),
                DatabaseName = config.Get("mongoDb:databaseName")
            };
        }

        public EventStoreSettings EventStoreSettings { get; }

        public MongoDBSettings MongoDBSettings { get; }
    }
}
