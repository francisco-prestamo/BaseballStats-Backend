using BaseballStats.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class DirectionStaffConfiguration : IEntityTypeConfiguration<DirectionStaff>
{
    public void Configure(EntityTypeBuilder<DirectionStaff> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(255);

        builder.HasMany(x => x.TeamsLead)
            .WithMany(x => x.DirectionStaffs);
    }
}