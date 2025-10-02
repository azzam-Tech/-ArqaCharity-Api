using ArqaCharity.Core.Models;

namespace ArqaCharity.Core.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<Result<string>> LoginAsync(string usernameOrEmail, string password);
    }


}
