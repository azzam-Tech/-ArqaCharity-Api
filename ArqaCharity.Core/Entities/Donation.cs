namespace ArqaCharity.Core.Entities
{
    public class Donation
    {
        public int Id { get; private set; }
        public string? DonorName { get; private set; } // قد يكون فارغًا
        public int ProjectId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime DonationDate { get; private set; }

        public Donation(int projectId, decimal amount, string? donorName = null)
        {
            if (amount <= 0)
                throw new ArgumentException("مبلغ التبرع يجب أن يكون أكبر من صفر", nameof(amount));

            ProjectId = projectId;
            Amount = amount;
            DonorName = donorName;
            DonationDate = DateTime.UtcNow;
        }
    }
}



