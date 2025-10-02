using ArqaCharity.Core.Entities;

namespace ArqaCharity.Core.Interfaces.IRepositories
{
    public interface IDonationAppointmentRepository : IGenericRepository<DonationAppointment>
    {
        Task<DonationAppointment?> GetByNationalIdAsync(string nationalId);
        Task<bool> ExistsByNationalIdAsync(string nationalId);
        Task<List<DonationAppointment>> GetPendingAppointmentsAsync();
        new Task UpdateAsync(DonationAppointment appointment);

    }
}
