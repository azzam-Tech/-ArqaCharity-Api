using ArqaCharity.Core.Entities;

namespace ArqaCharity.Core.Interfaces.IRepositories
{
    public interface IVolunteerRepository : IGenericRepository<Volunteer>
    {
        Task<Volunteer?> GetByPhoneNumberAsync(string phoneNumber);
        Task<bool> ExistsByPhoneNumberAsync(string phoneNumber);
    }
}
