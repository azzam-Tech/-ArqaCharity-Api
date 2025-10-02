namespace ArqaCharity.Core.Models.DTOs.Newses
{
    public class NewsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime NewsDate { get; set; }
        public string? PromoImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
