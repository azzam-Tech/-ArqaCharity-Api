using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Models;

namespace ArqaCharity.Core.Interfaces.IServices
{
    public interface IFinancialReportService
    {
        Task<Result<FinancialReport>> AddReportAsync(string reportName, DateTime reportDate, string pdfFilePath);
        Task<Result<IReadOnlyList<FinancialReport>>> GetAllReportsAsync();
        Task<Result<FinancialReport>> GetReportByIdAsync(int id);
    }
}
