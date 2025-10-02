using ArqaCharity.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static ArqaCharity.Core.Entities.Needy;

namespace ArqaCharity.Infrastructure.Data.Configurations
{
    public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
    {
        public void Configure(EntityTypeBuilder<Volunteer> builder)
        {
            builder.HasKey(v => v.Id);
            builder.Property(v => v.Name).IsRequired().HasMaxLength(150);
            builder.Property(v => v.PhoneNumber).IsRequired().HasMaxLength(15);
            builder.Property(v => v.Nationality).IsRequired().HasMaxLength(100);
            builder.Property(v => v.DateOfBirth).IsRequired();
            builder.Property(v => v.Profession).IsRequired().HasMaxLength(100);
            builder.Property(v => v.WorkShift).IsRequired();
            builder.Property(v => v.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}



