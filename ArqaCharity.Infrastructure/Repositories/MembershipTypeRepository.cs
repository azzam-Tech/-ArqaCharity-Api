using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Interfaces.IRepositories;
using ArqaCharity.Infrastructure.Data;

namespace ArqaCharity.Infrastructure.Repositories
{
    public class MembershipTypeRepository : GenericRepository<MembershipType>, IMembershipTypeRepository
    {
        public MembershipTypeRepository(AppDbContext context) : base(context) { }
    }
}

