using ArqaCharity.Core.Models.Enums;

namespace ArqaCharity.Core.Models.DTOs.Memberships
{
    public class RegisterMembershipDto
    {
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Profession { get; set; } = string.Empty;
        public string AcademicQualification { get; set; } = string.Empty;
        public MaritalStatus MaritalStatus { get; set; }
        public string NationalId { get; set; } = string.Empty;
        public int MembershipTypeId { get; set; }
    }
}
