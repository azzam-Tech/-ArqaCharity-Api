using ArqaCharity.Core.Models;
using Microsoft.AspNetCore.RateLimiting;
using System.Text.Json;
using System.Threading.RateLimiting;

namespace ArqaCharity.WebApi.Extensions
{
    public static class RateLimitPolicyNames
    {
        public const string General = "General";
        public const string Login = "Login";
    }

    public static class RateLimitingExtensions
    {
        public static IServiceCollection AddRateLimitingServices(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter(policyName: RateLimitPolicyNames.General, o =>
                {
                    o.PermitLimit = 100;
                    o.Window = TimeSpan.FromMinutes(10);
                    o.QueueLimit = 0;
                });

                options.AddFixedWindowLimiter(policyName: RateLimitPolicyNames.Login, o =>
                {
                    o.PermitLimit = 5;
                    o.Window = TimeSpan.FromMinutes(1);
                    o.QueueLimit = 0;
                });

                options.OnRejected = async (context, cancellationToken) =>
                {
                    if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                    {
                        context.HttpContext.Response.Headers.RetryAfter = retryAfter.ToString();
                    }

                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    context.HttpContext.Response.ContentType = "application/json";

                    var response = ApiResponse<object>.Failure(
                        message: "تم تجاوز حد الطلبات. يرجى المحاولة لاحقًا.",
                        statusCode: 429,
                        errors: new List<string> { "Rate limit exceeded." }
                    );

                    var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                    await context.HttpContext.Response.WriteAsync(json, cancellationToken);
                };
            });

            return services;
        }
    }
}
