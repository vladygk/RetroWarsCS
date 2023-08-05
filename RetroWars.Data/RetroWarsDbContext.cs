using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RetroWars.Data.Models;

namespace RetroWars.Data
{
    public class RetroWarsDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {

    }
}