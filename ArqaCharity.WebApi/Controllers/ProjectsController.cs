using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Interfaces.IServices;
using ArqaCharity.Core.Models;
using ArqaCharity.Core.Models.DTOs.StaticContents;
using ArqaCharity.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArqaCharity.WebApi.Controllers
{
    /// <summary>
    /// إدارة مشاريع الجمعية الخيرية (مثل حفر الآبار، بناء المدارس، إلخ)
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "ProjectsAndReports,SuperAdmin")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _service;
        private readonly AppDbContext _context;

        public ProjectsController(IProjectService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        /// <summary>
        /// جلب جميع المشاريع
        /// </summary>
        /// <returns>قائمة المشاريع</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<Project>>>> GetAll()
        {
            var result = await _service.GetAllProjectsAsync();
            return Ok(ApiResponse<IReadOnlyList<Project>>.Success(result.Value));
        }

        /// <summary>
        /// إنشاء مشروع خيري جديد.
        /// بيانات الطلب (Request Body):
        /// - name: اسم المشروع.
        /// - projectDate: تاريخ بدء المشروع.
        /// - description: وصف تفصيلي للمشروع.
        /// - location: موقع تنفيذ المشروع.
        /// - bankAccountNumber: رقم الحساب البنكي للتبرعات.
        /// - promoImageUrl: رابط الصورة الدعائية (اختياري).
        /// </summary>
        /// <returns>بيانات المشروع المنشأ</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Project>>> Create([FromBody] CreateProjectDto dto)
        {
            var result = await _service.CreateProjectAsync(
                dto.Name, dto.ProjectDate, dto.Description, dto.Location, dto.BankAccountNumber, dto.PromoImageUrl);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<Project>.Failure(result.Error.Message, result.Error.StatusCode));

            await _context.SaveChangesAsync();
            return Ok(ApiResponse<Project>.Success(result.Value, "تم إنشاء المشروع بنجاح"));
        }
    }
}
