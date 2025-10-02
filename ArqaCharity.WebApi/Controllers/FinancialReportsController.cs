using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Interfaces.IServices;
using ArqaCharity.Core.Models;
using ArqaCharity.Core.Models.DTOs.FinancialReports;
using ArqaCharity.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArqaCharity.WebApi.Controllers
{
    /// <summary>
    /// إدارة التقارير المالية للجمعية (تقارير شهرية/سنوية)
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "ProjectsAndReports,SuperAdmin")]
    public class FinancialReportsController : ControllerBase
    {
        private readonly IFinancialReportService _service;
        private readonly AppDbContext _context;

        public FinancialReportsController(IFinancialReportService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        /// <summary>
        /// جلب جميع التقارير المالية
        /// </summary>
        /// <returns>قائمة التقارير</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<FinancialReport>>>> GetAll()
        {
            var result = await _service.GetAllReportsAsync();
            return Ok(ApiResponse<IReadOnlyList<FinancialReport>>.Success(result.Value));
        }

        /// <summary>
        /// إضافة تقرير مالي جديد.
        /// بيانات الطلب (Request Body):
        /// - reportName: اسم التقرير.
        /// - reportDate: تاريخ إصدار التقرير.
        /// - pdfFilePath: مسار ملف PDF المحفوظ على الخادم.
        /// </summary>
        /// <returns>بيانات التقرير المضاف</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<FinancialReport>>> Add([FromBody] AddReportDto dto)
        {
            var result = await _service.AddReportAsync(dto.ReportName, dto.ReportDate, dto.PdfFilePath);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<FinancialReport>.Failure(result.Error.Message, result.Error.StatusCode));

            await _context.SaveChangesAsync();
            return Ok(ApiResponse<FinancialReport>.Success(result.Value, "تم إضافة التقرير بنجاح"));
        }
    }

}
