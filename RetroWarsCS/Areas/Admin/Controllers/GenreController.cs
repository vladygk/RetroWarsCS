namespace RetroWars.Web.App.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;
using RetroWars.Services.Data.Contracts;
using RetroWars.Web.ViewModels.Genre;
using static Common.NotificationMessagesConstants;
using static Common.GeneralApplicationConstants;

using System.Data;

public class GenreController : AdminBaseController
{
    private readonly IGenreService genreService;

    public GenreController(IGenreService genreService)
    {
        this.genreService = genreService;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        try
        {
            var allGenres = await this.genreService.GetAllGenresAsync();

            IEnumerable<GenreAdminViewModel> model = allGenres.Select(x => new GenreAdminViewModel() { Id = x.Id, Name = x.Name });

            return this.View(model);
        }
        catch (Exception)
        {

            TempData[ErrorMessage] = "Error: Can't load genres";
            return RedirectToAction("All", "Genre", new { Area = AdminAreaName });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        bool cantDelete = await this.genreService.CheckIfGenreIsAssociatedWithGames(id);

        if (cantDelete)
        {
            TempData[ErrorMessage] = "Error: Can't delete genre, it's associated with a game!";
            return RedirectToAction("All", "Genre", new { Admin = AdminAreaName });
        }

        bool success = await this.genreService.DeleteGenreAsync(id);

        if (success)
        {
            TempData[SuccessMessage] = "Success: Genre deleted!";
            return RedirectToAction("All", "Genre", new { Admin = AdminAreaName });
        }

        TempData[ErrorMessage] = "Error: Can't delete genre, please try again.";
        return RedirectToAction("All", "Genre", new { Admin = AdminAreaName });
    }
}
