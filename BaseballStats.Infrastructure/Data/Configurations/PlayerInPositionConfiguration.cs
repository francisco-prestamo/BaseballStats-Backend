using BaseballStats.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class PlayerInPositionConfiguration : IEntityTypeConfiguration<PlayerInPosition>
{
    public void Configure(EntityTypeBuilder<PlayerInPosition> builder)
    {
        builder.HasKey(p => new { p.PlayerId, p.Position });

        builder.Property(p => p.Position).HasConversion<string>();
        
        builder.HasOne(p => p.Player)
            .WithMany()
            .HasForeignKey(p => p.PlayerId);
    }
}