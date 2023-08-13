namespace RetroWars.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetroWars.Data.Models;


public class ForumThreadEntityConfiguration : IEntityTypeConfiguration<ForumThread>
{
    public void Configure(EntityTypeBuilder<ForumThread> builder)
    {
        builder.HasOne(ft => ft.User)
                 .WithMany(u => u.ForumThreads)
                 .HasForeignKey(ft => ft.UserId)
                 .OnDelete(DeleteBehavior.Restrict);
    }
}
