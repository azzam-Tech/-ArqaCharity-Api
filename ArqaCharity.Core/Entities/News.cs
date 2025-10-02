namespace ArqaCharity.Core.Entities
{
    public class News
    {
        public int Id { get;  set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public DateTime NewsDate { get; private set; }
        public string? PromoImageUrl { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public News(string title, string content, DateTime newsDate, string? promoImageUrl = null)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Content = content ?? throw new ArgumentNullException(nameof(content));
            NewsDate = newsDate;
            PromoImageUrl = promoImageUrl;
            CreatedAt = DateTime.UtcNow;
        }
    }
}



