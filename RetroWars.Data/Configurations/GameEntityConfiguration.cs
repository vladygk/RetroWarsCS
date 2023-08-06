using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetroWars.Data.Models;

namespace RetroWars.Data.Configurations;

public class GameEntityConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder
            .HasOne(g => g.Platform)
            .WithMany(p => p.Games)
            .HasForeignKey(g => g.PlatformId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(g => g.Genre)
            .WithMany(gr => gr.Games)
            .HasForeignKey(g => g.GenreId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(g => g.Users)
            .WithMany(u => u.FavoriteGames);
    }
}

