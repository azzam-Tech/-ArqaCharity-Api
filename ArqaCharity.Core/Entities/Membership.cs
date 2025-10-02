using ArqaCharity.Core.Models.Enums;

namespace ArqaCharity.Core.Entities
{
    public class Membership
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string Nationality { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string Profession { get; private set; }
        public string AcademicQualification { get; private set; }
        public MaritalStatus MaritalStatus { get; private set; }
        public string NationalId { get; private set; }
        public int MembershipTypeId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Membership(
            string name,
            string phoneNumber,
            string email,
            string nationality,
            DateTime dateOfBirth,
            string profession,
            string academicQualification,
            MaritalStatus maritalStatus,
            string nationalId,
            int membershipTypeId)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Nationality = nationality ?? throw new ArgumentNullException(nameof(nationality));
            Profession = profession ?? throw new ArgumentNullException(nameof(profession));
            AcademicQualification = academicQualification ?? throw new ArgumentNullException(nameof(academicQualification));
            NationalId = nationalId ?? throw new ArgumentNullException(nameof(nationalId));

            if (dateOfBirth >= DateTime.UtcNow.AddYears(-10))
                throw new ArgumentException("تاريخ الميلاد غير منطقي", nameof(dateOfBirth));

            DateOfBirth = dateOfBirth;
            MaritalStatus = maritalStatus;
            MembershipTypeId = membershipTypeId;
            CreatedAt = DateTime.UtcNow;
        }
    }
}



