using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Interfaces.IRepositories;
using ArqaCharity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ArqaCharity.Infrastructure.Repositories
{
    public class AdminRepository : GenericRepository<Admin>, IAdminRepository
    {
        public AdminRepository(AppDbContext context) : base(context) { }

        public async Task<Admin?> GetByUsernameAsync(string username)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.Username == username);
        }

        public async Task<Admin?> GetByEmailAsync(string email)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<bool> AnyAdminExistsAsync()
        {
            return await _context.Admins.AnyAsync();
        }
    }
}

