using Retrowars.Data.Repository;

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
    }

    [Required]
    [MaxLength(MaxFirstNameLength)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(MaxLastNameLength)]
    public string LastName { get; set; } = null!;

    public virtual ICollection<Game> FavoriteGames { get; set; }
}

