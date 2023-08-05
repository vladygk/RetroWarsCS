namespace RetroWars.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using RetroWars.Services.Data.Contracts;
using static Common.NotificationMessagesConstants;

public class GameController : Controller
{
    private readonly IGameService gameService;
    public GameController(IGameService gameService)
    {
        this.gameService = gameService;
    }
    public async Task<IActionResult> All()
    {
        var allGames = await this.gameService.GetAllGamesAsync();
        TempData[InformationMessage] = "Yaba daba du";
        return View(allGames);
    }
}

