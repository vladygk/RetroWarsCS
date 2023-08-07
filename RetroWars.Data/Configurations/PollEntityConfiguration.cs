namespace RetroWars.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

public class PollEntityConfiguration : IEntityTypeConfiguration<Poll>
{
    public void Configure(EntityTypeBuilder<Poll> builder)
    {
        builder.HasOne(p => p.FirstGame)
            .WithMany(g => g.PollsAsFirstParticipant)
            .HasForeignKey(p => p.FirstGameId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.SecondGame)
            .WithMany(g => g.PollsAsSecondParticipant)
            .HasForeignKey(p => p.SecondGameId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .Property(p => p.IsActive)
            .HasDefaultValue(true);

        builder
            .HasMany(p => p.Voters)
            .WithMany(au => au.Polls);
    }
}

