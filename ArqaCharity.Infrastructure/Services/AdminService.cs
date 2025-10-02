using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Interfaces.IRepositories;
using ArqaCharity.Core.Interfaces.IServices;
using ArqaCharity.Core.Models;
using ArqaCharity.Core.Models.Enums;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace ArqaCharity.Infrastructure.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<Result<Admin>> CreateAdminAsync(
            string creatorRoleName,
            string username,
            string email,
            string password,
            AdminRole role)
        {
            // تحويل اسم الدور إلى Enum
            if (!Enum.TryParse<AdminRole>(creatorRoleName, out var creatorRole))
                return Result<Admin>.Failure(new Error("InvalidRole", "دور المسؤول غير معروف", 403));

            // التحقق من الصلاحيات: فقط SuperAdmin يمكنه إنشاء أي Admin
            if (creatorRole != AdminRole.SuperAdmin)
                return Result<Admin>.Failure(new Error("Unauthorized", "غير مسموح لك بإنشاء مسؤولين", 403));

            // نفس منطق التحقق من CreateEmergencyAdminAsync
            if (string.IsNullOrWhiteSpace(username))
                return Result<Admin>.Failure(new Error("InvalidUsername", "اسم المستخدم مطلوب", 400));

            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
                return Result<Admin>.Failure(new Error("InvalidEmail", "البريد الإلكتروني غير صالح", 400));

            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                return Result<Admin>.Failure(new Error("WeakPassword", "كلمة المرور يجب أن تحتوي على 6 أحرف على الأقل", 400));

            if (await _adminRepository.GetByUsernameAsync(username) != null)
                return Result<Admin>.Failure(new Error("UsernameExists", "اسم المستخدم مستخدم مسبقًا", 409));

            if (await _adminRepository.GetByEmailAsync(email) != null)
                return Result<Admin>.Failure(new Error("EmailExists", "البريد الإلكتروني مستخدم مسبقًا", 409));

            var passwordHash = HashPassword(password);
            var admin = new Admin(username, email, passwordHash, role);
            await _adminRepository.AddAsync(admin);

            return Result<Admin>.Success(admin);
        }

        public async Task<bool> AnyAdminExistsAsync()
        {
            return await _adminRepository.AnyAdminExistsAsync();
        }

        public async Task<Result<Admin>> CreateEmergencyAdminAsync(string username, string email, string password, AdminRole role)
        {
            // التحقق من صحة المدخلات
            if (string.IsNullOrWhiteSpace(username))
                return Result<Admin>.Failure(new Error("InvalidUsername", "اسم المستخدم مطلوب", 400));

            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
                return Result<Admin>.Failure(new Error("InvalidEmail", "البريد الإلكتروني غير صالح", 400));

            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                return Result<Admin>.Failure(new Error("WeakPassword", "كلمة المرور يجب أن تحتوي على 6 أحرف على الأقل", 400));

            // التحقق من عدم وجود مستخدم بنفس الاسم أو البريد
            var existingByUsername = await _adminRepository.GetByUsernameAsync(username);
            if (existingByUsername != null)
                return Result<Admin>.Failure(new Error("UsernameExists", "اسم المستخدم مستخدم مسبقًا", 409));

            var existingByEmail = await _adminRepository.GetByEmailAsync(email);
            if (existingByEmail != null)
                return Result<Admin>.Failure(new Error("EmailExists", "البريد الإلكتروني مستخدم مسبقًا", 409));

            // تشفير كلمة المرور (بدون Identity — طريقة بسيطة)
            var passwordHash = HashPassword(password);

            var admin = new Admin(username, email, passwordHash, role);
            await _adminRepository.AddAsync(admin);
            // لن نحفظ هنا لأننا سنعتمد على Unit of Work لاحقًا (أو نحفظ في الكونترولر)
            // لكن لتبسيط الأمور، سنفترض أن SaveChanges يتم في الكونترولر أو عبر UnitOfWork

            return Result<Admin>.Success(admin);
        }

        private string HashPassword(string password)
        {
            // استخدام PBKDF2 (آمن)
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return $"{Convert.ToBase64String(salt)}.{hashed}";
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
