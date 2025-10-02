using ArqaCharity.Core.Interfaces.IRepositories;
using ArqaCharity.Core.Interfaces.IServices;
using ArqaCharity.Infrastructure.Repositories;
using ArqaCharity.Infrastructure.Services;

namespace ArqaCharity.WebApi.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<INeedyRepository, NeedyRepository>();
            services.AddScoped<IDonationAppointmentRepository, DonationAppointmentRepository>();
            services.AddScoped<IVolunteerRepository, VolunteerRepository>();
            services.AddScoped<IMembershipTypeRepository, MembershipTypeRepository>();
            services.AddScoped<IMembershipRepository, MembershipRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IFinancialReportRepository, FinancialReportRepository>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<IDonationRepository, DonationRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IStaticContentRepository, StaticContentRepository>();

            // Services
            services.AddScoped<INeedyService, NeedyService>();
            services.AddScoped<IDonationAppointmentService, DonationAppointmentService>();
            services.AddScoped<IVolunteerService, VolunteerService>();
            services.AddScoped<IMembershipService, MembershipService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IFinancialReportService, FinancialReportService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IDonationService, DonationService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IStaticContentService, StaticContentService>();

            return services;
        }
    }
}
