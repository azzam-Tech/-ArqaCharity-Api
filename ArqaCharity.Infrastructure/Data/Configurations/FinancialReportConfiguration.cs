using ArqaCharity.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArqaCharity.Infrastructure.Data.Configurations
{
    public class FinancialReportConfiguration : IEntityTypeConfiguration<FinancialReport>
    {
        public void Configure(EntityTypeBuilder<FinancialReport> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.ReportName).IsRequired().HasMaxLength(200);
            builder.Property(f => f.PdfFilePath).IsRequired().HasMaxLength(500);
            builder.Property(f => f.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
