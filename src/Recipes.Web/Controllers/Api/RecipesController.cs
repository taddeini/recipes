using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Logging;
using Recipes.Domain.Commands;
using Recipes.Domain.Queries;
using System;

namespace Recipes.Web.Api.Controllers
{    
    [Route("api/v1/[controller]")]
    public class RecipesController : Controller
    {
        private readonly IRecipeCommandHandler _recipeCommandHandler;
        private readonly IQueryProvider<RecipeQuery> _recipeQueryProvider;
        private readonly ILogger _logger;

        public RecipesController(IRecipeCommandHandler recipeCommandHandler, IQueryProvider<RecipeQuery> recipeQueryProvider, ILoggerFactory _loggerFactory)
        {
            _recipeCommandHandler = recipeCommandHandler;
            _recipeQueryProvider = recipeQueryProvider;
            _logger = _loggerFactory.CreateLogger(GetType().FullName);
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogDebug("Getting some recipes");
            var recipes = _recipeQueryProvider.GetAll();
            return new ObjectResult(recipes);
        }
        
        //[HttpGet("{id}")]
        //public IActionResult Get(Guid id)
        //{
        //    return new HttpNotFoundResult();            
        //}

        // POST api/v1/recipes
        [HttpPost]
        public IActionResult Post()
        {
            _logger.LogDebug("Saving some recipes");
            var recipeId = Guid.NewGuid();
            var addRecipeCommand = new AddRecipeCommand(recipeId, "Recipe Title 1", "Recipe Description 1");
            _recipeCommandHandler.Handle(addRecipeCommand);

            System.Threading.Thread.Sleep(3000);
            _recipeCommandHandler.Handle(new UpdateRecipeCommand(recipeId, "Recipe Title 1", "Recipe Description 2"));

            System.Threading.Thread.Sleep(3000);
            _recipeCommandHandler.Handle(new UpdateRecipeCommand(recipeId, "Recipe Title 2", "Recipe Description 2"));

            System.Threading.Thread.Sleep(3000);
            _recipeCommandHandler.Handle(new UpdateRecipeCommand(recipeId, "Recipe Title 2", "Recipe Description 3"));

            //System.Threading.Thread.Sleep(3000);
            //_recipeCommandHandler.Handle(new DeleteRecipeCommand(recipeId));

            //return new HttpStatusCodeResult((int)HttpStatusCode.Created);
            return Get();
        }

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
