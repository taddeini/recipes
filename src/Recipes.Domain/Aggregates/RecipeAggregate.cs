using Recipes.Domain.Events;
using System;
using System.Collections.Generic;

namespace Recipes.Domain.Aggregates
{
    public class RecipeAggregate : Aggregate
    {
        private string _title;

        private string _description;

        private RecipeAggregate()
        {
        }

        public static RecipeAggregate Load(IEnumerable<Event> history)
        {
            var recipe = new RecipeAggregate();
            foreach (var @event in history)
            {
                recipe.ApplyEvent(@event);
            }            
            return recipe;
        }

        public static RecipeAggregate Create(Guid id, string title, string description)
        {
            var recipe = new RecipeAggregate();
            recipe.ApplyEvent(new RecipeAdded(id, title, description));
            return recipe;
        }

        public void Update(string title, string description)
        {
            if (title != _title)
            {                
                ApplyEvent(new RecipeTitleUpdated(Id, title));
            }

            if (description != _description)
            {                
                ApplyEvent(new RecipeDescriptionUpdated(Id, description));
            }
        }
        
        public void Delete()
        {
            ApplyEvent(new RecipeDeleted(Id));
        }    

        protected override void ApplyEvent(Event @event)
        {
            //TODO: this method is dumb
            base.ApplyEvent(@event);

            switch (@event.GetType().Name)
            {
                case nameof(RecipeAdded):
                    Id = @event.Id;
                    _description = ((RecipeAdded)@event).Description;
                    _title = ((RecipeAdded)@event).Title;
                    break;

                case nameof(RecipeTitleUpdated):
                    _title = ((RecipeTitleUpdated)@event).Title;                    
                    break;

                case nameof(RecipeDescriptionUpdated):
                    _description = ((RecipeDescriptionUpdated)@event).Description;
                    break;
            }
        }
    }
}
