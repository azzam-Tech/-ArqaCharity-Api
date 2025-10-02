using ArqaCharity.Core.Entities;

namespace ArqaCharity.Core.Interfaces.IRepositories
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<Project?> GetByNameAsync(string name);
    }
}
