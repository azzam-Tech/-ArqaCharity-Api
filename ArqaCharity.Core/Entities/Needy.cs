namespace ArqaCharity.Core.Entities
{
    public class Needy
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public decimal RequiredAmount { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Needy(string name, string phoneNumber, decimal requiredAmount)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
            RequiredAmount = requiredAmount > 0 ? requiredAmount : throw new ArgumentException("المبلغ المطلوب يجب أن يكون أكبر من صفر", nameof(requiredAmount));
            CreatedAt = DateTime.UtcNow;
        }
    }
}
