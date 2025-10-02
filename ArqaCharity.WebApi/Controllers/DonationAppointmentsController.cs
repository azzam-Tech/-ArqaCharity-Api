using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Interfaces.IServices;
using ArqaCharity.Core.Models;
using ArqaCharity.Core.Models.DTOs.DonationAppointments;
using ArqaCharity.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArqaCharity.WebApi.Controllers
{
    /// <summary>
    /// إدارة مواعيد التبرع التي يحجزها المتبرعون لزيارة مقر الجمعية
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Beneficiaries,SuperAdmin")]
    public class DonationAppointmentsController : ControllerBase
    {
        private readonly IDonationAppointmentService _service;
        private readonly AppDbContext _context;

        public DonationAppointmentsController(IDonationAppointmentService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        /// <summary>
        /// جلب جميع طلبات التبرع قيد الانتظار (غير معتمدة بعد)
        /// </summary>
        /// <returns>قائمة الطلبات قيد الانتظار</returns>
        [HttpGet("pending")]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<DonationAppointment>>>> GetPending()
        {
            var result = await _service.GetPendingAppointmentsAsync();
            return Ok(ApiResponse<IReadOnlyList<DonationAppointment>>.Success(result.Value));
        }
        /// <summary>
        /// حجز موعد تبرع جديد.
        /// بيانات الطلب (Request Body):
        /// - name: الاسم الكامل.
        /// - phoneNumber: رقم الجوال.
        /// - nationalId: رقم الهوية الوطنية.
        /// - appointmentDate: موعد التبرع (يجب أن يكون في المستقبل).
        /// </summary>
        /// <returns>بيانات الموعد المحجوز</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<DonationAppointment>>> Create([FromBody] CreateAppointmentDto dto)
        {
            var result = await _service.CreateAppointmentAsync(dto.Name, dto.PhoneNumber, dto.NationalId, dto.AppointmentDate);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<DonationAppointment>.Failure(result.Error.Message, result.Error.StatusCode));

            await _context.SaveChangesAsync();
            return Ok(ApiResponse<DonationAppointment>.Success(result.Value, "تم حجز الموعد بنجاح"));
        }


        /// <summary>
        /// اعتماد طلب تبرع (قبوله)
        /// </summary>
        /// <param name="id">معرف الطلب</param>
        /// <returns>تأكيد القبول</returns>
        [HttpPatch("{id}/approve")]
        public async Task<ActionResult<ApiResponse<object>>> Approve(int id)
        {
            var result = await _service.UpdateAppointmentAsync(id, a => a.Approve());
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.Failure(result.Error.Message, result.Error.StatusCode));

            await _context.SaveChangesAsync();
            return Ok(ApiResponse<object>.Success(null, "تم قبول الطلب"));
        }


        /// <summary>
        /// رفض طلب تبرع
        /// </summary>
        /// <param name="id">معرف الطلب</param>
        /// <returns>تأكيد الرفض</returns>
        [HttpPatch("{id}/reject")]
        public async Task<ActionResult<ApiResponse<object>>> Reject(int id)
        {
            var result = await _service.UpdateAppointmentAsync(id, a => a.Reject());
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.Failure(result.Error.Message, result.Error.StatusCode));

            await _context.SaveChangesAsync();
            return Ok(ApiResponse<object>.Success(null, "تم رفض الطلب"));
        }
    }

}
