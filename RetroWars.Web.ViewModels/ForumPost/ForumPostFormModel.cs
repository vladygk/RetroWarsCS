namespace RetroWars.Web.ViewModels.ForumPost;

using System.ComponentModel.DataAnnotations;
using static Common.EntityValidationConstants.ForumPost;
public class ForumPostFormModel
{
    [Required]
    [StringLength(MaxContentLength, MinimumLength =MinContentLenght)]
    public string Content { get; set; } = null!;

    public string ForumThreadId { get; set; } = null!;
}
