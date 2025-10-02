using ArqaCharity.Core.Interfaces.IServices;
using ArqaCharity.Core.Models;
using ArqaCharity.Core.Models.DTOs;
using ArqaCharity.Core.Models.Enums;
using ArqaCharity.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace ArqaCharity.WebApi.Controllers
{
    /// <summary>
    /// كونترولر الطوارئ لإعادة تنشيط النظام في حال فقدان جميع حسابات المسؤولين
    /// </summary>
    [ApiController]
    [Route("emergency")]
    [DisableRateLimiting]
    public class EmergencyController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly AppDbContext _context;
        private readonly string _expectedSecretKey;

        public EmergencyController(
            IAdminService adminService,
            AppDbContext context,
            IConfiguration configuration)
        {
            _adminService = adminService;
            _context = context;
            _expectedSecretKey = configuration["EmergencySettings:SecretKey"]
                                 ?? throw new InvalidOperationException("Emergency Secret Key is missing in configuration.");
        }

        /// <summary>
        /// التحقق مما إذا كان هناك أي مسؤول مسجل في النظام
        /// </summary>
        /// <returns> true إذا وُجد مسؤول، false إذا لم يوجد</returns>
        [HttpGet("check")]
        public async Task<ActionResult<ApiResponse<bool>>> CheckAdminExists()
        {
            var exists = await _adminService.AnyAdminExistsAsync();
            var message = exists ? "يوجد مسؤولون في النظام." : "لا يوجد أي مسؤول في النظام.";
            return Ok(ApiResponse<bool>.Success(exists, message));
        }

        /// <summary>
        /// إنشاء حساب مسؤول طوارئ (SuperAdmin) باستخدام مفتاح سري معروف فقط لصاحب المشروع.
        /// بيانات الطلب (Request Body):
        /// - secretKey: المفتاح السري المتفق عليه (مثال: "ArqaCharityEmergencyKey2025!").
        /// - username: اسم المستخدم الجديد (يجب أن يكون فريدًا).
        /// - email: البريد الإلكتروني (يجب أن يكون فريدًا).
        /// - password: كلمة المرور (6 أحرف على الأقل).
        /// </summary>
        /// <returns>تأكيد إنشاء الحساب بنجاح</returns>
        [HttpPost("create")]
        public async Task<ActionResult<ApiResponse<object>>> CreateEmergencyAdmin([FromBody] CreateEmergencyAdminDto dto)
        {
            // التحقق من المفتاح السري
            if (string.IsNullOrWhiteSpace(dto.SecretKey) || dto.SecretKey != _expectedSecretKey)
            {
                return BadRequest(ApiResponse<object>.Failure("مفتاح الطوارئ غير صحيح.", 403));
            }

            var result = await _adminService.CreateEmergencyAdminAsync(
                dto.Username,
                dto.Email,
                dto.Password,
                AdminRole.SuperAdmin); // دائمًا SuperAdmin في الطوارئ

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse<object>.Failure(result.Error.Message, result.Error.StatusCode));
            }

            // حفظ التغييرات في قاعدة البيانات
            await _context.SaveChangesAsync();

            return Ok(ApiResponse<object>.Success(null, "تم إنشاء حساب المسؤول بنجاح. يمكنك الآن تسجيل الدخول."));
        }
    }

}

