using Retrowars.Data.Repository;

namespace RetroWars.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RetroWars.Common.EntityValidationConstants.Game;


public class Game : IBaseEntity
{

    public Game()
    {
        
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
    public Guid GenreId { get; set; }
    public virtual Genre Genre { get; set; }

    [Required]
    [MaxLength(MaxDescriptionLength)]
    public string Description { get; set; } = null!;

    [Required]
    public Guid PlatformId { get; set; }

    public virtual Platform Platform { get; set; }

    public virtual ICollection<Poll> PollsAsFirstParticipant { get; set; }

    public virtual ICollection<Poll> PollsAsSecondParticipant { get; set; }
}
