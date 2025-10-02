using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Interfaces.IRepositories;
using ArqaCharity.Core.Interfaces.IServices;
using ArqaCharity.Core.Models;
using ArqaCharity.Core.Models.DTOs;
using ArqaCharity.Core.Models.DTOs.Newses;

namespace ArqaCharity.Infrastructure.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _repository;

        public NewsService(INewsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<NewsDto>> CreateAsync(CreateNewsDto dto)
        {
            try
            {
                var news = new News(dto.Title, dto.Content, dto.NewsDate, dto.PromoImageUrl);
                await _repository.AddAsync(news);
                return Result<NewsDto>.Success(MapToDto(news));
            }
            catch (ArgumentException ex)
            {
                return Result<NewsDto>.Failure(new Error("ValidationError", ex.Message, 400));
            }
        }

        public async Task<Result<NewsDto>> UpdateAsync(int id, UpdateNewsDto dto)
        {
            var existingNews = await _repository.GetByIdAsync(id);
            if (existingNews == null)
                return Result<NewsDto>.Failure(new Error("NotFound", "الخبر غير موجود", 404));

            try
            {
                var news = new News(dto.Title, dto.Content, dto.NewsDate, dto.PromoImageUrl);
                news.Id = id;
                await _repository.UpdateAsync(news);
                return Result<NewsDto>.Success(MapToDto(news));
            }
            catch (ArgumentException ex)
            {
                return Result<NewsDto>.Failure(new Error("ValidationError", ex.Message, 400));
            }
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var news = await _repository.GetByIdAsync(id);
            if (news == null)
                return Result.Failure(new Error("NotFound", "الخبر غير موجود", 404));

            await _repository.DeleteAsync(news);
            return Result.Success();
        }

        public async Task<Result<NewsDto>> GetByIdAsync(int id)
        {
            var news = await _repository.GetByIdAsync(id);
            if (news == null)
                return Result<NewsDto>.Failure(new Error("NotFound", "الخبر غير موجود", 404));

            return Result<NewsDto>.Success(MapToDto(news));
        }

        public async Task<Result<PagedResult<NewsDto>>> GetFilteredAsync(NewsFilterDto filter)
        {
            var result = await _repository.GetFilteredAsync(filter);
            return Result<PagedResult<NewsDto>>.Success(result);
        }

        public async Task<Result<List<NewsDto>>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();
            return Result<List<NewsDto>>.Success(result);
        }

        private static NewsDto MapToDto(News news)
        {
            return new NewsDto
            {
                Id = news.Id,
                Title = news.Title,
                Content = news.Content,
                NewsDate = news.NewsDate,
                PromoImageUrl = news.PromoImageUrl,
                CreatedAt = news.CreatedAt
            };
        }

    }
}
