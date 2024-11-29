using BaseballStats.WebApi;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices();

builder.Services.AddAuthenticationJwtBearer(options =>
{
    options.SigningKey = builder.Configuration["Jwt:SigningKey"];
});
builder.Services.AddAuthorization();

builder.Services
    .AddFastEndpoints()
    .SwaggerDocument()
    .AddDbContext<AppDbContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("SupabaseConnection"));
    });


var app = builder.Build();

app.UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints()
    .UseSwaggerGen();

app.UseHttpsRedirection();

app.Run();