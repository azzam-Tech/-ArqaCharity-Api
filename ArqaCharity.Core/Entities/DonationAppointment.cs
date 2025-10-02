using ArqaCharity.Core.Models.Enums;

namespace ArqaCharity.Core.Entities
{
    public class DonationAppointment
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public string NationalId { get; private set; }
        public DateTime AppointmentDate { get; private set; }
        public AppointmentStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public DonationAppointment(string name, string phoneNumber, string nationalId, DateTime appointmentDate)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
            NationalId = nationalId ?? throw new ArgumentNullException(nameof(nationalId));
            if (appointmentDate <= DateTime.UtcNow)
                throw new ArgumentException("موعد التبرع يجب أن يكون في المستقبل", nameof(appointmentDate));

            AppointmentDate = appointmentDate;
            Status = AppointmentStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }

        public void Approve() => Status = AppointmentStatus.Approved;
        public void Reject() => Status = AppointmentStatus.Rejected;
    }

}


