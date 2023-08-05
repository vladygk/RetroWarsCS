namespace RetroWars.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RetroWars.Common.EntityValidationConstants.Game;


public class Game
{

    public Game()
    {
        this.Platforms = new HashSet<Platform>();
        this.Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(MaxNameLength)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(MaxDeveloperNameLength)]
    public string Developer { get; set; } = null!;

    [Required]
    [MaxLength(MaxPublisherNameLength)]
    public string Publisher { get; set; } = null!;

    [Required]
    [MaxLength(MaxImageUrlLength)]
    public string ImageUrl { get; set; } = null!;

    [Required]
    public int YearOfPublishing { get; set; }

    [Required]
    [ForeignKey(nameof(Genre))]
    public Guid GenreId { get; set; }
    public virtual Genre Genre { get; set; }

    [Required]
    [MaxLength(MaxDescriptionLength)]
    public string Description { get; set; } = null!;


    public virtual ICollection<Platform> Platforms { get; set; }


}
