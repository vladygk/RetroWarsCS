namespace RetroWars.Data.Models;

using RetroWars.Data.Repository;
using System.ComponentModel.DataAnnotations;
using static Common.EntityValidationConstants.ForumPost;
public class ForumPost : IBaseEntity
{

    public ForumPost() 
    {
        Id = Guid.NewGuid();
        PostTime = DateTime.Now;
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(MaxContentLength)]
    public string Content { get; set; } = null!;

    [Required]
    public DateTime PostTime { get; set; } 

    [Required]
    public Guid UserId { get; set; }
    public virtual ApplicationUser User { get; set; }

    [Required]
    public Guid ForumThreadId { get; set; }
    public virtual ForumThread ForumThread { get; set; }
}
