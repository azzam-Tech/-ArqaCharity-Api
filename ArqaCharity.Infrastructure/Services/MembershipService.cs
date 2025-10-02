using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Interfaces.IRepositories;
using ArqaCharity.Core.Interfaces.IServices;
using ArqaCharity.Core.Models;
using ArqaCharity.Core.Models.Enums;

namespace ArqaCharity.Infrastructure.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly IMembershipRepository _repo;

        public MembershipService(IMembershipRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<Membership>> RegisterMembershipAsync(
            string name, string phoneNumber, string email, string nationality,
            DateTime dateOfBirth, string profession, string academicQualification,
            MaritalStatus maritalStatus, string nationalId, int membershipTypeId)
        {
            try
            {
                // التحقق من التكرار
                if (await _repo.ExistsByNationalIdAsync(nationalId))
                    return Result<Membership>.Failure(new Error("MembershipExists", "العضوية مسجلة مسبقًا برقم الهوية", 409));

                if (await _repo.ExistsByEmailAsync(email))
                    return Result<Membership>.Failure(new Error("EmailExists", "البريد الإلكتروني مستخدم مسبقًا", 409));

                var membership = new Membership(
                    name, phoneNumber, email, nationality, dateOfBirth,
                    profession, academicQualification, maritalStatus, nationalId, membershipTypeId);

                await _repo.AddAsync(membership);
                return Result<Membership>.Success(membership);
            }
            catch (ArgumentException ex)
            {
                return Result<Membership>.Failure(new Error("ValidationError", ex.Message, 400));
            }
        }

        public async Task<Result<IReadOnlyList<Membership>>> GetAllMembershipsAsync()
        {
            var list = await _repo.ListAllAsync();
            return Result<IReadOnlyList<Membership>>.Success(list);
        }

        public async Task<Result<Membership>> GetMembershipByIdAsync(int id)
        {
            var membership = await _repo.GetByIdAsync(id);
            if (membership == null)
                return Result<Membership>.Failure(new Error("MembershipNotFound", "العضوية غير موجودة", 404));

            return Result<Membership>.Success(membership);
        }
    }
}
