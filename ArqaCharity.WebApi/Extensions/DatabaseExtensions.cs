﻿using ArqaCharity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ArqaCharity.WebApi.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
