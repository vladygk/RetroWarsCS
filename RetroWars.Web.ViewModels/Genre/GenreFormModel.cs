namespace RetroWars.Web.ViewModels.Genre;

using static Common.EntityValidationConstants.Genre;
using System.ComponentModel.DataAnnotations;

public class GenreFormModel
{
    [Required]
    [StringLength(MaxNameLength, MinimumLength =MinNameLength)]
    public string Name { get; set; } = null!;
}
