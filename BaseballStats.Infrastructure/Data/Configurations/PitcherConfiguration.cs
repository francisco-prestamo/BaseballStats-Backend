using BaseballStats.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class PitcherConfiguration : IEntityTypeConfiguration<Pitcher>
{
    public void Configure(EntityTypeBuilder<Pitcher> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .IsRequired();

        builder.Property(p => p.GamesWonNumber)
            .IsRequired();

        builder.Property(p => p.GamesLostNumber)
            .IsRequired();

        builder.Property(p => p.RightHanded)
            .IsRequired();

        builder.Property(p => p.AllowedRunsAvg)
            .IsRequired();

        builder.HasOne(p => p.Player)
            .WithOne()
            .HasForeignKey<Pitcher>(p => p.Id)
            .IsRequired();
    }
}