using ArqaCharity.Core.Entities;

namespace ArqaCharity.Core.Interfaces.IRepositories
{
    public interface IDonationRepository : IGenericRepository<Donation>
    {
        Task<List<Donation>> GetDonationsByProjectIdAsync(int projectId);
        Task<decimal> GetTotalDonationsAmountAsync();
    }
}
