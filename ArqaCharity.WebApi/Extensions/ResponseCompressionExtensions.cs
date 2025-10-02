using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

namespace ArqaCharity.WebApi.Extensions
{
    public static class ResponseCompressionExtensions
    {
        public static IServiceCollection AddResponseCompressionServices(this IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true; 
                options.Providers.Add<BrotliCompressionProvider>(); 
                options.Providers.Add<GzipCompressionProvider>();   

                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    "application/json",
                    "application/xml",
                    "text/plain",
                    "text/html"
                });
            });

            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            return services;
        }
    }
}
