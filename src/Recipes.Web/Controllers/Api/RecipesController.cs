using Microsoft.AspNet.Mvc;
using Recipes.Domain.Commands;
using System;
using System.Net;

namespace Recipes.Controllers
{
    [Route("api/v1/[controller]")]
    public class RecipesController : Controller
    {
        private readonly IRecipeCommandHandler _recipeCommandHandler;

        public RecipesController(IRecipeCommandHandler recipeCommandHandler)
        {
            _recipeCommandHandler = recipeCommandHandler;
        }

        //// GET: api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // POST api/v1/recipes/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return new ObjectResult("value");
        }

        // POST api/v1/recipes
        [HttpPost]
        public IActionResult Post()
        {
            var recipeId = Guid.NewGuid();
			var addRecipeCommand = new AddRecipeCommand(recipeId, "Recipe Title 1", "Recipe Description 1");

            _recipeCommandHandler.Handle(addRecipeCommand);
            _recipeCommandHandler.Handle(new UpdateRecipeCommand(recipeId, "Recipe Title 2", "Recipe Description 2"));

            return new HttpStatusCodeResult((int)HttpStatusCode.Created);
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
