using ArqaCharity.Core.Interfaces.IServices;
using ArqaCharity.Core.Models;
using ArqaCharity.Core.Models.DTOs.Admins;
using ArqaCharity.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArqaCharity.WebApi.Controllers
{
    /// <summary>
    /// إدارة حسابات المسؤولين (الإضافة فقط — التعديل والحذف غير مدعومين حاليًا)
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "SuperAdmin")]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly AppDbContext _context;

        public AdminsController(IAdminService adminService, AppDbContext context)
        {
            _adminService = adminService;
            _context = context;
        }

        /// <summary>
        /// إنشاء مسؤول جديد بواسطة SuperAdmin.
        /// بيانات الطلب (Request Body):
        /// - username: اسم المستخدم (يجب أن يكون فريدًا).
        /// - email: البريد الإلكتروني (يجب أن يكون فريدًا).
        /// - password: كلمة المرور (6 أحرف على الأقل).
        /// - role: صلاحية المسؤول (1 = ProjectsAndReports, 2 = Beneficiaries, 3 = Volunteers, 4 = Memberships, 99 = SuperAdmin).
        /// </summary>
        /// <returns>تأكيد إنشاء الحساب بنجاح</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<object>>> CreateAdmin([FromBody] CreateAdminDto dto)
        {
            var creatorRole = User.FindFirst("role")?.Value;
            if (string.IsNullOrEmpty(creatorRole))
                return Forbid();

            var result = await _adminService.CreateAdminAsync(creatorRole, dto.Username, dto.Email, dto.Password, dto.Role);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.Failure(result.Error.Message, result.Error.StatusCode));

            await _context.SaveChangesAsync();
            return Ok(ApiResponse<object>.Success(null, "تم إنشاء المسؤول بنجاح"));
        }
    }

}
