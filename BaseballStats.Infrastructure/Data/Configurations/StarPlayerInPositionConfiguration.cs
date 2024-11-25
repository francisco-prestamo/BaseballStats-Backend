using BaseballStats.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class StarPlayerInPositionConfiguration : IEntityTypeConfiguration<StarPlayerInPosition>
{
    public void Configure(EntityTypeBuilder<StarPlayerInPosition> builder)
    {
        builder.HasKey(x => new { x.PlayerId, x.Position, x.SeriesId });

        builder.Property(x => x.Position).HasConversion<string>();

        builder.HasOne(x => x.PlayerInPosition)
            .WithMany()
            .HasForeignKey(x => new { x.PlayerId, x.Position });
        
        builder.HasOne(x => x.Series)
            .WithMany()
            .HasForeignKey(x => x.SeriesId);
    }
}