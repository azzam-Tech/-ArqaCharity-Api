namespace ArqaCharity.Core.Models.DTOs.FinancialReports
{
    public class AddReportDto
    {
        public string ReportName { get; set; } = string.Empty;
        public DateTime ReportDate { get; set; }
        public string PdfFilePath { get; set; } = string.Empty;
    }
}
