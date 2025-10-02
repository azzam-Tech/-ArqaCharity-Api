using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Interfaces.IServices;
using ArqaCharity.Core.Models;
using ArqaCharity.Core.Models.DTOs.Donations;
using ArqaCharity.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArqaCharity.WebApi.Controllers
{
    /// <summary>
    /// إدارة سجلات التبرعات الواردة إلى الجمعية
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Beneficiaries,SuperAdmin")]
    public class DonationsController : ControllerBase
    {
        private readonly IDonationService _service;
        private readonly AppDbContext _context;

        public DonationsController(IDonationService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        /// <summary>
        /// جلب إجمالي مبلغ التبرعات المستلمة
        /// </summary>
        /// <returns>المبلغ الإجمالي</returns>
        [HttpGet("total")]
        public async Task<ActionResult<ApiResponse<decimal>>> GetTotal()
        {
            var result = await _service.GetTotalDonationsAsync();
            return Ok(ApiResponse<decimal>.Success(result.Value, "إجمالي التبرعات"));
        }

        /// <summary>
        /// جلب جميع التبرعات المرتبطة بمشروع معين
        /// </summary>
        /// <param name="projectId">معرف المشروع</param>
        /// <returns>قائمة التبرعات</returns>
        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<Donation>>>> GetByProject(int projectId)
        {
            var result = await _service.GetDonationsByProjectAsync(projectId);
            return Ok(ApiResponse<IReadOnlyList<Donation>>.Success(result.Value));
        }

        /// <summary>
        /// تسجيل تبرع جديد (يدوي أو عبر النظام).
        /// بيانات الطلب (Request Body):
        /// - projectId: معرف المشروع المتبرع له.
        /// - amount: مبلغ التبرع (أكبر من صفر).
        /// - donorName: اسم المتبرع (اختياري).
        /// </summary>
        /// <returns>بيانات التبرع المسجل</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Donation>>> Record([FromBody] RecordDonationDto dto)
        {
            var result = await _service.RecordDonationAsync(dto.ProjectId, dto.Amount, dto.DonorName);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<Donation>.Failure(result.Error.Message, result.Error.StatusCode));

            await _context.SaveChangesAsync();
            return Ok(ApiResponse<Donation>.Success(result.Value, "تم تسجيل التبرع بنجاح"));
        }
    }

}
