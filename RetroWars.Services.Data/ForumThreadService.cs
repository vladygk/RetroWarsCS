namespace RetroWars.Services.Data;

using RetroWars.Data.Models;
using RetroWars.Data.Repository;
using RetroWars.Services.Data.Contracts;
using RetroWars.Web.ViewModels.ForumThread;

public class ForumThreadService : IForumThreadService
{
    private readonly IRepository<ForumThread> forumThreadRepository;

    public ForumThreadService(IRepository<ForumThread> forumThreadRepository)
    {
        this.forumThreadRepository = forumThreadRepository;
    }
    public async Task<bool> CreateAsync(ForumThreadFormModel model, string userId)
    {
        try
        {
            ForumThread thread = new ForumThread()
            {
                Title = model.Title,
                UserId = Guid.Parse(userId),
            };


            await this.forumThreadRepository.AddAsync(thread);
            await this.forumThreadRepository.SaveAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteOneAsync(string id)
    {
        ForumThread? thread = await this.forumThreadRepository.GetOneAsync(id, false);

        if (thread is null)
        {
            return false;
        }
        await this.forumThreadRepository.DeleteOneAsync(Guid.Parse(id));
        await this.forumThreadRepository.SaveAsync(); 
        return true;

    }

    public async Task<IEnumerable<ForumThreadViewModel>> GetAllAsync()
    {
       IEnumerable<ForumThread> allThreads = await this.forumThreadRepository.GetAllAsync();

        return allThreads.Select(t => new ForumThreadViewModel()
        {
            Id = t.Id,
            Title = t.Title,
            CreatedDateTime = t.CreatedDateTime.ToString("MM/dd/yyyy h:mm tt"),
            UserName =$"{t.User.FirstName} {t.User.LastName}",
            ForumPostsCount = t.ForumPosts.Count

        });
    }

    public async Task<ForumThreadViewModel> GetOneAsync(string id)
    {
        ForumThread? thread = await this.forumThreadRepository.GetOneAsync(id,false);

        if (thread is null)
        {
            throw new ArgumentException("Invalid id.");
        }

        return new ForumThreadViewModel()
        {
            Id = thread.Id,
            Title = thread.Title,
            CreatedDateTime = thread.CreatedDateTime.ToString("MM/dd/yyyy h:mm tt"),
            UserName = $"{thread.User.FirstName} {thread.User.LastName}",
            ForumPostsCount = thread.ForumPosts.Count
        };
    }
}
