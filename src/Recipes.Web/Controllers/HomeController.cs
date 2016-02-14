using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Configuration;

namespace Recipes.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}