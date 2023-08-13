namespace RetroWars.Data.Models;

using RetroWars.Data.Repository;
using System.ComponentModel.DataAnnotations;
using static RetroWars.Common.EntityValidationConstants.Platform;

public class Platform : IBaseEntity
{
    public Platform()
    {
        this.Games = new HashSet<Game>();
        this.Id = Guid.NewGuid();
    }
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(MaxNameLength)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(MaxImageUrlLength)]
    public string ImageUrl { get; set; } = null!;

    [Required]
    [MaxLength(MaxCompanyNameLength)]
    public string Company { get; set; } = null!;

    [Required]
    [MaxLength(MaxDescriptionLength)]
    public string Description { get; set; } = null!;

    [Required]
    public int YearOfRelease { get; set; }

    public virtual ICollection<Game> Games { get; set; }
}

