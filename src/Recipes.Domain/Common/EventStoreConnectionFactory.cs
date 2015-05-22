using EventStore.ClientAPI;
using Microsoft.Framework.ConfigurationModel;
using System.Net;

namespace Recipes.Domain.Common
{
    public class EventStoreConnectionFactory
    {
        public static IEventStoreConnection GetConnection(IConfiguration settings)
        {
            var ipAddress = Dns.GetHostAddresses(settings.Get("eventStore:hostName"))[0];
            return EventStoreConnection.Create(new IPEndPoint(ipAddress, int.Parse(settings.Get("eventStore:port"))));
        }
    }
}
