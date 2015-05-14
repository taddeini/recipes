using EventStore.ClientAPI;
using Microsoft.Framework.OptionsModel;
using Newtonsoft.Json;
using Recipes.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recipes.Domain.Common
{
    public interface IEventStore<TAggregate> where TAggregate : Aggregate
    {
        void Save(TAggregate aggregate);
    }

    public class RecipeEventStore : IEventStore<Recipe>
    {
        private readonly IEventStoreConnection _esConnection;
        //private readonly IEventPublisher _publisher;

        //public EventStoreRepository(IOptions<EventStoreSettings> settings, IEventPublisher publisher)
        public RecipeEventStore(IOptions<EventStoreSettings> settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            //if (publisher == null) throw new ArgumentNullException("publisher");
            //_publisher = publisher;            

            _esConnection = EventStoreConnectionFactory.GetConnection(settings.Options);
            _esConnection.ConnectAsync().Wait();
        }

        public void Save(Recipe recipe)
        {
            var aggregateType = recipe.GetType().Name;
            var streamName = $"{aggregateType.ToLower()}-{recipe.Id.ToString()}";
            var eventData = new List<EventData>();

            foreach (var @event in recipe.PendingChanges)
            {
                var data = new EventData(Guid.NewGuid(),
                    @event.GetType().Name,
                    true,
                    Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event)),
                    new byte[] { });
                eventData.Add(data);
            }

            _esConnection.AppendToStreamAsync(streamName, ExpectedVersion.Any, eventData);
            //_publisher.Publish(@event);
            recipe.MarkChangesCommitted();
        }
    }
}
