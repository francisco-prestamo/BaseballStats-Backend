using BaseballStats.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class RegisteredUserConfiguration : IEntityTypeConfiguration<RegisteredUser>
{
    public void Configure(EntityTypeBuilder<RegisteredUser> builder)
    {
        builder.UseTptMappingStrategy();
        
        builder.Property(x => x.Username).HasMaxLength(255);

        builder.Property(x => x.Password).HasMaxLength(255);
        
        builder.Property(x => x.Type);
    }
}