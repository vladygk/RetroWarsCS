namespace RetroWars.Web.App.Controllers;

using RetroWars.Services.Data.Contracts;
using ViewModels.Platform;
using Microsoft.AspNetCore.Mvc;

using static Common.NotificationMessagesConstants;
using Microsoft.AspNetCore.Authorization;
using static Common.GeneralApplicationConstants;

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

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return this.View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(PlatformFormModel model)
    {
     bool success =  await this.platformService.CreatePlatformAsync(model);

        if (success)
        {
            TempData[SuccessMessage] = "Success: Platfrom added!";
            return RedirectToAction("All", "Platform");
        }else
        {
            TempData[ErrorMessage] = "Error: Can't add platform, plase try again.";
            return RedirectToAction("All", "Platform");
        }
    }

    [HttpGet]
    [Authorize(Roles = AdminRoleName)]
    public async Task<IActionResult> Delete(string id)
    {
        bool cantDelete = await this.platformService.CheckIfPlatformIsAssociatedWithGames(id);

        if (cantDelete)
        {
            TempData[ErrorMessage] = "Error: Can't delete platform, it's associated with a game!";
            return RedirectToAction("All", "Platform");
        }

        bool success = await this.platformService.DeletePlatformAsync(id);


        if (success)
        {
            TempData[SuccessMessage] = "Success: Platfrom deleted.";
            return RedirectToAction("All", "Platform");
        }
        else
        {
            TempData[ErrorMessage] = "Error: Can't delete platform, plase try again.";
            return RedirectToAction("All", "Platform");
        }
    }
}

