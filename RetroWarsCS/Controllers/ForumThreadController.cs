namespace RetroWars.Web.App.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetroWars.Services.Data.Contracts;
using RetroWars.Web.Infrastructure.Extensions;
using RetroWars.Web.ViewModels.ForumThread;
using static Common.NotificationMessagesConstants;

public class ForumThreadController : AuthorizationController
{
    private readonly IForumThreadService forumThreadService;
    private readonly IForumPostService forumPostService;
    public ForumThreadController(IForumThreadService forumThreadService, IForumPostService forumPostService)
    {
        this.forumThreadService = forumThreadService;
        this.forumPostService = forumPostService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> All()
    {
        try
        {
            var threads = await this.forumThreadService.GetAllAsync();
            return this.View(threads);
        }
        catch
        {
            TempData[ErrorMessage] = "Error: Can't load threads, plase try again.";
            return RedirectToAction("Index", "Home");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return this.View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(ForumThreadFormModel model)
    {
        try
        {
            string userId = User.GetId();
            await this.forumThreadService.CreateAsync(model, userId);
            return RedirectToAction("All", "ForumThread");
        }
        catch 
        {

            TempData[ErrorMessage] = "Error: Can't add thread, plase try again.";
            return RedirectToAction("All", "ForumThread");
        }
  
    }

    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        try
        {
            ForumThreadViewModel viewModel =  await this.forumThreadService.GetOneAsync(id);

            viewModel.ForumPosts = await this.forumPostService.GetAllPostsAsync(id);

            DetailsPageWrapperViewModel wrapper = new DetailsPageWrapperViewModel()
            {
                ForumThreadViewModel = viewModel
            };

            return this.View(wrapper);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Error: Can't oad thread, plase try again.";
            return RedirectToAction("All", "ForumThread");

        }
    }

}
