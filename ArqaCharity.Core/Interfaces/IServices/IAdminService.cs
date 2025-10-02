using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Models;
using ArqaCharity.Core.Models.Enums;

namespace ArqaCharity.Core.Interfaces.IServices
{
    public interface IAdminService
    {
        Task<Result<Admin>> CreateEmergencyAdminAsync(string username, string email, string password, AdminRole role);
        Task<Result<Admin>> CreateAdminAsync(string creatorRoleName, string username, string email, string password, AdminRole role);
        Task<bool> AnyAdminExistsAsync();
    }
}
