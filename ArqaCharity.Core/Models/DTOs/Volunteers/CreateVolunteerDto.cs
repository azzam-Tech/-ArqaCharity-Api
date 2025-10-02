using ArqaCharity.Core.Models.Enums;

namespace ArqaCharity.Core.Models.DTOs.Volunteers
{

    public class CreateVolunteerDto
    {
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Profession { get; set; } = string.Empty;
        public WorkShift WorkShift { get; set; }
    }
}
