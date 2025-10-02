using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Interfaces.IRepositories;
using ArqaCharity.Core.Interfaces.IServices;
using ArqaCharity.Core.Models;

namespace ArqaCharity.Infrastructure.Services
{
    public class DonationAppointmentService : IDonationAppointmentService
    {
        private readonly IDonationAppointmentRepository _repo;

        public DonationAppointmentService(IDonationAppointmentRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<DonationAppointment>> CreateAppointmentAsync(
            string name, string phoneNumber, string nationalId, DateTime appointmentDate)
        {
            try
            {
                var appointment = new DonationAppointment(name, phoneNumber, nationalId, appointmentDate);

                if (await _repo.ExistsByNationalIdAsync(nationalId))
                    return Result<DonationAppointment>.Failure(new Error("AppointmentExists", "طلب التبرع مسجل مسبقًا", 409));

                await _repo.AddAsync(appointment);
                return Result<DonationAppointment>.Success(appointment);
            }
            catch (ArgumentException ex)
            {
                return Result<DonationAppointment>.Failure(new Error("ValidationError", ex.Message, 400));
            }
        }

        public async Task<Result> ApproveAppointmentAsync(int id)
        {
            var appointment = await _repo.GetByIdAsync(id);
            if (appointment == null)
                return Result.Failure(new Error("AppointmentNotFound", "الطلب غير موجود", 404));

            appointment.Approve();
            await _repo.UpdateAsync(appointment);
            return Result.Success();
        }

        public async Task<Result> RejectAppointmentAsync(int id)
        {
            var appointment = await _repo.GetByIdAsync(id);
            if (appointment == null)
                return Result.Failure(new Error("AppointmentNotFound", "الطلب غير موجود", 404));

            appointment.Reject();
            await _repo.UpdateAsync(appointment);
            return Result.Success();
        }

        public async Task<Result<IReadOnlyList<DonationAppointment>>> GetPendingAppointmentsAsync()
        {
            var list = await _repo.GetPendingAppointmentsAsync();
            return Result<IReadOnlyList<DonationAppointment>>.Success(list);
        }

        public async Task<Result<DonationAppointment>> GetAppointmentByIdAsync(int id)
        {
            var appointment = await _repo.GetByIdAsync(id);
            if (appointment == null)
                return Result<DonationAppointment>.Failure(new Error("AppointmentNotFound", "الطلب غير موجود", 404));

            return Result<DonationAppointment>.Success(appointment);
        }

        public async Task<Result> UpdateAppointmentAsync(int id, Action<DonationAppointment> updateAction)
        {
            var appointment = await _repo.GetByIdAsync(id);
            if (appointment == null)
                return Result.Failure(new Error("AppointmentNotFound", "الطلب غير موجود", 404));

            updateAction(appointment);
            await _repo.UpdateAsync(appointment);
            return Result.Success();
        }
    }
}
