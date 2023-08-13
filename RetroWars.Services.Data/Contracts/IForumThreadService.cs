
using RetroWars.Web.ViewModels.ForumThread;

namespace RetroWars.Services.Data.Contracts;

public interface IForumThreadService
{
    public Task<IEnumerable<ForumThreadViewModel>> GetAllAsync();

    public Task<ForumThreadViewModel> GetOneAsync(string id);

    public Task<bool> CreateAsync(ForumThreadFormModel model, string userId);

    public Task<bool> DeleteOneAsync(string id);

}
