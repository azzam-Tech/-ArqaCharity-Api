using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Models;

namespace ArqaCharity.Core.Interfaces.IServices
{
    public interface INeedyService
    {
        Task<Result<Needy>> AddNeedyAsync(string name, string phoneNumber, decimal requiredAmount);
        Task<Result<IReadOnlyList<Needy>>> GetAllNeedyAsync();
        Task<Result<Needy>> GetNeedyByIdAsync(int id);
    }


}
