using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Interfaces.IServices;
using ArqaCharity.Core.Models;
using ArqaCharity.Core.Models.DTOs.Needys;
using ArqaCharity.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArqaCharity.WebApi.Controllers
{
    /// <summary>
    /// إدارة بيانات المحتاجين المسجلين لدى الجمعية
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Beneficiaries,SuperAdmin")]
    public class NeedyController : ControllerBase
    {
        private readonly INeedyService _needyService;
        private readonly AppDbContext _context;

        public NeedyController(INeedyService needyService, AppDbContext context)
        {
            _needyService = needyService;
            _context = context;
        }
        /// <summary>
        /// جلب قائمة بجميع المحتاجين
        /// </summary>
        /// <returns>قائمة المحتاجين</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<Needy>>>> GetAll()
        {
            var result = await _needyService.GetAllNeedyAsync();
            return Ok(ApiResponse<IReadOnlyList<Needy>>.Success(result.Value));
        }

        /// <summary>
        /// جلب بيانات محتاج معين بواسطة معرفه
        /// </summary>
        /// <param name="id">معرف المحتاج</param>
        /// <returns>بيانات المحتاج</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Needy>>> GetById(int id)
        {
            var result = await _needyService.GetNeedyByIdAsync(id);
            if (!result.IsSuccess)
                return NotFound(ApiResponse<Needy>.Failure(result.Error.Message, result.Error.StatusCode));

            return Ok(ApiResponse<Needy>.Success(result.Value));
        }

        /// <summary>
        /// إضافة محتاج جديد إلى النظام.
        /// بيانات الطلب (Request Body):
        /// - name: الاسم الكامل.
        /// - phoneNumber: رقم الجوال (يجب أن يكون فريدًا).
        /// - requiredAmount: المبلغ المطلوب (أكبر من صفر).
        /// </summary>
        /// <returns>بيانات المحتاج المضاف</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Needy>>> Add([FromBody] CreateNeedyDto dto)
        {
            var result = await _needyService.AddNeedyAsync(dto.Name, dto.PhoneNumber, dto.RequiredAmount);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<Needy>.Failure(result.Error.Message, result.Error.StatusCode));

            await _context.SaveChangesAsync();
            return Ok(ApiResponse<Needy>.Success(result.Value, "تم إضافة المحتاج بنجاح"));
        }
    }
}
