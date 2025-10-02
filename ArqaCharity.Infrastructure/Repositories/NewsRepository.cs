using ArqaCharity.Core.Entities;
using ArqaCharity.Core.Interfaces.IRepositories;
using ArqaCharity.Core.Models.DTOs;
using ArqaCharity.Core.Models.DTOs.Newses;
using ArqaCharity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ArqaCharity.Infrastructure.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly AppDbContext _context;

        public NewsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<News> AddAsync(News news)
        {
            await _context.News.AddAsync(news);
            return news;
        }

        public async Task<News?> GetByIdAsync(int id)
        {
            return await _context.News.FindAsync(id);
        }

        public async Task UpdateAsync(News news)
        {
            _context.Entry(news).State = EntityState.Modified;
        }

        public async Task DeleteAsync(News news)
        {
            _context.News.Remove(news);
        }

        public async Task<List<NewsDto>> GetAllAsync()
        {
            return await _context.News
                .OrderByDescending(n => n.NewsDate)
                .Select(n => new NewsDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    Content = n.Content,
                    NewsDate = n.NewsDate,
                    PromoImageUrl = n.PromoImageUrl,
                    CreatedAt = n.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<PagedResult<NewsDto>> GetFilteredAsync(NewsFilterDto filter)
        {
            var query = _context.News.AsQueryable();

            // تطبيق الفلاتر
            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                var searchTerm = filter.SearchTerm.Trim().ToLower();
                query = query.Where(n =>
                    EF.Functions.Like(n.Title.ToLower(), $"%{searchTerm}%") ||
                    EF.Functions.Like(n.Content.ToLower(), $"%{searchTerm}%"));
            }

            if (filter.FromDate.HasValue)
                query = query.Where(n => n.NewsDate >= filter.FromDate.Value);

            if (filter.ToDate.HasValue)
                query = query.Where(n => n.NewsDate <= filter.ToDate.Value);

            // العدد الإجمالي
            var totalCount = await query.CountAsync();

            // التصفح
            var pageSize = Math.Min(filter.PageSize, 100);
            var pageNumber = Math.Max(filter.PageNumber, 1);
            var items = await query
                .OrderByDescending(n => n.NewsDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(n => new NewsDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    Content = n.Content,
                    NewsDate = n.NewsDate,
                    PromoImageUrl = n.PromoImageUrl,
                    CreatedAt = n.CreatedAt
                })
                .ToListAsync();

            return new PagedResult<NewsDto>
            {
                Items = items,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize),
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }

        public Task<IReadOnlyList<News>> ListAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}

