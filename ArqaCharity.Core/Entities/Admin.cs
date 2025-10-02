using ArqaCharity.Core.Models.Enums;

namespace ArqaCharity.Core.Entities
{
    public class Admin
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; } // سيتم تشفيره لاحقًا
        public AdminRole Role { get; private set; }
        public DateTime CreatedAt { get; private set; }

        // للاستخدام الداخلي فقط (مثل الطوارئ)
        public Admin(string username, string email, string passwordHash, AdminRole role)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
            Role = role;
            CreatedAt = DateTime.UtcNow;
        }
    }
}


