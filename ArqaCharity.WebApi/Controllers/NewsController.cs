using ArqaCharity.Core.Interfaces.IServices;
using ArqaCharity.Core.Models;
using ArqaCharity.Core.Models.DTOs;
using ArqaCharity.Core.Models.DTOs.Newses;
using ArqaCharity.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArqaCharity.WebApi.Controllers
{
    /// <summary>
    /// إدارة الأخبار والمنشورات الرسمية للجمعية
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "ProjectsAndReports,SuperAdmin")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _service;
        private readonly AppDbContext _context;

        public NewsController(INewsService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        /// <summary>
        /// جلب جميع الأخبار (بدون تصفح)
        /// </summary>
        /// <returns>قائمة جميع الأخبار مرتبة بتاريخ النشر (الأحدث أولًا)</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<List<NewsDto>>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(ApiResponse<List<NewsDto>>.Success(result.Value));
        }

        /// <summary>
        /// جلب الأخبار مع دعم الفلترة والتصفح
        /// </summary>
        /// <param name="filter">معايير الفلترة (البحث، التواريخ، التصفح)</param>
        /// <returns>قائمة الأخبار المصفاة مع معلومات التصفح</returns>
        [HttpGet("filtered")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<PagedResult<NewsDto>>>> GetFiltered([FromQuery] NewsFilterDto filter)
        {
            var result = await _service.GetFilteredAsync(filter);
            return Ok(ApiResponse<PagedResult<NewsDto>>.Success(result.Value));
        }

        /// <summary>
        /// جلب خبر معين بواسطة معرفه
        /// </summary>
        /// <param name="id">معرف الخبر</param>
        /// <returns>بيانات الخبر المطلوب</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<NewsDto>>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (!result.IsSuccess)
                return NotFound(ApiResponse<NewsDto>.Failure(result.Error.Message, result.Error.StatusCode));

            return Ok(ApiResponse<NewsDto>.Success(result.Value));
        }

        /// <summary>
        /// إنشاء خبر جديد
        /// </summary>
        /// <param name="dto">بيانات الخبر الجديد</param>
        /// <returns>بيانات الخبر المنشأ</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<NewsDto>>> Create([FromBody] CreateNewsDto dto)
        {
            var result = await _service.CreateAsync(dto);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<NewsDto>.Failure(result.Error.Message, result.Error.StatusCode));

            await _context.SaveChangesAsync();
            return Ok(ApiResponse<NewsDto>.Success(result.Value, "تم إنشاء الخبر بنجاح"));
        }

        /// <summary>
        /// تعديل خبر موجود
        /// </summary>
        /// <param name="id">معرف الخبر المراد تعديله</param>
        /// <param name="dto">بيانات الخبر المحدثة</param>
        /// <returns>بيانات الخبر المعدّل</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<NewsDto>>> Update(int id, [FromBody] UpdateNewsDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            if (!result.IsSuccess)
                return result.Error.StatusCode == 404
                    ? NotFound(ApiResponse<NewsDto>.Failure(result.Error.Message, result.Error.StatusCode))
                    : BadRequest(ApiResponse<NewsDto>.Failure(result.Error.Message, result.Error.StatusCode));

            await _context.SaveChangesAsync();
            return Ok(ApiResponse<NewsDto>.Success(result.Value, "تم تعديل الخبر بنجاح"));
        }

        /// <summary>
        /// حذف خبر
        /// </summary>
        /// <param name="id">معرف الخبر المراد حذفه</param>
        /// <returns>تأكيد الحذف</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result.IsSuccess)
                return NotFound(ApiResponse<object>.Failure(result.Error.Message, result.Error.StatusCode));

            await _context.SaveChangesAsync();
            return Ok(ApiResponse<object>.Success(null, "تم حذف الخبر بنجاح"));
        }
    }


}
