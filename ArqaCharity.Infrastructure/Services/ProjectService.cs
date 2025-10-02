using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Interfaces.IRepositories;
using ArqaCharity.Core.Interfaces.IServices;
using ArqaCharity.Core.Models;

namespace ArqaCharity.Infrastructure.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repo;

        public ProjectService(IProjectRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<Project>> CreateProjectAsync(
            string name, DateTime projectDate, string description,
            string location, string bankAccountNumber, string? promoImageUrl = null)
        {
            try
            {
                var project = new Project(name, projectDate, description, location, bankAccountNumber, promoImageUrl);
                await _repo.AddAsync(project);
                return Result<Project>.Success(project);
            }
            catch (ArgumentException ex)
            {
                return Result<Project>.Failure(new Error("ValidationError", ex.Message, 400));
            }
        }

        public async Task<Result<IReadOnlyList<Project>>> GetAllProjectsAsync()
        {
            var list = await _repo.ListAllAsync();
            return Result<IReadOnlyList<Project>>.Success(list);
        }

        public async Task<Result<Project>> GetProjectByIdAsync(int id)
        {
            var project = await _repo.GetByIdAsync(id);
            if (project == null)
                return Result<Project>.Failure(new Error("ProjectNotFound", "المشروع غير موجود", 404));

            return Result<Project>.Success(project);
        }
    }
}
