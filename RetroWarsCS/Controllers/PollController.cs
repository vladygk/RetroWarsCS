using RetroWars.Common.Enums;
using RetroWars.Web.Infrastructure.Extensions;

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
            if (ModelState.IsValid)
            {
                await this.pollService.CreatePollAsync(model);
                TempData[SuccessMessage] = "Success: Poll created.";
                return RedirectToAction("All", "Poll");
            }

            TempData[ErrorMessage] = "Error: Can't create a Poll for the same game";
            model.Games = await this.gameService.GetAllPollSelectGameViewModels();
            return this.View(model);


        }
        catch
        {
            TempData[ErrorMessage] = "Error: Can't create poll.";
            return RedirectToAction("All", "Poll");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Vote(string id)
    {
        try
        {
            PollViewModel model = await this.pollService.GetOnePollAsync(id);

            return this.View(model);

        }
        catch
        {
            TempData[ErrorMessage] = "Error: Cant load voting screen";
            return RedirectToAction("All", "Poll");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Vote(PollViewModel model)
    {
        try
        {
            PollViewModel poll = await this.pollService.GetOnePollAsync(model.Id.ToString());

            VoteOptions choice = model.Vote == poll.FirstGameId
                ? VoteOptions.VoteForFirst
                : VoteOptions.VoteForSecond;


            string userId = User.GetId()!;
            await this.pollService.MarkUserAsVoted(poll.Id.ToString(), userId);

            PollViewModel votedPoll = await this.pollService.IncreaseVotes(model.Id.ToString(), choice);
            return RedirectToAction("Vote", "Poll", votedPoll);
        }
        catch
        {
            TempData[ErrorMessage] = "Error: Cant  vote";
            return RedirectToAction("All", "Poll");
        }
        return View(model);
    }
}

