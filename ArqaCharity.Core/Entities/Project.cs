namespace ArqaCharity.Core.Entities
{
    public class Project
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateTime ProjectDate { get; private set; }
        public string Description { get; private set; }
        public string Location { get; private set; }
        public string? PromoImageUrl { get; private set; }
        public string BankAccountNumber { get; private set; } // رقم الحساب البنكي للتبرع
        public DateTime CreatedAt { get; private set; }

        public Project(string name, DateTime projectDate, string description, string location, string bankAccountNumber, string? promoImageUrl = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Location = location ?? throw new ArgumentNullException(nameof(location));
            BankAccountNumber = bankAccountNumber ?? throw new ArgumentNullException(nameof(bankAccountNumber));

            ProjectDate = projectDate;
            PromoImageUrl = promoImageUrl;
            CreatedAt = DateTime.UtcNow;
        }
    }
}



