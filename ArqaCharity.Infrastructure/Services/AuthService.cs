using ArqaCharity.Core.Interfaces.IRepositories;
using ArqaCharity.Core.Interfaces.IServices;
using ArqaCharity.Core.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ArqaCharity.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IAdminRepository adminRepository, IConfiguration configuration)
        {
            _adminRepository = adminRepository;
            _configuration = configuration;
        }

        public async Task<Result<string>> LoginAsync(string usernameOrEmail, string password)
        {
            if (string.IsNullOrWhiteSpace(usernameOrEmail) || string.IsNullOrWhiteSpace(password))
                return Result<string>.Failure(new Error("InvalidCredentials", "اسم المستخدم وكلمة المرور مطلوبان", 400));

            // البحث بالاسم أو البريد
            var admin = await _adminRepository.GetByUsernameAsync(usernameOrEmail);
            if (admin == null)
                admin = await _adminRepository.GetByEmailAsync(usernameOrEmail);

            if (admin == null)
                return Result<string>.Failure(new Error("InvalidCredentials", "بيانات الاعتماد غير صحيحة", 401));

            if (!VerifyPassword(password, admin.PasswordHash))
                return Result<string>.Failure(new Error("InvalidCredentials", "بيانات الاعتماد غير صحيحة", 401));

            // إنشاء JWT Token
            var token = GenerateJwtToken(admin);
            return Result<string>.Success(token);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            var parts = hashedPassword.Split('.', 2);
            if (parts.Length != 2) return false;

            var salt = Convert.FromBase64String(parts[0]);
            var storedHash = Convert.FromBase64String(parts[1]);

            var hashedInput = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);

            return hashedInput.SequenceEqual(storedHash);
        }

        private string GenerateJwtToken(Core.Entities.Admin admin)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]!);
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
            new Claim(ClaimTypes.Name, admin.Username),
            new Claim(ClaimTypes.Email, admin.Email),
            new Claim(ClaimTypes.Role, admin.Role.ToString())
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(8),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
