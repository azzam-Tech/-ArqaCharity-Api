using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Models;

namespace ArqaCharity.Core.Interfaces.IServices
{
    public interface IDonationAppointmentService
    {
        Task<Result<DonationAppointment>> CreateAppointmentAsync(
            string name,
            string phoneNumber,
            string nationalId,
            DateTime appointmentDate);

        Task<Result> ApproveAppointmentAsync(int id);
        Task<Result> RejectAppointmentAsync(int id);
        Task<Result<IReadOnlyList<DonationAppointment>>> GetPendingAppointmentsAsync();
        Task<Result<DonationAppointment>> GetAppointmentByIdAsync(int id);
        Task<Result> UpdateAppointmentAsync(int id, Action<DonationAppointment> updateAction);

    }


}
