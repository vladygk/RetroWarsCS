namespace RetroWars.Web.App.Controllers;

using Microsoft.AspNetCore.Authorization;
using Common.Enums;
using Infrastructure.Extensions;


using Microsoft.AspNetCore.Mvc;
using RetroWars.Services.Data.Contracts;
using ViewModels.Poll;

using static Common.NotificationMessagesConstants;
using static Common.GeneralApplicationConstants;
using Microsoft.Extensions.Caching.Memory;
using RetroWars.Web.ViewModels.Game;

public class PollController : AuthorizationController
{
    private readonly IPollService pollService;
    private readonly IGameService gameService;
    private readonly IMemoryCache cache;
    public PollController(IPollService pollService, IGameService gameService, IMemoryCache cache)
    {
        this.pollService = pollService;
        this.gameService = gameService;
        this.cache = cache;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        try
        {

            IEnumerable<PollViewModel> allPolls =
                this.cache.Get<IEnumerable<PollViewModel>>(PollsCacheKey);

            if (allPolls == null)
            {
                allPolls = await this.pollService.GetAllActivePollsAsync();

                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(PollsCacheDurationMinutes));

                this.cache.Set(PollsCacheKey, allPolls, cacheOptions);
            }


            return this.View(allPolls);
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
                this.cache.Remove(PollsCacheKey);

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
            TempData[ErrorMessage] = "Error: Can't  vote.";
            return RedirectToAction("All", "Poll");
        }
        return View(model);
    }

    [HttpGet]
    [Authorize(Roles = AdminRoleName)]
    public async Task<IActionResult> DeactivatePoll(string id)
    {
        try
        {
          await  this.pollService.DeactivateAPoll(id);

          return RedirectToAction("All", "Poll");
        }
        catch
        {
            TempData[ErrorMessage] = "Error: Can't  deactivate.";
            return RedirectToAction("All", "Poll");
        }
    }
}

