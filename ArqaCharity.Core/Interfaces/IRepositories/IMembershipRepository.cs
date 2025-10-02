using ArqaCharity.Core.Entities;

namespace ArqaCharity.Core.Interfaces.IRepositories
{
    public interface IMembershipRepository : IGenericRepository<Membership>
    {
        Task<Membership?> GetByNationalIdAsync(string nationalId);
        Task<Membership?> GetByEmailAsync(string email);
        Task<bool> ExistsByNationalIdAsync(string nationalId);
        Task<bool> ExistsByEmailAsync(string email);

    }
}
