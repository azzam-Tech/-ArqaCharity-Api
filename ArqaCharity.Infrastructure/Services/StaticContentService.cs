using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Interfaces.IRepositories;
using ArqaCharity.Core.Interfaces.IServices;
using ArqaCharity.Core.Models;
using ArqaCharity.Core.Models.DTOs;
using System.Text.Json;

namespace ArqaCharity.Infrastructure.Services
{
    public class StaticContentService : IStaticContentService
    {
        private readonly IStaticContentRepository _repository;

        public StaticContentService(IStaticContentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> AddContentAsync(string key, JsonElement jsonData)
        {
            if (string.IsNullOrWhiteSpace(key))
                return Result.Failure(new Error("InvalidKey", "المفتاح مطلوب", 400));

            if (await _repository.KeyExistsAsync(key))
                return Result.Failure(new Error("KeyExists", "المفتاح مستخدم مسبقًا", 409));

            var jsonDataString = jsonData.GetRawText();
            var content = new StaticContent(key, jsonDataString);
            await _repository.AddAsync(content);
            return Result.Success();
        }

        public async Task<Result> UpdateContentAsync(string key, JsonElement jsonData)
        {
            var existing = await _repository.GetByKeyAsync(key);
            if (existing == null)
                return Result.Failure(new Error("NotFound", "المحتوى غير موجود", 404));

            existing.UpdateData(jsonData.GetRawText());
            await _repository.UpdateAsync(existing);
            return Result.Success();
        }

        public async Task<Result> DeleteContentAsync(string key)
        {
            if (!await _repository.KeyExistsAsync(key))
                return Result.Failure(new Error("NotFound", "المحتوى غير موجود", 404));

            await _repository.DeleteByKeyAsync(key);
            return Result.Success();
        }

        public async Task<Result<Dictionary<string, object>>> GetContentByKeyAsync(string key)
        {
            var content = await _repository.GetByKeyAsync(key);
            if (content == null)
                return Result<Dictionary<string, object>>.Failure(new Error("NotFound", "المحتوى غير موجود", 404));

            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(content.JsonData)
                       ?? new Dictionary<string, object>();
            return Result<Dictionary<string, object>>.Success(data);
        }

        public async Task<Result<List<StaticContentDto>>> GetAllContentsAsync()
        {
            var contents = await _repository.ListAllAsync();
            var dtos = contents.Select(c => new StaticContentDto
            {
                Id = c.Id,
                Key = c.Key,
                Data = JsonSerializer.Deserialize<Dictionary<string, object>>(c.JsonData) ?? new(),
                LastUpdated = c.LastUpdated
            }).ToList();

            return Result<List<StaticContentDto>>.Success(dtos);
        }

        public async Task<Result<List<string>>> GetAllKeysAsync()
        {
            var contents = await _repository.ListAllAsync();
            var keys = contents.Select(c => c.Key).ToList();
            return Result<List<string>>.Success(keys);
        }
    }
}
