using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetroWars.Data.Models;

namespace RetroWars.Data.Configurations;

public class GameEntityConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder
            .HasMany(g => g.Platforms)
            .WithMany(p => p.Games);

        builder
            .HasOne(g => g.Genre)
            .WithMany(gr => gr.Games)
            .HasForeignKey(g=>g.GenreId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

