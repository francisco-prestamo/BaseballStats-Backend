using BaseballStats.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class AlignedPlayerInGameConfiguration : IEntityTypeConfiguration<AlignedPlayerInGame>
{
    public void Configure(EntityTypeBuilder<AlignedPlayerInGame> builder)
    {
        builder.HasIndex(x => new { x.PlayerId, x.GameId }).IsUnique();
        builder.HasIndex(x => new { x.Position, x.TeamId, x.GameId}).IsUnique();

        builder.HasKey(x => new { x.PlayerId, x.GameId, x.Position });

        builder.HasOne(x => x.Player)
            .WithMany()
            .HasForeignKey(x => x.PlayerId);

        builder.HasOne(x => x.Game)
            .WithMany()
            .HasForeignKey(x => x.GameId);
        
        builder.HasOne(x => x.Team)
            .WithMany()
            .HasForeignKey(x => x.TeamId);
    }
}