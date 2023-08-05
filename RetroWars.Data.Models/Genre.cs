namespace RetroWars.Data.Models;

using Retrowars.Data.Repository;
using System.ComponentModel.DataAnnotations;
using static RetroWars.Common.EntityValidationConstants.Genre;

public class Genre : IBaseEntity
{

    public Genre()
    {
        this.Id = Guid.NewGuid();
        this.Games = new List<Game>();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(MaxNameLength)]
    public string Name { get; set; } = null!;

    public virtual ICollection<Game> Games { get; set; }
}

