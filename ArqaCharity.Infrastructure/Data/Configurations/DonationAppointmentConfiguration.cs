using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArqaCharity.Infrastructure.Data.Configurations
{

    public class DonationAppointmentConfiguration : IEntityTypeConfiguration<DonationAppointment>
    {
        public void Configure(EntityTypeBuilder<DonationAppointment> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Name).IsRequired().HasMaxLength(150);
            builder.Property(d => d.PhoneNumber).IsRequired().HasMaxLength(15);
            builder.Property(d => d.NationalId).IsRequired().HasMaxLength(20);
            builder.Property(d => d.AppointmentDate).IsRequired();
            builder.Property(d => d.Status).HasDefaultValue(AppointmentStatus.Pending);
            builder.Property(d => d.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }

}