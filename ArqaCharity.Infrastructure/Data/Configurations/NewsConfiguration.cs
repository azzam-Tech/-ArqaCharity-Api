using ArqaCharity.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArqaCharity.Infrastructure.Data.Configurations
{
    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.HasKey(n => n.Id);
            builder.Property(n => n.Title).IsRequired().HasMaxLength(300);
            builder.Property(n => n.Content).IsRequired().HasColumnType("nvarchar(max)");
            builder.Property(n => n.PromoImageUrl).HasMaxLength(500);
            builder.Property(n => n.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}

