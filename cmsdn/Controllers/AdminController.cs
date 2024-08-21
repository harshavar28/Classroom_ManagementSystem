using Microsoft.AspNetCore.Mvc;

namespace cmsdn.Controllers
{
    public class AdminController : Controller
    {
        // GET: /Admin/
        public IActionResult Index()
        {
            return View();
        }

        // Additional actions for managing users, settings, etc., can be added here.
    }
}
