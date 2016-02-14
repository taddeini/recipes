using Recipes.Domain.Events;
using System;
using System.Collections.Generic;

namespace Recipes.Domain
{
    public class RecipeAggregate : Aggregate
    {
        private string _title;
        private string _description;
        private IList<Ingredient> _ingredients;

        internal RecipeAggregate()
        {
            Handles<RecipeAdded>(OnRecipeAdded);
            Handles<RecipeTitleChanged>(OnRecipeTitleUpdated);
            Handles<RecipeDescriptionChanged>(OnRecipeDescriptionUpdated);
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
                ApplyEvent(new RecipeTitleChanged(Id, title));
            }

            if (description != _description)
            {
                ApplyEvent(new RecipeDescriptionChanged(Id, description));
            }
        }

        public void Delete()
        {
            ApplyEvent(new RecipeDeleted(Id));
        }

        public void AddIngredient(Ingredient ingredient)
        {

        }

        private void OnRecipeAdded(RecipeAdded added)
        {
            Id = added.Id;
            _description = added.Description;
            _title = added.Title;
        }

        private void OnRecipeTitleUpdated(RecipeTitleChanged titleUpdated)
        {
            _title = titleUpdated.Title;
        }

        private void OnRecipeDescriptionUpdated(RecipeDescriptionChanged descUpdated)
        {
            _description = descUpdated.Description;
        }
    }
}
