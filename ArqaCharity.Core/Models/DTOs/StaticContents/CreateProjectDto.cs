namespace ArqaCharity.Core.Models.DTOs.StaticContents
{
    public class CreateProjectDto
    {
        public string Name { get; set; } = string.Empty;
        public DateTime ProjectDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string BankAccountNumber { get; set; } = string.Empty;
        public string? PromoImageUrl { get; set; }
    }
}
