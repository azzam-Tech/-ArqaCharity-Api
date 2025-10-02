using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Interfaces.IRepositories;
using ArqaCharity.Core.Interfaces.IServices;
using ArqaCharity.Core.Models;

namespace ArqaCharity.Infrastructure.Services
{
    public class DonationService : IDonationService
    {
        private readonly IDonationRepository _repo;
        private readonly IProjectRepository _projectRepo;

        public DonationService(IDonationRepository repo, IProjectRepository projectRepo)
        {
            _repo = repo;
            _projectRepo = projectRepo;
        }

        public async Task<Result<Donation>> RecordDonationAsync(int projectId, decimal amount, string? donorName = null)
        {
            // التحقق من وجود المشروع
            var project = await _projectRepo.GetByIdAsync(projectId);
            if (project == null)
                return Result<Donation>.Failure(new Error("ProjectNotFound", "المشروع غير موجود", 404));

            try
            {
                var donation = new Donation(projectId, amount, donorName);
                await _repo.AddAsync(donation);
                return Result<Donation>.Success(donation);
            }
            catch (ArgumentException ex)
            {
                return Result<Donation>.Failure(new Error("ValidationError", ex.Message, 400));
            }
        }

        public async Task<Result<IReadOnlyList<Donation>>> GetDonationsByProjectAsync(int projectId)
        {
            var donations = await _repo.GetDonationsByProjectIdAsync(projectId);
            return Result<IReadOnlyList<Donation>>.Success(donations);
        }

        public async Task<Result<decimal>> GetTotalDonationsAsync()
        {
            var total = await _repo.GetTotalDonationsAmountAsync();
            return Result<decimal>.Success(total);
        }
    }
}
