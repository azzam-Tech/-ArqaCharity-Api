using ArqaCharity.Core.Entities;

namespace ArqaCharity.Core.Interfaces.IRepositories
{
    public interface INeedyRepository : IGenericRepository<Needy>
    {
        Task<Needy?> GetByPhoneNumberAsync(string phoneNumber);
        Task<bool> ExistsByPhoneNumberAsync(string phoneNumber);
    }
}
