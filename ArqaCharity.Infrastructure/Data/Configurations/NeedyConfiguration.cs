using ArqaCharity.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArqaCharity.Infrastructure.Data.Configurations
{
    public class NeedyConfiguration : IEntityTypeConfiguration<Needy>
    {
        public void Configure(EntityTypeBuilder<Needy> builder)
        {
            builder.HasKey(n => n.Id);
            builder.Property(n => n.Name).IsRequired().HasMaxLength(150);
            builder.Property(n => n.PhoneNumber).IsRequired().HasMaxLength(15);
            builder.Property(n => n.RequiredAmount).HasColumnType("decimal(18,2)");
            builder.Property(n => n.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }

}

