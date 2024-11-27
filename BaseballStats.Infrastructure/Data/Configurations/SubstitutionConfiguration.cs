using BaseballStats.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class SubstitutionConfiguration : IEntityTypeConfiguration<Substitution>
{
    public void Configure(EntityTypeBuilder<Substitution> builder)
    {
        builder.HasKey(x => new { x.PlayerInId, x.PlayerOutId, x.GameId, x.Time });

        builder.HasOne(x => x.PlayerIn)
            .WithMany()
            .HasForeignKey(x => x.PlayerInId);

        builder.HasOne(x => x.PlayerOut)
            .WithMany()
            .HasForeignKey(x => x.PlayerOutId);

        builder.HasOne(x => x.Game)
            .WithMany()
            .HasForeignKey(x => x.GameId);
    
        builder.HasOne(x => x.Team)
            .WithMany()
            .HasForeignKey(x => x.TeamId);
    }
}