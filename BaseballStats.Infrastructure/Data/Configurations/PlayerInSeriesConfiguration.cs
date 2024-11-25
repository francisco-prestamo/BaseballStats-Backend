using BaseballStats.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class PlayerInSeriesConfiguration : IEntityTypeConfiguration<PlayerInSeries>
{
    public void Configure(EntityTypeBuilder<PlayerInSeries> builder)
    {
        builder.HasKey(x => new {x.PlayerId, x.SeriesId});
        
        builder.HasOne(x => x.Player)
            .WithMany()
            .HasForeignKey(x => x.PlayerId);

        builder.HasOne(x => x.Series)
            .WithMany()
            .HasForeignKey(x => x.SeriesId);

        builder.HasOne(x => x.Team)
            .WithOne()
            .HasForeignKey<PlayerInSeries>(x => x.TeamId);
    }
}