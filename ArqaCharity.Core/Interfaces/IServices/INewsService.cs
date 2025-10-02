using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Models;
using ArqaCharity.Core.Models.DTOs;
using ArqaCharity.Core.Models.DTOs.Newses;

namespace ArqaCharity.Core.Interfaces.IServices
{
    public interface INewsService
    {
        Task<Result<NewsDto>> CreateAsync(CreateNewsDto dto);
        Task<Result<NewsDto>> UpdateAsync(int id, UpdateNewsDto dto);
        Task<Result> DeleteAsync(int id);
        Task<Result<NewsDto>> GetByIdAsync(int id);
        Task<Result<PagedResult<NewsDto>>> GetFilteredAsync(NewsFilterDto filter);
        Task<Result<List<NewsDto>>> GetAllAsync();
    }


}
