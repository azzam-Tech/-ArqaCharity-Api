using ArqaCharity.Core.Models;
using ArqaCharity.Core.Models.DTOs;
using System.Text.Json;

namespace ArqaCharity.Core.Interfaces.IServices
{
    public interface IStaticContentService
    {
        Task<Result> AddContentAsync(string key, JsonElement jsonData);
        Task<Result> UpdateContentAsync(string key, JsonElement jsonData);
        Task<Result> DeleteContentAsync(string key);
        Task<Result<Dictionary<string, object>>> GetContentByKeyAsync(string key);
        Task<Result<List<StaticContentDto>>> GetAllContentsAsync();
        Task<Result<List<string>>> GetAllKeysAsync();
    }
}
