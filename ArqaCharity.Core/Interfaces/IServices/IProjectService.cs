using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Models;

namespace ArqaCharity.Core.Interfaces.IServices
{
    public interface IProjectService
    {
        Task<Result<Project>> CreateProjectAsync(
            string name,
            DateTime projectDate,
            string description,
            string location,
            string bankAccountNumber,
            string? promoImageUrl = null);

        Task<Result<IReadOnlyList<Project>>> GetAllProjectsAsync();
        Task<Result<Project>> GetProjectByIdAsync(int id);
    }


}
