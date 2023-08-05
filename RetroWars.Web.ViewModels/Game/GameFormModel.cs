namespace RetroWars.Web.ViewModels.Game;

using System.ComponentModel.DataAnnotations;
using Genre;
using static Common.EntityValidationConstants.Game;
public class GameFormModel
{
    [Required]
    [StringLength(MaxNameLength, MinimumLength = MinNameLength)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(MaxDeveloperNameLength, MinimumLength = MinDeveloperNameLength)]
    public string Developer { get; set; } = null!;

    [Required]
    [StringLength(MaxPublisherNameLength, MinimumLength = MinPublisherNameLength)]
    public string Publisher { get; set; } = null!;

    [Required]
    [StringLength(MaxImageUrlLength, MinimumLength = MinImageUrlLength)]
    public string ImageUrl { get; set; } = null!;

    [Range(typeof(decimal), MinYear, MaxYear)]
    public int YearOfPublishing { get; set; }

    public int GenreId { get; set; }

    public IEnumerable<GameSelectGenreFormModel> Genres { get; set; }

    [Required]
    [StringLength(MaxDescriptionLength, MinimumLength = MinDescriptionLength)]
    public string Description { get; set; } = null!;


    public string Platforms { get; set; } = null!;
}

