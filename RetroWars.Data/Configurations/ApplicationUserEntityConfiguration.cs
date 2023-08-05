using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetroWars.Data.Models;

namespace RetroWars.Data.Configurations;

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser> 
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .Property(u => u.FirstName)
            .HasDefaultValue("Test");

        builder
            .Property(u => u.LastName)
            .HasDefaultValue("Test");
    }
}

