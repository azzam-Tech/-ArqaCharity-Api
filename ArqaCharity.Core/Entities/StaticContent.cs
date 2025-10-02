using System.ComponentModel.DataAnnotations;

namespace ArqaCharity.Core.Entities
{
    public class StaticContent
    {
        public int Id { get; private set; }

        [Required, MaxLength(100)]
        public string Key { get; private set; }

        [Required]
        public string JsonData { get; private set; }

        public DateTime LastUpdated { get; private set; } = DateTime.UtcNow;

        public StaticContent(string key, string jsonData)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            JsonData = jsonData ?? throw new ArgumentNullException(nameof(jsonData));
            LastUpdated = DateTime.UtcNow;
        }

        public void UpdateData(string jsonData)
        {
            JsonData = jsonData ?? throw new ArgumentNullException(nameof(jsonData));
            LastUpdated = DateTime.UtcNow;
        }
    }

}


