namespace RetroWars.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using RetroWars.Web.ViewModels.Game;
using RetroWars.Services.Data.Contracts;
using static Common.NotificationMessagesConstants;

public class GameController : Controller
{
    private readonly IGameService gameService;
    private readonly IGenreService genreService;
    private readonly IPlatformService platformService;
    public GameController(IGameService gameService, IGenreService genreService, IPlatformService platformService)
    {

        this.gameService = gameService;
        this.genreService = genreService;
        this.platformService = platformService;
    }
    [HttpGet]
    public async Task<IActionResult> All()
    {
        var allGames = await this.gameService.GetAllGamesAsync();
        return this.View(allGames);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        GameFormModel formModel = new GameFormModel()
        {
            Platforms = await this.platformService.GetAllPlatformsAsync(),
            Genres = await this.genreService.GetAllGenresAsync()
        };
        return this.View(formModel);
    }

    [HttpPost]
    public async Task<IActionResult> Add(GameFormModel formModel)
    {
        await this.gameService.CreateGameAsync(formModel);

        return RedirectToAction("All");
    }
}

