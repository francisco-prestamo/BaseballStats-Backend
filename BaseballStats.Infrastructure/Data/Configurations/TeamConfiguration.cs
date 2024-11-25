using BaseballStats.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(255);
        builder.Property(x => x.Initials).HasMaxLength(3);
        builder.Property(x => x.RepresentedEntity).HasMaxLength(255);
        builder.Property(x => x.Color).HasMaxLength(50);

        builder.HasOne(x => x.TechnicalDirector)
            .WithMany()
            .HasForeignKey(x => x.TechnicalDirectorId);

        builder.HasMany(x => x.DirectionStaffs)
            .WithMany(x => x.TeamsLead);
    }
}