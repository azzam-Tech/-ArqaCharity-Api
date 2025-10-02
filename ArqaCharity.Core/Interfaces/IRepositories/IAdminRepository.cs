using ArqaCharity.Core.Entities;

namespace ArqaCharity.Core.Interfaces.IRepositories
{
    public interface IAdminRepository : IGenericRepository<Admin>
    {
        Task<Admin?> GetByUsernameAsync(string username);
        Task<Admin?> GetByEmailAsync(string email);
        Task<bool> AnyAdminExistsAsync();
    }
}