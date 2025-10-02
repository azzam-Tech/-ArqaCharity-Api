using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Interfaces.IRepositories;
using ArqaCharity.Infrastructure.Data;

namespace ArqaCharity.Infrastructure.Repositories
{
    public class FinancialReportRepository : GenericRepository<FinancialReport>, IFinancialReportRepository
    {
        public FinancialReportRepository(AppDbContext context) : base(context) { }
    }
}

