using EventStore.ClientAPI;
using Microsoft.Framework.OptionsModel;
using Newtonsoft.Json;
using Recipes.Domain.Aggregates;
using Recipes.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recipes.Domain.Repositories
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
        public RecipeEventStore(IOptions<EventStoreSettings> esSettings)
        {
            if (esSettings == null) throw new ArgumentNullException(nameof(esSettings));
            //if (publisher == null) throw new ArgumentNullException("publisher");
            //_publisher = publisher;            

            _esConnection = EventStoreConnectionFactory.GetConnection(esSettings.Options);
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

            _esConnection.AppendToStreamAsync(streamName, ExpectedVersion.Any, eventData).Wait();
            //_publisher.Publish(@event);
            recipe.MarkChangesCommitted();
        }
    }
}
