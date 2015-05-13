using EventStore.ClientAPI;
using System.Net;

namespace Recipes.Domain.Common
{
    public class EventStoreConnectionFactory
    {
        public static IEventStoreConnection GetConnection(EventStoreSettings settings)
        {
            var ipAddress = Dns.GetHostAddresses(settings.HostName)[0];
            return EventStoreConnection.Create(new IPEndPoint(ipAddress, settings.Port));
        }
    }
}
