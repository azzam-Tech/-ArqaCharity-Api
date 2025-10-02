using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Models.DTOs;
using ArqaCharity.Core.Models.DTOs.Newses;
namespace ArqaCharity.Core.Interfaces.IRepositories
{
    public interface INewsRepository : IGenericRepository<News>
    {
        Task<News> AddAsync(News news);
        Task<News?> GetByIdAsync(int id);
        Task UpdateAsync(News news);
        Task DeleteAsync(News news);
        Task<List<NewsDto>> GetAllAsync();
        Task<PagedResult<NewsDto>> GetFilteredAsync(NewsFilterDto filter);
    }
}
