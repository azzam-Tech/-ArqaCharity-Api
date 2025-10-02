using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Interfaces.IRepositories;
using ArqaCharity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ArqaCharity.Infrastructure.Repositories
{
    public class VolunteerRepository : GenericRepository<Volunteer>, IVolunteerRepository
    {
        public VolunteerRepository(AppDbContext context) : base(context) { }

        public async Task<Volunteer?> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.Volunteers.FirstOrDefaultAsync(v => v.PhoneNumber == phoneNumber);
        }

        public async Task<bool> ExistsByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.Volunteers.AnyAsync(v => v.PhoneNumber == phoneNumber);
        }
    }
}

