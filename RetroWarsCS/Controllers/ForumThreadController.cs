namespace RetroWars.Web.App.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using X.PagedList;
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
    public async Task<IActionResult> All(int? page)
    {
        try
        {
            var threads = await this.forumThreadService.GetAllAsync();

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return this.View(threads.ToPagedList(pageNumber, pageSize));
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
    public async Task<IActionResult> Details(string id, int? page)
    {
        try
        {
            ForumThreadViewModel viewModel =  await this.forumThreadService.GetOneAsync(id);

            viewModel.ForumPosts = await this.forumPostService.GetAllPostsAsync(id);
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            viewModel.ForumPostsPaged = viewModel.ForumPosts.ToPagedList(pageNumber,pageSize);

            viewModel.ForumPosts = viewModel.ForumPosts.ToPagedList(pageNumber, pageSize);

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

    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {

        try
        {
            bool success = await this.forumThreadService.DeleteOneAsync(id);
            if(success)
            {
                TempData[SuccessMessage] = "Success: Thread deleted!";
                return RedirectToAction("All", "ForumThread");
            }

            TempData[ErrorMessage] = "Error: Can't delete thread, plase try again.";
            return RedirectToAction("All", "ForumThread");
        }
        catch
        {
            TempData[ErrorMessage] = "Error: Can't delete thread, plase try again.";
            return RedirectToAction("All", "ForumThread");
        }
    }

}
