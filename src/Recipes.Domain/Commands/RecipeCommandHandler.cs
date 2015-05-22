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
        private readonly IRepository<RecipeAggregate> _recipeRepository;

        public RecipeCommandHandler(IRepository<RecipeAggregate> recipeRepository)
        {
            if (recipeRepository == null) throw new ArgumentNullException(nameof(recipeRepository));
            _recipeRepository = recipeRepository;
        }
        
        public void Handle(AddRecipeCommand command)
        {            
            var recipe = RecipeAggregate.Create(command.Id, command.Title, command.Description);
            _recipeRepository.Save(recipe);            
        }

        public void Handle(UpdateRecipeCommand command)
        {            
            var recipe = _recipeRepository.Get(command.Id).Result;        
            recipe.Update(command.Title, command.Description);
            _recipeRepository.Save(recipe);
        }

        public void Handle(DeleteRecipeCommand command)
        {
            var recipe = _recipeRepository.Get(command.Id).Result;
            recipe.Delete();
            _recipeRepository.Save(recipe);
        }
    }
}
