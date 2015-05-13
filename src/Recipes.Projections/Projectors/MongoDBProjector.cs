using EventStore.ClientAPI;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Recipes.Domain.Aggregates;
using Recipes.Domain.Events;
using System;
using System.Text;

namespace Recipes.Projections.Projectors
{
    public class MongoDBProjector : IProjector
    {
        private readonly IMongoDatabase _database;

        public MongoDBProjector(IMongoDatabase database)
        {
            if (database == null) throw new ArgumentNullException(nameof(IMongoDatabase));
            _database = database;            
        }

        public void HandleEvent(ResolvedEvent resolvedEvent, EventStoreSubscription subscription)
        {            
            HandleEventInternal(resolvedEvent.Event);
        }

        public void HandleEventDropped(EventStoreSubscription subscription, SubscriptionDropReason reason, Exception exception)
        {
            Console.WriteLine($"MongoDBProjector Dropped: {reason}");
        }

        private void HandleEventInternal(RecordedEvent recordedEvent)
        {
            var eventData = Encoding.UTF8.GetString(recordedEvent.Data);

            switch (recordedEvent.EventType)
            {
                case nameof(RecipeAdded):
                    var recipeAdded = JsonConvert.DeserializeObject<RecipeAdded>(eventData);
                    var recipe = new Recipe(recipeAdded.Id, recipeAdded.Title, recipeAdded.Description);
                    var recipeDocument = BsonDocument.Parse(JsonConvert.SerializeObject(recipe));



                    break;

                case nameof(RecipeDescriptionUpdated):
                    var descUpdated = JsonConvert.DeserializeObject<RecipeAdded>(eventData);
                    break;

                case nameof(RecipeTitleUpdated):
                    var titleUpdated = JsonConvert.DeserializeObject<RecipeAdded>(eventData);
                    break;
            }
        }
    }
}
