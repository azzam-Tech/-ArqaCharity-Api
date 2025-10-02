using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Interfaces.IRepositories;
using ArqaCharity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ArqaCharity.Infrastructure.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(AppDbContext context) : base(context) { }

        public async Task<Project?> GetByNameAsync(string name)
        {
            return await _context.Projects.FirstOrDefaultAsync(p => p.Name == name);
        }
    }
}

