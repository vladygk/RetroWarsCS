﻿namespace RetroWars.Data.Models;

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
    public Guid FirstGameId { get; set; }

    [Required]
    public bool IsActive { get; set; }

    public virtual Game FirstGame { get; set; }

    [Required]
    public Guid SecondGameId { get; set; }

    public virtual Game SecondGame { get; set; }

    [Required] public int VotesForFirst { get; set; }

    [Required] public int VotesForSecond { get; set; }
}

