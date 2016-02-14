﻿using Recipes.Domain;
using Recipes.Domain.Queries;
using Recipes.Domain.Repositories;
using System;
using System.Linq;

namespace Recipes.Domain.Commands
{
    public interface IRecipeCommandHandler :
        ICommandHandler<AddRecipeCommand>,
        ICommandHandler<UpdateRecipeCommand>,
        ICommandHandler<DeleteRecipeCommand>,
        ICommandHandler<AddIngredientCommand>
    { }

    public class RecipeCommandHandler : IRecipeCommandHandler
    {
        private readonly IRepository<RecipeAggregate> _recipeRepository;
        private readonly IQueryProvider<RecipeQuery> _recipeQueryProvider;

        public RecipeCommandHandler(IRepository<RecipeAggregate> recipeRepository, IQueryProvider<RecipeQuery> recipeQueryProvider)
        {
            if (recipeRepository == null) throw new ArgumentNullException(nameof(recipeRepository));
            if (recipeQueryProvider == null) throw new ArgumentNullException(nameof(recipeQueryProvider));

            _recipeRepository = recipeRepository;
            _recipeQueryProvider = recipeQueryProvider;
        }

        public void Handle(AddRecipeCommand command)
        {
            var duplicateRecipe = _recipeQueryProvider
                .Find(rec => (rec.Title.ToLower() == command.Title.ToLower()))
                .Result
                .FirstOrDefault();

            if (duplicateRecipe != null)
            {
                throw new InvalidOperationException("Recipe with that title already exists");
            }

            var recipe = RecipeAggregate.Create(command.Id, command.Title, command.Description);
            _recipeRepository.Save(recipe);
        }

        public void Handle(UpdateRecipeCommand command)
        {
            var recipe = _recipeRepository.Get(command.Id).Result;
            if (recipe != null)
            {
                // Verify title is unique before updating
                var duplicateRecipe = _recipeQueryProvider 
                    .Find(rec => (rec.Title.ToLower() == command.Title.ToLower()))
                    .Result
                    .FirstOrDefault();

                if (duplicateRecipe == null)
                {
                    recipe.Update(command.Title, command.Description);
                    _recipeRepository.Save(recipe);
                }
            }
        }

        public void Handle(DeleteRecipeCommand command)
        {
            var recipe = _recipeRepository.Get(command.Id).Result;
            if (recipe != null)
            {
                recipe.Delete();
                _recipeRepository.Save(recipe);
            }
        }

        public void Handle(AddIngredientCommand command)
        {
            //var recipe = _recipeRepository.Get(command.Id).Result;
            //if (recipe != null)
            //{
            //    recipe.AddIngredient(command);
            //    _recipeRepository.Save(recipe);
            //}
        }
    }
}
