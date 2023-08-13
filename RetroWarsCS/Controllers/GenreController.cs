namespace RetroWars.Web.App.Controllers;
using Microsoft.AspNetCore.Mvc;
using RetroWars.Services.Data.Contracts;
using RetroWars.Web.ViewModels.Genre;
using static Common.NotificationMessagesConstants;

public class GenreController : AuthorizationController
{
    private readonly IGenreService genreService;
    public GenreController(IGenreService genreService)
    {
        this.genreService = genreService;
    }

    [HttpGet]
    public IActionResult Add()
    {
        return this.View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(GenreFormModel model)
    {
        if (ModelState.IsValid)
        {
            bool success = await this.genreService.CreateGenreAsync(model);

            if (success)
            {
                TempData[SuccessMessage] = "Success: Genre added!";
                return RedirectToActionPermanent("All", "Game");
            }
            else
            {
                TempData[ErrorMessage] = "Error: Can't add genre, plase try again.";
                return RedirectToAction("All", "Game");
            }
        }
        TempData[ErrorMessage] = "Error: Invalid input data";
        return RedirectToAction("All", "Game");
    }
}
