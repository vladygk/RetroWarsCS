using Microsoft.AspNetCore.Http;
using RetroWars.Web.ViewModels.Platform;

namespace RetroWars.Web.ViewModels.Game;

using System.ComponentModel.DataAnnotations;
using Genre;
using static Common.EntityValidationConstants.Game;
public class GameFormModel
{
    public GameFormModel()
    {
        this.Platforms = new HashSet<GameSelectPlatformsFormModel>();
        this.Genres = new HashSet<GameSelectGenreFormModel>();
    }
    [Required]
    [StringLength(MaxNameLength, MinimumLength = MinNameLength)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(MaxDeveloperNameLength, MinimumLength = MinDeveloperNameLength)]
    public string Developer { get; set; } = null!;

    [Required]
    [StringLength(MaxPublisherNameLength, MinimumLength = MinPublisherNameLength)]
    public string Publisher { get; set; } = null!;

    
    public IFormFile? File { get; set; }

    [Range(typeof(int), MinYear, MaxYear)]
    public int YearOfPublishing { get; set; }

    public Guid GenreId { get; set; }

    public IEnumerable<GameSelectGenreFormModel> Genres { get; set; }

    [Required]
    [StringLength(MaxDescriptionLength, MinimumLength = MinDescriptionLength)]
    public string Description { get; set; } = null!;

    public Guid PlatformId { get; set; }
    public IEnumerable<GameSelectPlatformsFormModel> Platforms { get; set; } = null!;
  
}

