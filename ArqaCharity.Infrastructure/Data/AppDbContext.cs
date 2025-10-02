using ArqaCharity.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArqaCharity.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<Needy> Needy { get; set; } = null!;
        public DbSet<DonationAppointment> DonationAppointments { get; set; } = null!;
        public DbSet<Volunteer> Volunteers { get; set; } = null!;
        public DbSet<MembershipType> MembershipTypes { get; set; } = null!;
        public DbSet<Membership> Memberships { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<FinancialReport> FinancialReports { get; set; } = null!;
        public DbSet<News> News { get; set; } = null!;
        public DbSet<Donation> Donations { get; set; } = null!;
        public DbSet<Admin> Admins { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // تطبيق التكوينات من مجلد Configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}