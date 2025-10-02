using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Interfaces.IRepositories;
using ArqaCharity.Core.Interfaces.IServices;
using ArqaCharity.Core.Models;
using ArqaCharity.Core.Models.Enums;

namespace ArqaCharity.Infrastructure.Services
{
    public class VolunteerService : IVolunteerService
    {
        private readonly IVolunteerRepository _repo;

        public VolunteerService(IVolunteerRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<Volunteer>> AddVolunteerAsync(
            string name, string phoneNumber, string nationality, DateTime dateOfBirth, string profession, WorkShift workShift)
        {
            try
            {
                var volunteer = new Volunteer(name, phoneNumber, nationality, dateOfBirth, profession, workShift);

                if (await _repo.ExistsByPhoneNumberAsync(phoneNumber))
                    return Result<Volunteer>.Failure(new Error("VolunteerExists", "المتطوع مسجل مسبقًا", 409));

                await _repo.AddAsync(volunteer);
                return Result<Volunteer>.Success(volunteer);
            }
            catch (ArgumentException ex)
            {
                return Result<Volunteer>.Failure(new Error("ValidationError", ex.Message, 400));
            }
        }

        public async Task<Result<IReadOnlyList<Volunteer>>> GetAllVolunteersAsync()
        {
            var list = await _repo.ListAllAsync();
            return Result<IReadOnlyList<Volunteer>>.Success(list);
        }

        public async Task<Result<Volunteer>> GetVolunteerByIdAsync(int id)
        {
            var volunteer = await _repo.GetByIdAsync(id);
            if (volunteer == null)
                return Result<Volunteer>.Failure(new Error("VolunteerNotFound", "المتطوع غير موجود", 404));

            return Result<Volunteer>.Success(volunteer);
        }
    }
}
