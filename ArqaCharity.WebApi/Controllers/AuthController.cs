using ArqaCharity.Core.Interfaces.IServices;
using ArqaCharity.Core.Models;
using ArqaCharity.Core.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArqaCharity.WebApi.Controllers
{
    /// <summary>
    /// تسجيل دخول المسؤولين إلى النظام
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// تسجيل دخول مسؤول باستخدام اسم المستخدم (أو البريد) وكلمة المرور.
        /// بيانات الطلب (Request Body):
        /// - usernameOrEmail: اسم المستخدم أو البريد الإلكتروني.
        /// - password: كلمة المرور (6 أحرف على الأقل).
        /// </summary>
        /// <returns>رمز JWT صالح للاستخدام في الطلبات اللاحقة</returns>
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<string>>> Login([FromBody] LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto.UsernameOrEmail, dto.Password);
            if (result.IsSuccess)
                return Ok(ApiResponse<string>.Success(result.Value, "تم تسجيل الدخول بنجاح"));

            return Unauthorized(ApiResponse<string>.Failure(result.Error.Message, result.Error.StatusCode));
        }
    }

}
