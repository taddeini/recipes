using Microsoft.AspNet.Mvc;

namespace Recipes.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {                        
            return new FileContentResult(System.IO.File.ReadAllBytes("dist/index.htm"), "text/html");
        }
    }
}