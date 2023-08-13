namespace RetroWars.Data.Models;

using RetroWars.Data.Repository;
using System.ComponentModel.DataAnnotations;
using static Common.EntityValidationConstants.ForumThread;
public class ForumThread : IBaseEntity
{
    public ForumThread()
    {
        Id = Guid.NewGuid();
        CreatedDateTime = DateTime.Now;
        ForumPosts = new HashSet<ForumPost>();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(MaxTitleLength)]
    public string Title { get; set; } = null!;
    [Required]
    public DateTime CreatedDateTime { get; set; }
    [Required]
    public Guid UserId { get; set; }

    public virtual ApplicationUser User { get; set; }

    public virtual ICollection<ForumPost> ForumPosts { get; set; }
}
