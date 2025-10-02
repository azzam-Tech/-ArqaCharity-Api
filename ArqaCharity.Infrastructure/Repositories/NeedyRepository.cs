using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Interfaces.IRepositories;
using ArqaCharity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ArqaCharity.Infrastructure.Repositories
{
    public class NeedyRepository : GenericRepository<Needy>, INeedyRepository
    {
        public NeedyRepository(AppDbContext context) : base(context) { }

        public async Task<Needy?> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.Needy.FirstOrDefaultAsync(n => n.PhoneNumber == phoneNumber);
        }

        public async Task<bool> ExistsByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.Needy.AnyAsync(n => n.PhoneNumber == phoneNumber);
        }
    }
}

