using BaseballStats.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasKey(x => new { x.Team1Id, x.Team2Id, x.Date });

        builder.HasOne(x => x.Team1)
            .WithMany()
            .HasForeignKey(x => x.Team1Id);

        builder.HasOne(x => x.Team2)
            .WithMany()
            .HasForeignKey(x => x.Team2Id);

        builder.HasOne(x => x.Series)
            .WithMany()
            .HasForeignKey(x => x.SeriesId);
    }
}