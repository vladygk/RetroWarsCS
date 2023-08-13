namespace RetroWars.Data.Models;

using RetroWars.Data.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Poll : IBaseEntity
{
    public Poll()
    {
        this.Id = Guid.NewGuid();
        this.Voters = new HashSet<ApplicationUser>();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid FirstGameId { get; set; }

    [Required]
    public bool IsActive { get; set; }

    public virtual Game FirstGame { get; set; }

    [Required]
    public Guid SecondGameId { get; set; }

    public virtual Game SecondGame { get; set; }

    [Required] public int VotesForFirst { get; set; }

    [Required] public int VotesForSecond { get; set; }

    public virtual ICollection<ApplicationUser> Voters { get; set; }
}

