using RetroWars.Data.Repository;

namespace RetroWars.Data.Models;

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static RetroWars.Common.EntityValidationConstants.ApplicationUser;

public class ApplicationUser : IdentityUser<Guid>, IBaseEntity
{
    public ApplicationUser()
    {
        this.Id = Guid.NewGuid();

        this.FavoriteGames = new HashSet<Game>();
        this.Polls = new HashSet<Poll>();
        this.ForumPosts = new HashSet<ForumPost>();
        this.ForumThreads = new HashSet<ForumThread>();
    }

    [Required]
    [MaxLength(MaxFirstNameLength)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(MaxLastNameLength)]
    public string LastName { get; set; } = null!;

    public virtual ICollection<Game> FavoriteGames { get; set; }

    public virtual ICollection<Poll> Polls { get; set; }

    public virtual ICollection<ForumPost> ForumPosts { get; set; }

    public virtual ICollection<ForumThread> ForumThreads { get; set; }
}

