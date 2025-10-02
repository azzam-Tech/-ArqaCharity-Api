using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Interfaces.IRepositories;
using ArqaCharity.Core.Models.Enums;
using ArqaCharity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ArqaCharity.Infrastructure.Repositories
{
    public class DonationAppointmentRepository : GenericRepository<DonationAppointment>, IDonationAppointmentRepository
    {
        public DonationAppointmentRepository(AppDbContext context) : base(context) { }

        public async Task<DonationAppointment?> GetByNationalIdAsync(string nationalId)
        {
            return await _context.DonationAppointments.FirstOrDefaultAsync(d => d.NationalId == nationalId);
        }

        public async Task<bool> ExistsByNationalIdAsync(string nationalId)
        {
            return await _context.DonationAppointments.AnyAsync(d => d.NationalId == nationalId);
        }

        public async Task<List<DonationAppointment>> GetPendingAppointmentsAsync()
        {
            return await _context.DonationAppointments
                .Where(d => d.Status == AppointmentStatus.Pending)
                .ToListAsync();
        }

        public async Task UpdateAsync(DonationAppointment appointment)
        {
            _context.Entry(appointment).State = EntityState.Modified;
        }
    }
}

