using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Interfaces.IRepositories;
using ArqaCharity.Core.Interfaces.IServices;
using ArqaCharity.Core.Models;

namespace ArqaCharity.Infrastructure.Services
{
    public class FinancialReportService : IFinancialReportService
    {
        private readonly IFinancialReportRepository _repository;

        public FinancialReportService(IFinancialReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<FinancialReport>> AddReportAsync(string reportName, DateTime reportDate, string pdfFilePath)
        {
            try
            {
                var report = new FinancialReport(reportName, reportDate, pdfFilePath);
                await _repository.AddAsync(report);
                return Result<FinancialReport>.Success(report);
            }
            catch (ArgumentException ex)
            {
                return Result<FinancialReport>.Failure(new Error("ValidationError", ex.Message, 400));
            }
        }

        public async Task<Result<IReadOnlyList<FinancialReport>>> GetAllReportsAsync()
        {
            var reports = await _repository.ListAllAsync();
            return Result<IReadOnlyList<FinancialReport>>.Success(reports);
        }

        public async Task<Result<FinancialReport>> GetReportByIdAsync(int id)
        {
            var report = await _repository.GetByIdAsync(id);
            if (report == null)
                return Result<FinancialReport>.Failure(new Error("ReportNotFound", "التقرير غير موجود", 404));

            return Result<FinancialReport>.Success(report);
        }
    }
}
