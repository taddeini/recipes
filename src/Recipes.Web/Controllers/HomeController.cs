using Microsoft.AspNet.Mvc;

namespace Recipes.Web.Controllers
{
    public class HomeController : Controller
    {        
        public IActionResult Index()
        {
            return RedirectToActionPermanent("index", "recipes");
        }
    }
}
