using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Interfaces.IServices;
using ArqaCharity.Core.Models;
using ArqaCharity.Core.Models.DTOs.Memberships;
using ArqaCharity.Core.Models.Enums;
using ArqaCharity.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArqaCharity.WebApi.Controllers
{
    /// <summary>
    /// إدارة عضويات الأفراد في الجمعية
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Memberships,SuperAdmin")]
    public class MembershipsController : ControllerBase
    {
        private readonly IMembershipService _service;
        private readonly AppDbContext _context;

        public MembershipsController(IMembershipService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        /// <summary>
        /// جلب جميع العضويات المسجلة
        /// </summary>
        /// <returns>قائمة العضويات</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<Membership>>>> GetAll()
        {
            var result = await _service.GetAllMembershipsAsync();
            return Ok(ApiResponse<IReadOnlyList<Membership>>.Success(result.Value));
        }

        /// <summary>
        /// تسجيل عضوية جديدة.
        /// بيانات الطلب (Request Body):
        /// - name: الاسم الكامل.
        /// - phoneNumber: رقم الجوال.
        /// - email: البريد الإلكتروني (يجب أن يكون فريدًا).
        /// - nationality: الجنسية.
        /// - dateOfBirth: تاريخ الميلاد (يجب أن يكون قبل 10 سنوات على الأقل).
        /// - profession: المهنة.
        /// - academicQualification: المؤهل العلمي.
        /// - maritalStatus: الحالة الاجتماعية (0 = أعزب, 1 = متزوج, 2 = مطلق, 3 = أرمل).
        /// - nationalId: رقم الهوية الوطنية (يجب أن يكون فريدًا).
        /// - membershipTypeId: معرف نوع العضوية (يجب أن يكون موجودًا في النظام).
        /// </summary>
        /// <returns>بيانات العضوية المسجلة</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Membership>>> Register([FromBody] RegisterMembershipDto dto)
        {
            var result = await _service.RegisterMembershipAsync(
                dto.Name, dto.PhoneNumber, dto.Email, dto.Nationality, dto.DateOfBirth,
                dto.Profession, dto.AcademicQualification, dto.MaritalStatus, dto.NationalId, dto.MembershipTypeId);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<Membership>.Failure(result.Error.Message, result.Error.StatusCode));

            await _context.SaveChangesAsync();
            return Ok(ApiResponse<Membership>.Success(result.Value, "تم تسجيل العضوية بنجاح"));
        }
    }

}
