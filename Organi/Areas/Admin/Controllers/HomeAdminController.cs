using Microsoft.AspNetCore.Mvc;

namespace Organi.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
