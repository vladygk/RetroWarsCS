namespace RetroWars.Web.App.Controllers;

using Microsoft.AspNetCore.Mvc;


public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    [HttpGet]
   
    public IActionResult Error(int statusCode)
    {
        if(statusCode == 404)
        {
            return View("Error404");

        }


        return View();
    }
    [HttpGet]
    public IActionResult Error401()
    {
        
        return View();
    }

}
