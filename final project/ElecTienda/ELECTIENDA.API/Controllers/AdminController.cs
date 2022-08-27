using Microsoft.AspNetCore.Mvc;


namespace ElECTIENDA.MVC
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View("Admin");
        }
    }
}
