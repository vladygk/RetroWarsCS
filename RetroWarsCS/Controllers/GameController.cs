using Microsoft.AspNetCore.Mvc;
using RetroWars.Services.Data.Contracts;

namespace RetroWars.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService gameService;
        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }
        public async Task<IActionResult > All()
        {
            var allGames = await this.gameService.GetAllGamesAsync();

            return View(allGames);
        }
    }
}
