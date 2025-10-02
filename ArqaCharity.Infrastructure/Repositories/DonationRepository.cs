using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Interfaces.IRepositories;
using ArqaCharity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ArqaCharity.Infrastructure.Repositories
{
    public class DonationRepository : GenericRepository<Donation>, IDonationRepository
    {
        public DonationRepository(AppDbContext context) : base(context) { }

        public async Task<List<Donation>> GetDonationsByProjectIdAsync(int projectId)
        {
            return await _context.Donations
                .Where(d => d.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalDonationsAmountAsync()
        {
            return await _context.Donations.SumAsync(d => d.Amount);
        }
    }
}

