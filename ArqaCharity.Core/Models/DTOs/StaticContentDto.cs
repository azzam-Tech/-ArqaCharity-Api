namespace ArqaCharity.Core.Models.DTOs
{
    public class StaticContentDto
    {
        public int Id { get; set; }
        public string Key { get; set; } = string.Empty;
        public Dictionary<string, object> Data { get; set; } = new();
        public DateTime LastUpdated { get; set; }
    }
}
