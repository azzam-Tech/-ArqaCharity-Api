using ArqaCharity.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArqaCharity.Infrastructure.Data.Configurations
{
    public class MembershipConfiguration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Name).IsRequired().HasMaxLength(150);
            builder.Property(m => m.PhoneNumber).IsRequired().HasMaxLength(15);
            builder.Property(m => m.Email).IsRequired().HasMaxLength(256);
            builder.Property(m => m.Nationality).IsRequired().HasMaxLength(100);
            builder.Property(m => m.DateOfBirth).IsRequired();
            builder.Property(m => m.Profession).IsRequired().HasMaxLength(100);
            builder.Property(m => m.AcademicQualification).IsRequired().HasMaxLength(200);
            builder.Property(m => m.MaritalStatus).IsRequired();
            builder.Property(m => m.NationalId).IsRequired().HasMaxLength(20);
            builder.Property(m => m.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

            builder.HasIndex(m => m.Email);
            builder.HasIndex(m => m.PhoneNumber);
        }
    }
}
