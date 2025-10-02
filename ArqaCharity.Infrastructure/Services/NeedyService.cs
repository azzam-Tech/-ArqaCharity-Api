using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Interfaces.IRepositories;
using ArqaCharity.Core.Interfaces.IServices;
using ArqaCharity.Core.Models;

namespace ArqaCharity.Infrastructure.Services
{
    public class NeedyService : INeedyService
    {
        private readonly INeedyRepository _needyRepository;

        public NeedyService(INeedyRepository needyRepository)
        {
            _needyRepository = needyRepository;
        }

        public async Task<Result<Needy>> AddNeedyAsync(string name, string phoneNumber, decimal requiredAmount)
        {
            try
            {
                // محاولة إنشاء الكيان — سيُرمي استثناء إذا كانت البيانات غير صالحة
                var needy = new Needy(name, phoneNumber, requiredAmount);

                // التحقق من التكرار
                if (await _needyRepository.ExistsByPhoneNumberAsync(phoneNumber))
                {
                    return Result<Needy>.Failure(new Error("NeedyExists", "المحتاج مسجل مسبقًا برقم الجوال هذا", 409));
                }

                await _needyRepository.AddAsync(needy);
                return Result<Needy>.Success(needy);
            }
            catch (ArgumentException ex)
            {
                return Result<Needy>.Failure(new Error("ValidationError", ex.Message, 400));
            }
        }

        public async Task<Result<IReadOnlyList<Needy>>> GetAllNeedyAsync()
        {
            var list = await _needyRepository.ListAllAsync();
            return Result<IReadOnlyList<Needy>>.Success(list);
        }

        public async Task<Result<Needy>> GetNeedyByIdAsync(int id)
        {
            var needy = await _needyRepository.GetByIdAsync(id);
            if (needy == null)
                return Result<Needy>.Failure(new Error("NeedyNotFound", "المحتاج غير موجود", 404));

            return Result<Needy>.Success(needy);
        }
    }
}
