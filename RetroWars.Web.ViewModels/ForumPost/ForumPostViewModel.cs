namespace RetroWars.Web.ViewModels.ForumPost;

public class ForumPostViewModel
{
    public Guid Id { get; set; }

    public string Content { get; set; } = null!;

    public string PostTime { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public Guid UserId { get; set; }
}
