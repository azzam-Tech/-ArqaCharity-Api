using ArqaCharity.Core.Models.Enums;

namespace ArqaCharity.Core.Entities
{
    public class Volunteer
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Nationality { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string Profession { get; private set; }
        public WorkShift WorkShift { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Volunteer(string name, string phoneNumber, string nationality, DateTime dateOfBirth, string profession, WorkShift workShift)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
            Nationality = nationality ?? throw new ArgumentNullException(nameof(nationality));
            Profession = profession ?? throw new ArgumentNullException(nameof(profession));

            if (dateOfBirth >= DateTime.UtcNow.AddYears(-10))
                throw new ArgumentException("تاريخ الميلاد غير منطقي", nameof(dateOfBirth));

            DateOfBirth = dateOfBirth;
            WorkShift = workShift;
            CreatedAt = DateTime.UtcNow;
        }
    }
}

