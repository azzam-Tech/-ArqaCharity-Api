using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Models;
using ArqaCharity.Core.Models.Enums;

namespace ArqaCharity.Core.Interfaces.IServices
{
    public interface IVolunteerService
    {
        Task<Result<Volunteer>> AddVolunteerAsync(
            string name,
            string phoneNumber,
            string nationality,
            DateTime dateOfBirth,
            string profession,
            WorkShift workShift);

        Task<Result<IReadOnlyList<Volunteer>>> GetAllVolunteersAsync();
        Task<Result<Volunteer>> GetVolunteerByIdAsync(int id);
    }


}
