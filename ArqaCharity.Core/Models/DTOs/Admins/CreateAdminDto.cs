using ArqaCharity.Core.Models.Enums;

namespace ArqaCharity.Core.Models.DTOs.Admins
{
    public class CreateAdminDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public AdminRole Role { get; set; }
    }
}
