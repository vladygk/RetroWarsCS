namespace RetroWars.Services.Data.Contracts;

using RetroWars.Web.ViewModels.ForumPost;

public interface IForumPostService
{
    public Task<IEnumerable<ForumPostViewModel>> GetAllPostsAsync(string threadId);
    public Task<bool> CreatePostAsync(ForumPostFormModel model, string threadId, string userId);

    public Task<bool> DeletePostAsync(string id);
}
