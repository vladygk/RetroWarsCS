namespace RetroWars.Services.Data;

using RetroWars.Data.Models;
using RetroWars.Data.Repository;
using RetroWars.Services.Data.Contracts;
using RetroWars.Web.ViewModels.ForumPost;


public class ForumPostService : IForumPostService
{
    private readonly IRepository<ForumPost> forumPostRepository;

    public ForumPostService(IRepository<ForumPost> forumPostRepository)
    {
        this.forumPostRepository = forumPostRepository;
    }
    public async Task<bool> CreatePostAsync(ForumPostFormModel model, string threadId, string userId)
    {
        try
        {
            ForumPost forumPost = new ForumPost()
            {
                Content = model.Content,
                UserId = Guid.Parse(userId),
                ForumThreadId = Guid.Parse(threadId)
            };

            await this.forumPostRepository.AddAsync(forumPost);
            await this.forumPostRepository.SaveAsync();
            return true;
        }
        catch (Exception)
        {

            return false;
        }
    }

    public async Task<bool> DeletePostAsync(string id)
    {
        ForumPost? forumPost = await this.forumPostRepository.GetOneAsync(id,false);

        if(forumPost is null) {
            return false;
        }

        await this.forumPostRepository.DeleteOneAsync(Guid.Parse(id));
        await this.forumPostRepository.SaveAsync();
        return true;
    }

    public async Task<IEnumerable<ForumPostViewModel>> GetAllPostsAsync(string threadId)
    {
       IEnumerable<ForumPost> allPosts = await this.forumPostRepository.GetAllAsync();
        IEnumerable<ForumPost> allPostsForThread = allPosts.Where(p=>p.ForumThreadId==Guid.Parse(threadId));

        return allPostsForThread.Select(p => new ForumPostViewModel() { 
            Id = p.Id,
            Content = p.Content,
            PostTime = p.PostTime.ToString("MM/dd/yyyy h:mm tt"),
            UserName = $"{p.User.FirstName} {p.User.LastName}",
            UserId = p.UserId,
        });
    }
}

