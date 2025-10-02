using ArqaCharity.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArqaCharity.Infrastructure.Data.Configurations
{

    public class DonationConfiguration : IEntityTypeConfiguration<Donation>
    {
        public void Configure(EntityTypeBuilder<Donation> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.DonorName).HasMaxLength(150);
            builder.Property(d => d.Amount).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(d => d.DonationDate).HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne<Project>()
                   .WithMany()
                   .HasForeignKey(d => d.ProjectId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

}

