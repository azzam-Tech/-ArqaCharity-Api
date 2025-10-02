namespace ArqaCharity.WebApi.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCorsPolicies(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()  
                          .AllowAnyMethod()  
                          .AllowAnyHeader(); 
                });
            });

            return services;
        }
    }
}
