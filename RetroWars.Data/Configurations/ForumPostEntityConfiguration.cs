namespace RetroWars.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetroWars.Data.Models;


public class ForumPostEntityConfiguration : IEntityTypeConfiguration<ForumPost>
{
    public void Configure(EntityTypeBuilder<ForumPost> builder)
    {
        builder.HasOne(fp => fp.User)
            .WithMany(u => u.ForumPosts)
            .HasForeignKey(fp => fp.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(fp => fp.ForumThread)
            .WithMany(ft => ft.ForumPosts)
            .HasForeignKey(fp => fp.ForumThreadId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
