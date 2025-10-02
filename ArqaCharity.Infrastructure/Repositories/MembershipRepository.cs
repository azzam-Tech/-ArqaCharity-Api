using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Interfaces.IRepositories;
using ArqaCharity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ArqaCharity.Infrastructure.Repositories
{
    public class MembershipRepository : GenericRepository<Membership>, IMembershipRepository
    {
        public MembershipRepository(AppDbContext context) : base(context) { }

        public async Task<Membership?> GetByNationalIdAsync(string nationalId)
        {
            return await _context.Memberships.FirstOrDefaultAsync(m => m.NationalId == nationalId);
        }

        public async Task<Membership?> GetByEmailAsync(string email)
        {
            return await _context.Memberships.FirstOrDefaultAsync(m => m.Email == email);
        }

        public async Task<bool> ExistsByNationalIdAsync(string nationalId)
        {
            return await _context.Memberships.AnyAsync(m => m.NationalId == nationalId);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Memberships.AnyAsync(m => m.Email == email);
        }
    }
}

