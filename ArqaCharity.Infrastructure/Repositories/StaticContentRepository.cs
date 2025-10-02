using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Interfaces.IRepositories;
using ArqaCharity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ArqaCharity.Infrastructure.Repositories
{
    public class StaticContentRepository : GenericRepository<StaticContent>, IStaticContentRepository
    {
        public StaticContentRepository(AppDbContext context) : base(context) { }

        public async Task<StaticContent?> GetByKeyAsync(string key)
        {
            return await _context.Set<StaticContent>().FirstOrDefaultAsync(s => s.Key == key);
        }

        public async Task<bool> KeyExistsAsync(string key)
        {
            return await _context.Set<StaticContent>().AnyAsync(s => s.Key == key);
        }

        public async Task DeleteByKeyAsync(string key)
        {
            var entity = await GetByKeyAsync(key);
            if (entity != null)
                _context.Set<StaticContent>().Remove(entity);
        }
    }
}

