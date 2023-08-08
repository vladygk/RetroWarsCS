namespace RetroWars.Web.App.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;
using RetroWars.Services.Data.Contracts;
using RetroWars.Web.ViewModels.User;
using static Common.NotificationMessagesConstants;

public class UserController : AdminBaseController
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }
    [HttpGet]
    public async Task<IActionResult> All()
    {
        try
        {
            IEnumerable<UserViewModel> models = await this.userService.AllAsync();
            return View(models);
        }
        catch
        {
            TempData[ErrorMessage] = "Error: Can't load users.";
            return this.View(new List<UserViewModel>());
        }
    }

    [HttpGet]
    public async Task<IActionResult> MakeAdmin(string id)
    {
        try
        {
           await this.userService.MakeAdmin(id);

           return RedirectToAction("All", "User");
        }
        catch 
        {
            TempData[ErrorMessage] = "Error: Can't make admin.";
            return RedirectToAction("All","User");
        }
    }

}

