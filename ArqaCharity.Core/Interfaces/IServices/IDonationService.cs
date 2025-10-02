using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Models;

namespace ArqaCharity.Core.Interfaces.IServices
{
    public interface IDonationService
    {
        Task<Result<Donation>> RecordDonationAsync(int projectId, decimal amount, string? donorName = null);
        Task<Result<IReadOnlyList<Donation>>> GetDonationsByProjectAsync(int projectId);
        Task<Result<decimal>> GetTotalDonationsAsync();
    }


}
