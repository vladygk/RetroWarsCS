namespace RetroWars.Web.Controllers;

using RetroWars.Services.Data.Contracts;
using ViewModels.Platform;
using Microsoft.AspNetCore.Mvc;

using static Common.NotificationMessagesConstants;

public class PlatformController : AuthorizationController
{
    private readonly IPlatformService platformService;
    public PlatformController(IPlatformService platformService)
    {
        this.platformService = platformService;
    }
    public async Task<IActionResult> All()
    {
        IEnumerable<PlatformViewModel> viewModels = await this.platformService.GetAllPlatformsViewModelAsync();

        return View(viewModels);
    }

    public async Task<IActionResult> Details(string id)
    {
        try
        {
            PlatformViewModel viewModel = await this.platformService.GetOnePlatformsViewModelAsync(id);

            return this.View(viewModel);
        }
        catch
        {
            TempData[ErrorMessage] = "Error: Can't load platforms or/and categories";
            return RedirectToAction("All","Platform");
        }
    }
}

