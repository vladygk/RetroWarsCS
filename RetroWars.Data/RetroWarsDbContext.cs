using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RetroWars.Data.Configurations;
using RetroWars.Data.Models;

namespace RetroWars.Data;

    public class RetroWarsDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public RetroWarsDbContext(DbContextOptions<RetroWarsDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<Platform> Platforms { get; set; } = null!;
        public DbSet<Poll> Polls { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
            builder.ApplyConfiguration(new GameEntityConfiguration());
            builder.ApplyConfiguration(new PollEntityConfiguration());
            

            base.OnModelCreating(builder);
        }


    }
