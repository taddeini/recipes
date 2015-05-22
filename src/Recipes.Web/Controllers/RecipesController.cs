using Microsoft.AspNet.Mvc;
using Recipes.Domain.Aggregates;
using Recipes.Domain.Queries;
using System;
using System.Linq;

namespace Recipes.Web.Controllers
{
    [Route("[controller]")]
    public class RecipesController : Controller
    {
        private readonly IQueryProvider<RecipeQuery> _recipeQueryProvider;

        public RecipesController(IQueryProvider<RecipeQuery> recipeQueryProvider)
        {
            _recipeQueryProvider = recipeQueryProvider;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var recipes = _recipeQueryProvider.GetAll().Result;
            return View(recipes);
        }

        [HttpGet("{urlTitle}")]
        public IActionResult Detail(string urlTitle)
        {
            var title = RecipeUtils.ConvertUrlToTitle(urlTitle);
            var recipe = _recipeQueryProvider
                .Find(rec => (rec.Title.ToLower() == title.ToLower()))
                .Result
                .FirstOrDefault();

            if (recipe == null)
            {
                return View(SharedViews.NotFound);
            }
            return View(recipe);
        }
    }
}
