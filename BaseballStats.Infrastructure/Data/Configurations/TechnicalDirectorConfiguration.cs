using BaseballStats.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class TechnicalDirectorConfiguration : IEntityTypeConfiguration<TechnicalDirector>
{
    public void Configure(EntityTypeBuilder<TechnicalDirector> builder)
    {
    }
}