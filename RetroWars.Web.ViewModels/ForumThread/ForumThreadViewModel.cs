using RetroWars.Web.ViewModels.ForumPost;
using X.PagedList;

namespace RetroWars.Web.ViewModels.ForumThread;

public class ForumThreadViewModel
{
    public ForumThreadViewModel()
    {
        this.ForumPosts = new HashSet<ForumPostViewModel>();    
    }
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string CreatedDateTime { get; set; } = null!;

    public string UserName { get; set; } = null!;
    public Guid UserId { get; set; }    

    public int ForumPostsCount { get; set; }

    public IEnumerable<ForumPostViewModel> ForumPosts { get; set; }
    public X.PagedList.IPagedList<ForumPostViewModel>? ForumPostsPaged { get; set; }
}
