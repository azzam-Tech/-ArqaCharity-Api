using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Interfaces.IServices;
using ArqaCharity.Core.Models;
using ArqaCharity.Core.Models.DTOs.Volunteers;
using ArqaCharity.Core.Models.Enums;
using ArqaCharity.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArqaCharity.WebApi.Controllers
{
    /// <summary>
    /// إدارة بيانات المتطوعين في الجمعية
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Volunteers,SuperAdmin")]
    public class VolunteersController : ControllerBase
    {
        private readonly IVolunteerService _service;
        private readonly AppDbContext _context;

        public VolunteersController(IVolunteerService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        /// <summary>
        /// جلب قائمة بجميع المتطوعين
        /// </summary>
        /// <returns>قائمة المتطوعين</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<Volunteer>>>> GetAll()
        {
            var result = await _service.GetAllVolunteersAsync();
            return Ok(ApiResponse<IReadOnlyList<Core.Entities.Volunteer>>.Success(result.Value));
        }

        /// <summary>
        /// تسجيل متطوع جديد.
        /// بيانات الطلب (Request Body):
        /// - name: الاسم الكامل.
        /// - phoneNumber: رقم الجوال (يجب أن يكون فريدًا).
        /// - nationality: الجنسية.
        /// - dateOfBirth: تاريخ الميلاد (يجب أن يكون قبل 10 سنوات على الأقل).
        /// - profession: المهنة.
        /// - workShift: وقت العمل (0 = صباحي, 1 = مسائي).
        /// </summary>
        /// <returns>بيانات المتطوع المسجل</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Volunteer>>> Add([FromBody] CreateVolunteerDto dto)
        {
            var result = await _service.AddVolunteerAsync(
                dto.Name, dto.PhoneNumber, dto.Nationality, dto.DateOfBirth, dto.Profession, dto.WorkShift);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<Volunteer>.Failure(result.Error.Message, result.Error.StatusCode));

            await _context.SaveChangesAsync();
            return Ok(ApiResponse<Volunteer>.Success(result.Value, "تم تسجيل المتطوع بنجاح"));
        }
    }

}
