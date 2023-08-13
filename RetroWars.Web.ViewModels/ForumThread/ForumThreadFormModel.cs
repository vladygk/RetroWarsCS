namespace RetroWars.Web.ViewModels.ForumThread;

using System.ComponentModel.DataAnnotations;
using static Common.EntityValidationConstants.ForumThread;

public class ForumThreadFormModel
{

    [Required]
    [StringLength(MaxTitleLength, MinimumLength = MinTitleLenght)]
    public string Title { get; set; } = null!;


}
