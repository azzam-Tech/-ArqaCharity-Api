using ArqaCharity.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArqaCharity.Infrastructure.Data.Configurations
{

    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Username).IsRequired().HasMaxLength(50);
            builder.Property(a => a.Email).IsRequired().HasMaxLength(256);
            builder.Property(a => a.PasswordHash).IsRequired();
            builder.Property(a => a.Role).IsRequired();
            builder.Property(a => a.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

            builder.HasIndex(a => a.Username).IsUnique();
            builder.HasIndex(a => a.Email).IsUnique();
        }
    }

}

