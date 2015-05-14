using Recipes.Domain.Aggregates;
using Recipes.Domain.Repositories;
using System;

namespace Recipes.Domain.Commands
{
    public interface IRecipeCommandHandler : 
        ICommandHandler<AddRecipeCommand>, 
        ICommandHandler<UpdateRecipeCommand>,
        ICommandHandler<DeleteRecipeCommand>
    {
    }

    public class RecipeCommandHandler : IRecipeCommandHandler
    {
        private readonly IRepository<Recipe> _recipeRepository;

        public RecipeCommandHandler(IRepository<Recipe> recipeRepository)
        {
            if (recipeRepository == null) throw new ArgumentNullException(nameof(recipeRepository));
            _recipeRepository = recipeRepository;
        }
        
        public void Handle(AddRecipeCommand command)
        {            
            var recipe = new Recipe(command.Id, command.Title, command.Description);
            _recipeRepository.Save(recipe);            
        }

        public void Handle(UpdateRecipeCommand command)
        {            
            var recipe = _recipeRepository.Get(command.Id).Result;
            if (recipe == null)
            {
                return;
            }

            recipe.Update(command.Title, command.Description);
            _recipeRepository.Save(recipe);
        }

        public void Handle(DeleteRecipeCommand command)
        {
            var recipe = _recipeRepository.Get(command.Id).Result;
            if (recipe == null)
            {
                return;
            }

            recipe.Delete();
            _recipeRepository.Save(recipe);
        }
    }
}
