using EventStore.ClientAPI;
using Recipes.Domain.Common;
using Recipes.Projections.Projectors;
using System;

namespace Recipes.Projections.Host
{
    public class Program
    {
        public void Main(string[] args)
        {
            //TODO: add to config
            var mongoSettings = new MongoDBSettings
            {
                HostName = "dev.recipes.com",
                Port = 27017,
                DatabaseName = "recipes"
            };
            var mongoDb = MongoDBFactory.GetDatabase(mongoSettings);

            //TODO: add to config
            var eventStoreSettings = new EventStoreSettings()
            {
                HostName = "dev.recipes.com",
                Port = 1113
            };

            var connection = EventStoreConnectionFactory.GetConnection(eventStoreSettings);            
            connection.AuthenticationFailed += (s, a) => Console.WriteLine($"ES authentication failed: {a.Reason}...");
            connection.Closed += (s, a) => Console.WriteLine($"ES connection closed: {a.Reason}...");
            connection.Connected += (s, a)  => Console.WriteLine($"ES connected: {a.RemoteEndPoint}..."); 
            connection.Disconnected += (s, a) => Console.WriteLine($"ES disconnected: {a.RemoteEndPoint}...");
            connection.ErrorOccurred += (s, a) => Console.WriteLine($"ES error: {a.Exception.Message}...");
            connection.Reconnecting += (s, a) => Console.WriteLine($"ES reconnecting...");

            connection.ConnectAsync().Wait();

            var registrar = new ProjectorRegistrar(connection);            
            registrar.Register(new MongoDBProjector(mongoDb));
            
            Console.Read();
        }        
    }
}
