using ArqaCharity.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArqaCharity.Infrastructure.Data.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Description).IsRequired().HasColumnType("nvarchar(max)");
            builder.Property(p => p.Location).IsRequired().HasMaxLength(300);
            builder.Property(p => p.PromoImageUrl).HasMaxLength(500);
            builder.Property(p => p.BankAccountNumber).IsRequired().HasMaxLength(34); // IBAN max length
            builder.Property(p => p.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}



