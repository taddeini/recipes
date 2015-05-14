using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using Microsoft.Framework.ConfigurationModel;
using Recipes.Domain.Common;
using Recipes.Projections.Projectors;
using System;
using System.Collections.Generic;

namespace Recipes.Projections.Host
{
    public class Program
    {
        private List<IProjector> _projectors = new List<IProjector>();

        public void Main(string[] args)
        {
            var settings = new ProjectorSettings();
            var connection = EventStoreConnectionFactory.GetConnection(settings.EventStoreSettings);

            // Subscribe to connection notifications
            connection.AuthenticationFailed += (s, a) => Console.WriteLine($"ES authentication failed: {a.Reason}");
            connection.Closed += (s, a) => Console.WriteLine($"ES connection closed: {a.Reason}");
            connection.Connected += (s, a) => Console.WriteLine($"ES connected: {a.RemoteEndPoint}");
            connection.Disconnected += (s, a) => Console.WriteLine($"ES disconnected: {a.RemoteEndPoint}");
            connection.ErrorOccurred += (s, a) => Console.WriteLine($"ES error: {a.Exception.Message}");
            connection.Reconnecting += (s, a) => Console.WriteLine($"ES reconnecting...");
            connection.ConnectAsync().Wait();

            // Subscribe to events
            connection.SubscribeToAllAsync(false,
                (sub, evt) => HandleEvent(evt, sub),
                (sub, reason, ex) => HandleEventDropped(sub, reason, ex),
                new UserCredentials("admin", "changeit")).Wait();

            RegisterProjectors(settings);
            
            Console.Read();
        }

        private void RegisterProjectors(ProjectorSettings settings)
        {            
            _projectors.Add(new MongoDBProjector(settings.MongoDBSettings));
        }

        private void HandleEvent(ResolvedEvent @event, EventStoreSubscription subscription)
        {
            _projectors.ForEach(proj => proj.HandleEvent(@event, subscription));
        }

        private void HandleEventDropped(EventStoreSubscription subscription, SubscriptionDropReason reason, Exception exception)
        {
            //TODO: Devlop some recovery plan
            Console.WriteLine($"Event Dropped: {reason}");
        }
    }
}
