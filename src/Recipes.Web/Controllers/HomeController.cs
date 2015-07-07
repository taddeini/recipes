using Microsoft.AspNet.Mvc;

namespace Recipes.Web.Controllers
{
    public class HomeController : Controller
    {        
        [HttpGet]
        public IActionResult Index()
        {
            return new FilePathResult("Index.htm", "text/html");
        }
    }
}
