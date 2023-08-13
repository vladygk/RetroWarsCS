namespace RetroWars.Web.App.Areas.Admin.Controllers;

using ViewModels;
using RetroWars.Services.Data.Contracts;
using static Common.NotificationMessagesConstants;
using static Common.GeneralApplicationConstants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

public class PollController : AdminBaseController
{
    private readonly IPollService pollService;
    private readonly IMemoryCache cache;
    public PollController(IPollService pollService, IMemoryCache cache)
    {
        this.pollService = pollService;
        this.cache = cache;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {

        try
        {
            IEnumerable<PollAdminViewModel> allPolls =  await this.pollService.GetAllPollAdminViewModels();

        return this.View(allPolls);
        }
        catch
        {
            TempData[ErrorMessage] = "Error: Can't load polls.";
            return this.View(new List<PollAdminViewModel>());
        }
    }

    [HttpGet]
    public async Task<IActionResult> Activate(string id)
    {
        try
        {
             await this.pollService.ActivateAPoll(id);
            this.cache.Remove(PollsCacheKey);
            return RedirectToAction("All", "Poll", new { area = AdminAreaName });
        }
        catch
        {
            TempData[ErrorMessage] = "Error: Can't activate.";
            return RedirectToAction("All", "Poll", new { area = AdminAreaName });
        }
    }


}

