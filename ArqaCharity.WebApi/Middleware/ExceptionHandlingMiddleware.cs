using ArqaCharity.Core.Models;
using System.Net;
using System.Text.Json;

namespace ArqaCharity.WebApi.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var statusCode = GetStatusCode(exception);
        var message = GetErrorMessage(exception, statusCode);

        var response = ApiResponse<object>.Failure(message, statusCode);

        context.Response.StatusCode = statusCode;

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var jsonResponse = JsonSerializer.Serialize(response, options);

        await context.Response.WriteAsync(jsonResponse);
    }

    private static int GetStatusCode(Exception exception) => exception switch
    {
        ArgumentException => (int)HttpStatusCode.BadRequest,
        KeyNotFoundException => (int)HttpStatusCode.NotFound,
        UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
        _ => (int)HttpStatusCode.InternalServerError
    };

    private static string GetErrorMessage(Exception exception, int statusCode) => statusCode switch
    {
        400 => "الطلب غير صالح. يرجى التحقق من البيانات المدخلة.",
        404 => "الموارد المطلوبة غير موجودة.",
        401 => "غير مصرح لك بالوصول إلى هذه الموارد.",
        _ => "حدث خطأ داخلي في الخادم. يرجى المحاولة لاحقًا."
    };
}