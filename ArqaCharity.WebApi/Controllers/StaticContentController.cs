using ArqaCharity.Core.Interfaces.IServices;
using ArqaCharity.Core.Models;
using ArqaCharity.Core.Models.DTOs;
using ArqaCharity.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ArqaCharity.WebApi.Controllers
{
    /// <summary>
    /// إدارة المحتوى الثابت العام للجمعية (مثل معلومات الاتصال، شركاء النجاح، إلخ)
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "SuperAdmin,ProjectsAndReports")] 
    public class StaticContentController : ControllerBase
    {
        private readonly IStaticContentService _service;
        private readonly AppDbContext _context;

        public StaticContentController(IStaticContentService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        /// <summary>
        /// جلب قائمة بأسماء جميع أنواع المحتوى الثابت (المفاتيح)
        /// </summary>
        /// <returns>قائمة المفاتيح (مثل: "ContactInfo", "AboutUs")</returns>
        [HttpGet("keys")]
        [AllowAnonymous] 
        public async Task<ActionResult<ApiResponse<List<string>>>> GetAllKeys()
        {
            var result = await _service.GetAllKeysAsync();
            return Ok(ApiResponse<List<string>>.Success(result.Value));
        }

        /// <summary>
        /// جلب جميع عناصر المحتوى الثابت مع بياناتها
        /// </summary>
        /// <returns>قائمة كاملة بالمحتوى</returns>
        [HttpGet]
        [AllowAnonymous] 
        public async Task<ActionResult<ApiResponse<List<StaticContentDto>>>> GetAll()
        {
            var result = await _service.GetAllContentsAsync();
            return Ok(ApiResponse<List<StaticContentDto>>.Success(result.Value));
        }

        /// <summary>
        /// جلب محتوى ثابت معين بواسطة مفتاحه
        /// </summary>
        /// <param name="key">مفتاح المحتوى (مثل: "ContactInfo")</param>
        /// <returns>بيانات المحتوى ككائن JSON</returns>
        [HttpGet("{key}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<Dictionary<string, object>>>> GetByKey(string key)
        {
            var result = await _service.GetContentByKeyAsync(key);
            if (!result.IsSuccess)
                return NotFound(ApiResponse<Dictionary<string, object>>.Failure(result.Error.Message, result.Error.StatusCode));

            return Ok(ApiResponse<Dictionary<string, object>>.Success(result.Value));
        }

        /// <summary>
        /// إضافة عنصر جديد من المحتوى الثابت.
        /// بيانات الطلب (Request Body):
        /// - key: مفتاح فريد للمحتوى (مثال: "ContactInfo").
        /// - jsonData: كائن JSON يحتوي على الحقول والقيم (مثال: {"email": "info@arqa.org", "phone": "123456"}).
        /// </summary>
        /// <returns>تأكيد الإضافة</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<object>>> Add([FromBody] AddStaticContentDto dto)
        {
            if (!JsonSerializer.Deserialize<JsonElement>(dto.JsonData).ValueKind.Equals(JsonValueKind.Object))
                return BadRequest(ApiResponse<object>.Failure("يجب أن يكون المحتوى كائن JSON", 400));

            var jsonData = JsonSerializer.Deserialize<JsonElement>(dto.JsonData);
            var result = await _service.AddContentAsync(dto.Key, jsonData);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.Failure(result.Error.Message, result.Error.StatusCode));

            await _context.SaveChangesAsync();
            return Ok(ApiResponse<object>.Success(null, "تم إضافة المحتوى بنجاح"));
        }

        /// <summary>
        /// تعديل محتوى ثابت موجود.
        /// بيانات الطلب (Request Body):
        /// - jsonData: كائن JSON جديد يحتوي على الحقول والقيم المحدثة.
        /// </summary>
        /// <param name="key">مفتاح المحتوى المراد تعديله</param>
        /// <returns>تأكيد التعديل</returns>
        [HttpPut("{key}")]
        public async Task<ActionResult<ApiResponse<object>>> Update(string key, [FromBody] UpdateStaticContentDto dto)
        {
            var jsonData = JsonSerializer.Deserialize<JsonElement>(dto.JsonData);
            var result = await _service.UpdateContentAsync(key, jsonData);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.Failure(result.Error.Message, result.Error.StatusCode));

            await _context.SaveChangesAsync();
            return Ok(ApiResponse<object>.Success(null, "تم تحديث المحتوى بنجاح"));
        }

        /// <summary>
        /// حذف محتوى ثابت
        /// </summary>
        /// <param name="key">مفتاح المحتوى المراد حذفه</param>
        /// <returns>تأكيد الحذف</returns>
        [HttpDelete("{key}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete(string key)
        {
            var result = await _service.DeleteContentAsync(key);
            if (!result.IsSuccess)
                return NotFound(ApiResponse<object>.Failure(result.Error.Message, result.Error.StatusCode));

            await _context.SaveChangesAsync();
            return Ok(ApiResponse<object>.Success(null, "تم حذف المحتوى بنجاح"));
        }
    }

    public class AddStaticContentDto
    {
        public string Key { get; set; } = string.Empty;
        public string JsonData { get; set; } = "{}"; 
    }

    public class UpdateStaticContentDto
    {
        public string JsonData { get; set; } = "{}";
    }
}
