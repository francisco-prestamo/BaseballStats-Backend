using BaseballStats.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class DirectionStaffTeamConfiguration : IEntityTypeConfiguration<DirectionStaffTeam>
{
    public void Configure(EntityTypeBuilder<DirectionStaffTeam> builder)
    {
        builder.HasKey(x => new { x.DirectionStaffId, x.TeamId });

        builder
            .HasOne(x => x.DirectionStaff)
            .WithMany(x => x.DirectionStaffTeams)
            .HasForeignKey(x => x.DirectionStaffId);

        builder
            .HasOne(x => x.Team)
            .WithMany(x => x.DirectionStaffTeams)
            .HasForeignKey(x => x.TeamId);
    }
}