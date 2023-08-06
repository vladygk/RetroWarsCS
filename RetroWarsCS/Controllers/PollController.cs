namespace RetroWars.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using RetroWars.Services.Data.Contracts;
using ViewModels.Poll;
using ViewModels.Game;
using static Common.NotificationMessagesConstants;

public class PollController : Controller
{
    private readonly IPollService pollService;
    private readonly IGameService gameService;
    public PollController(IPollService pollService, IGameService gameService)
    {
        this.pollService = pollService;
        this.gameService = gameService;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        try
        {
            IEnumerable<PollViewModel> viewModels = await this.pollService.GetAllPollsAsync();
            return this.View(viewModels);
        }
        catch
        {
            TempData[ErrorMessage] = "Error: Can't load polls.";
            return this.View(new List<PollViewModel>());
        }
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        try
        {
            PollFormModel model = new PollFormModel()
            {
                Games = await this.gameService.GetAllPollSelectGameViewModels()
            };
            return this.View(model);
        }
        catch
        {
            TempData[ErrorMessage] = "Error: Can't load polls.";
            return RedirectToAction("All", "Poll");
        }

    }

    [HttpPost]
    public async Task<IActionResult> Create(PollFormModel model)
    {
        try
        {
            await this.pollService.CreatePollAsync(model);
            TempData[SuccessMessage] = "Success: Poll created.";

            return RedirectToAction("All", "Poll");
        }
        catch
        {
            TempData[ErrorMessage] = "Error: Can't create poll.";
            return RedirectToAction("All", "Poll");
        }
    }
}

