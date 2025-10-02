using ArqaCharity.Core.Entities;

namespace ArqaCharity.Core.Interfaces.IRepositories
{
    public interface IStaticContentRepository : IGenericRepository<StaticContent>
    {
        Task<StaticContent?> GetByKeyAsync(string key);
        Task<bool> KeyExistsAsync(string key);
        Task DeleteByKeyAsync(string key);
    }
}
