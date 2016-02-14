using EventStore.ClientAPI;
using Microsoft.Framework.Configuration;
using System.Net;

namespace Recipes.Domain.Common
{
    public class EventStoreConnectionFactory
    {
        public static IEventStoreConnection GetConnection(IConfiguration settings)
        {
            //var ipAddress = Dns.GetHostAddresses(settings.Get("eventStore:hostName"))[0];
            //var endpoint = new IPEndPoint(ipAddress, int.Parse(settings.Get("eventStore:port")));
            //var connection = EventStoreConnection.Create(endpoint);

            //connection.ConnectAsync().Wait();

            //return connection;
            return null;
        }
    }
}
