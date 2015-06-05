using Recipes.Domain.Events;
using System;

namespace Recipes.Domain.Aggregates
{
    public class RecipeAggregate : Aggregate
    {
        private string _title;
        private string _description;

        internal RecipeAggregate()
        {
            Handles<RecipeAdded>(OnRecipeAdded);
            Handles<RecipeTitleUpdated>(OnRecipeTitleUpdated);
            Handles<RecipeDescriptionUpdated>(OnRecipeDescriptionUpdated);
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

        private void OnRecipeAdded(RecipeAdded added)
        {
            Id = added.Id;
            _description = added.Description;
            _title = added.Title;
        }

        private void OnRecipeTitleUpdated(RecipeTitleUpdated titleUpdated)
        {
            _title = titleUpdated.Title;
        }

        private void OnRecipeDescriptionUpdated(RecipeDescriptionUpdated descUpdated)
        {
            _description = descUpdated.Description;
        } 
    }
}
