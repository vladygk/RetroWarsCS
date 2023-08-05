namespace RetroWars.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Poll
{
    public Poll()
    {
        this.Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey(nameof(FirstGame))]
    public Guid FirstGameId { get; set; }

    public virtual Game FirstGame { get; set; }

    [Required]
    [ForeignKey(nameof(SecondGame))]
    public Guid SecondGameId { get; set; }

    public virtual Game SecondGame { get; set; }

    [Required] public int VotesForFirst { get; set; }

    [Required] public int VotesForSecond { get; set; }
}

