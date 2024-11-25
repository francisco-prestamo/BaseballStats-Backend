using BaseballStats.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class PitcherConfiguration : IEntityTypeConfiguration<Pitcher>
{
    public void Configure(EntityTypeBuilder<Pitcher> builder)
    {
    }
}