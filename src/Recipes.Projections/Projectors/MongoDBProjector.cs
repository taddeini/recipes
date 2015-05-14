using EventStore.ClientAPI;
using Microsoft.Framework.OptionsModel;
using MongoDB.Driver;
using Newtonsoft.Json;
using Recipes.Domain.Aggregates;
using Recipes.Domain.Common;
using Recipes.Domain.Events;
using System;
using System.Text;

namespace Recipes.Projections.Projectors
{
    public class MongoDBProjector : IProjector
    {        
        private readonly IMongoCollection<Recipe> _recipes;

        public MongoDBProjector(MongoDBSettings settings)
        {                        
            _recipes = MongoDBFactory
                .GetDatabase(settings)
                .GetCollection<Recipe>("recipe");
        }

        public void HandleEvent(ResolvedEvent resolvedEvent, EventStoreSubscription subscription)
        {
            var recordedEvent = resolvedEvent.Event;
            var eventData = Encoding.UTF8.GetString(recordedEvent.Data);
            switch (recordedEvent.EventType)
            {
                case nameof(RecipeAdded):
                    var recipeAdded = JsonConvert.DeserializeObject<RecipeAdded>(eventData);
                    var recipe = new Recipe(recipeAdded.Id, recipeAdded.Title, recipeAdded.Description);
                    _recipes.InsertOneAsync(recipe);

                    ConsoleIt(recordedEvent);
                    break;

                case nameof(RecipeDescriptionUpdated):
                    var descUpdated = JsonConvert.DeserializeObject<RecipeDescriptionUpdated>(eventData);

                    _recipes.UpdateOneAsync(rec => (rec.Id == descUpdated.Id),
                        Builders<Recipe>.Update.Set(rec => rec.Description, descUpdated.Description));

                    ConsoleIt(recordedEvent);
                    break;

                case nameof(RecipeTitleUpdated):
                    var titleUpdated = JsonConvert.DeserializeObject<RecipeTitleUpdated>(eventData);

                    _recipes.UpdateOneAsync(rec => (rec.Id == titleUpdated.Id),
                        Builders<Recipe>.Update.Set(rec => rec.Title, titleUpdated.Title));

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
