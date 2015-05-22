using EventStore.ClientAPI;
using Newtonsoft.Json;
using Recipes.Domain.Aggregates;
using Recipes.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Domain.Repositories
{
    public class EventStoreRecipeRepository : IRepository<RecipeAggregate>
    {
        private readonly IEventStoreConnection _connection;
        private readonly static string AGGREGATE_PREFIX = "recipe";

        public EventStoreRecipeRepository(IEventStoreConnection connection)
        {
            _connection = connection;
            _connection.ConnectAsync().Wait();
        }

        public Task<RecipeAggregate> Get(Guid id)
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
                switch (streamEvent.Event.EventType)
                {
                    case nameof(RecipeAdded):
                        allEvents.Add(JsonConvert.DeserializeObject<RecipeAdded>(eventData));
                        break;
                    case nameof(RecipeDescriptionUpdated):
                        allEvents.Add(JsonConvert.DeserializeObject<RecipeDescriptionUpdated>(eventData));
                        break;
                    case nameof(RecipeTitleUpdated):
                        allEvents.Add(JsonConvert.DeserializeObject<RecipeTitleUpdated>(eventData));
                        break;
                    case nameof(RecipeDeleted):
                        allEvents.Add(JsonConvert.DeserializeObject<RecipeDeleted>(eventData));
                        break;
                }
            }

            return Task.FromResult(RecipeAggregate.Load(allEvents));
        }

        public void Save(RecipeAggregate recipe)
        {
            var streamName = $"{AGGREGATE_PREFIX}-{recipe.Id.ToString()}";
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

            _connection.AppendToStreamAsync(streamName, ExpectedVersion.Any, eventData);
            recipe.MarkChangesCommitted();
        }
    }
}
