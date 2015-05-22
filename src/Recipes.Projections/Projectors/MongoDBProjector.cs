using EventStore.ClientAPI;
using MongoDB.Driver;
using Newtonsoft.Json;
using Recipes.Domain.Aggregates;
using Recipes.Domain.Common;
using Recipes.Domain.Events;
using Recipes.Domain.Queries;
using System;
using System.Text;

namespace Recipes.Projections.Projectors
{
    public class MongoDBProjector : IProjector
    {
        private readonly IMongoCollection<RecipeQuery> _recipes;

        public MongoDBProjector(IMongoDatabase database)
        {
            _recipes = database.GetCollection<RecipeQuery>("recipe");
        }

        public void HandleEvent(ResolvedEvent resolvedEvent, EventStoreSubscription subscription)
        {
            var recordedEvent = resolvedEvent.Event;
            var eventData = Encoding.UTF8.GetString(recordedEvent.Data);

            switch (recordedEvent.EventType)
            {
                case nameof(RecipeAdded):
                    var recipeAdded = JsonConvert.DeserializeObject<RecipeAdded>(eventData);
                    var recipe = new RecipeQuery
                    {
                        Id = recipeAdded.Id,
                        Title = recipeAdded.Title,
                        Description = recipeAdded.Description
                    };
                    _recipes.InsertOneAsync(recipe);

                    ConsoleIt(recordedEvent);
                    break;

                case nameof(RecipeDescriptionUpdated):
                    var descUpdated = JsonConvert.DeserializeObject<RecipeDescriptionUpdated>(eventData);

                    _recipes.UpdateOneAsync(rec => (rec.Id == descUpdated.Id),
                        Builders<RecipeQuery>.Update.Set(rec => rec.Description, descUpdated.Description));

                    ConsoleIt(recordedEvent);
                    break;

                case nameof(RecipeTitleUpdated):
                    var titleUpdated = JsonConvert.DeserializeObject<RecipeTitleUpdated>(eventData);

                    _recipes.UpdateOneAsync(rec => (rec.Id == titleUpdated.Id),
                        Builders<RecipeQuery>.Update.Set(rec => rec.Title, titleUpdated.Title));

                    ConsoleIt(recordedEvent);
                    break;

                case nameof(RecipeDeleted):
                    var recipeDeleted = JsonConvert.DeserializeObject<RecipeDeleted>(eventData);

                    _recipes.DeleteOneAsync(rec => (rec.Id == recipeDeleted.Id));

                    ConsoleIt(recordedEvent);
                    break;
            }
        }

        private void ConsoleIt(RecordedEvent recordedEvent)
        {
            Console.WriteLine($"MongoDBProjector Handling: {recordedEvent.EventType}");
        }
    }
}
