namespace BabyMemory.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
               return Redirect("/Children/All");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(Activity.Current?.Id ?? HttpContext.TraceIdentifier);
        }
    }
}