namespace ArqaCharity.Core.Models.DTOs.DonationAppointments
{
    public class CreateAppointmentDto
    {
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string NationalId { get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }
    }
}
