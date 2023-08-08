namespace RetroWars.Web.App.Controllers;

using Microsoft.AspNetCore.Mvc;


public class HomeController : Controller
{

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int statusCode)
    {
        return View();
    }
}
