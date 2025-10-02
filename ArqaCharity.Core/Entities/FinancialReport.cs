namespace ArqaCharity.Core.Entities
{
    public class FinancialReport
    {
        public int Id { get; private set; }
        public string ReportName { get; private set; }
        public DateTime ReportDate { get; private set; }
        public string PdfFilePath { get; private set; } // مسار الملف المحفوظ على السيرفر أو CDN
        public DateTime CreatedAt { get; private set; }

        public FinancialReport(string reportName, DateTime reportDate, string pdfFilePath)
        {
            ReportName = reportName ?? throw new ArgumentNullException(nameof(reportName));
            PdfFilePath = pdfFilePath ?? throw new ArgumentNullException(nameof(pdfFilePath));
            ReportDate = reportDate;
            CreatedAt = DateTime.UtcNow;
        }
    }
}