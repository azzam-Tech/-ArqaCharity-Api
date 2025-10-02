// File: WebApi/Program.cs

using ArqaCharity.WebApi.Extensions;
using ArqaCharity.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDatabase(builder.Configuration)          
    .AddApplicationServices()                    
    .AddJwtAuthentication(builder.Configuration) 
    .AddCorsPolicies()                           
    .AddResponseCompressionServices()            
    .AddRateLimitingServices()                   
    .AddSwaggerDocumentation();                  

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ArqaCharity API v1"));
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseResponseCompression();

app.UseAuthentication(); 
app.UseAuthorization();  

app.MapControllers();

app.Run();
