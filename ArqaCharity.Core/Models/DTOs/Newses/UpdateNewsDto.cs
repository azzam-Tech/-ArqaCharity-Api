namespace ArqaCharity.Core.Models.DTOs.Newses
{
    public class UpdateNewsDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime NewsDate { get; set; }
        public string? PromoImageUrl { get; set; }
    }

}
