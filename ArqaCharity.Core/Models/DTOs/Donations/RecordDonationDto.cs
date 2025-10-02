namespace ArqaCharity.Core.Models.DTOs.Donations
{
    public class RecordDonationDto
    {
        public int ProjectId { get; set; }
        public decimal Amount { get; set; }
        public string? DonorName { get; set; }
    }
}
