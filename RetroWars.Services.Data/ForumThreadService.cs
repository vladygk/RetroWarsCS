namespace RetroWars.Services.Data;

using RetroWars.Data.Models;
using RetroWars.Data.Repository;
using RetroWars.Services.Data.Contracts;
using RetroWars.Web.ViewModels.ForumThread;

public class ForumThreadService : IForumThreadService
{
    private readonly IRepository<ForumThread> forumThreadRepository;
    private readonly IRepository<ForumPost> forumPostRepository;
    public ForumThreadService(IRepository<ForumThread> forumThreadRepository, IRepository<ForumPost> forumPostRepository)
    {
        this.forumThreadRepository = forumThreadRepository;
        this.forumPostRepository = forumPostRepository;
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

        for (int i = 0; i < thread.ForumPosts.Count; i++)
        {
            await this.forumPostRepository.DeleteOneAsync(thread.ForumPosts.ToList()[i].Id);
        }
        await this.forumPostRepository.SaveAsync();

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
            UserId = t.UserId,
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
            UserId = thread.UserId,
            ForumPostsCount = thread.ForumPosts.Count
        };
    }
}
