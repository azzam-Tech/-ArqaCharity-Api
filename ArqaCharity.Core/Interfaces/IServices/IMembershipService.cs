using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Models;
using ArqaCharity.Core.Models.Enums;

namespace ArqaCharity.Core.Interfaces.IServices
{
    public interface IMembershipService
    {
        Task<Result<Membership>> RegisterMembershipAsync(
            string name,
            string phoneNumber,
            string email,
            string nationality,
            DateTime dateOfBirth,
            string profession,
            string academicQualification,
            MaritalStatus maritalStatus,
            string nationalId,
            int membershipTypeId);

        Task<Result<IReadOnlyList<Membership>>> GetAllMembershipsAsync();
        Task<Result<Membership>> GetMembershipByIdAsync(int id);
    }


}
