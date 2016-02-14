using EventStore.ClientAPI;
using Newtonsoft.Json;
using Recipes.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Domain.Repositories
{    
    public class EventStoreRepository<TAggregate> : IRepository<TAggregate> where TAggregate : Aggregate
    {
        private readonly IEventStoreConnection _connection;
        private readonly static string AGGREGATE_PREFIX = "recipe";
        private readonly static string QUALIFIED_NAME_FORMAT = "Recipes.Domain.Events.{0}, Recipes.Domain";

        public EventStoreRepository(IEventStoreConnection connection)
        {
            _connection = connection;            
        }
        
        public Task<TAggregate> Get(Guid id)
        {
            var streamName = $"{AGGREGATE_PREFIX}-{id.ToString()}";
            var allEvents = new List<Event>();
            var streamEvents = new List<ResolvedEvent>();

            StreamEventsSlice currentSlice;
            var nextSliceStart = StreamPosition.Start;
            do
            {
                currentSlice = _connection.ReadStreamEventsForwardAsync(streamName, nextSliceStart, 200, false).Result;
                nextSliceStart = currentSlice.NextEventNumber;
                streamEvents.AddRange(currentSlice.Events);
            }
            while (!currentSlice.IsEndOfStream);

            foreach (var streamEvent in streamEvents)
            {
                var eventData = Encoding.UTF8.GetString(streamEvent.Event.Data);
                var eventType = Type.GetType(string.Format(QUALIFIED_NAME_FORMAT, streamEvent.Event.EventType));
                var eventObject = JsonConvert.DeserializeObject(eventData, eventType) as Event;
                if (eventObject != null)
                {
                    allEvents.Add(eventObject);
                }                                    
            }

            return Task.FromResult(Aggregate.LoadFromHistory<TAggregate>(allEvents));
        }
        
        public void Save(TAggregate aggregate)
        {
            var streamName = $"{AGGREGATE_PREFIX}-{aggregate.Id.ToString()}";
            var eventData = new List<EventData>();

            foreach (var @event in aggregate.PendingChanges)
            {
                var data = new EventData(Guid.NewGuid(),
                    @event.GetType().Name,
                    true,
                    Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event)),
                    new byte[] { });
                eventData.Add(data);
            }

            _connection.AppendToStreamAsync(streamName, ExpectedVersion.Any, eventData);
            aggregate.MarkChangesCommitted();
        }    
    }
}
