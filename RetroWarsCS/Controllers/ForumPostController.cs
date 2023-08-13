namespace RetroWars.Web.App.Controllers;

using Microsoft.AspNetCore.Mvc;
using RetroWars.Services.Data.Contracts;
using RetroWars.Web.Infrastructure.Extensions;
using RetroWars.Web.ViewModels.ForumPost;
using RetroWars.Web.ViewModels.Game;
using static Common.NotificationMessagesConstants;

public class ForumPostController : AuthorizationController
{
    private readonly IForumPostService forumPostService;

    public ForumPostController(IForumPostService forumPostService)
    {
        this.forumPostService = forumPostService;
    }


    [HttpPost]
    public async Task<IActionResult> Add(ForumPostFormModel postModel)
    {
        if (ModelState.IsValid)
        {
            string userId = User.GetId()!;
            bool success = await this.forumPostService.CreatePostAsync(postModel, postModel.ForumThreadId, userId);

            if(success) {
                TempData[SuccessMessage] = "Success: Post added.";
                return RedirectToAction("Details", "ForumThread", new {id = postModel.ForumThreadId });
            }
            TempData[ErrorMessage] = "Error: Can't add post.";
            return RedirectToAction("All", "ForumThread");
        }

        TempData[ErrorMessage] = "Error: Can't add post.";
        return RedirectToAction("All", "ForumThread");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id, string threadId)
    {
        try
        {
            
            bool success = await this.forumPostService.DeletePostAsync(id);

            if (success)
            {
                TempData[SuccessMessage] = "Success: Post deleted.";
                return RedirectToAction("Details", "ForumThread", new {id= threadId });
            }
            TempData[ErrorMessage] = "Error: Can't delete post, plase try again.";
            return RedirectToAction("Details", "ForumThread", new { id = threadId });
        }
        catch
        {
            TempData[ErrorMessage] = "Error: Can't delete post, plase try again.";
            return RedirectToAction("Details", "ForumThread", new { id = threadId });
        }
    }
}
