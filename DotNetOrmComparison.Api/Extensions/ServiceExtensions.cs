using System.Text.Json;
using DotNetOrmComparison.Core.Contracts.Repositories;
using DotNetOrmComparison.Core.Contracts.Services;
using DotNetOrmComparison.Data.Dapper;
using DotNetOrmComparison.Data.EntityFramework;
using DotNetOrmComparison.Services;
using Microsoft.EntityFrameworkCore;

namespace DotNetOrmComparison.Api.Extensions;

public static class ServiceExtensions
{
    public static void ConfigurePersistence(this IServiceCollection services)
    {
        var appDbConnection = Environment.GetEnvironmentVariable("DOTNET_ORM_COMPARISON_DB_CONNECTION");

        // EntityFramework
        services.AddDbContext<AppDbContext>(opts =>
        {
            opts.UseNpgsql(appDbConnection);
            opts.EnableSensitiveDataLogging();
        });
        services.AddScoped<IEmployeeEntityFrameworkRepository, EmployeeEntityFrameworkRepository>();
        services.AddScoped<IEmployeeEntityFrameworkService, EmployeeEntityFrameworkService>();

        // Dapper
        services.AddScoped<DapperDbContext>(provider =>
            new DapperDbContext(appDbConnection!));
        services.AddScoped<IEmployeeDapperRepository, EmployeeDapperRepository>();
        services.AddScoped<IEmployeeDapperService, EmployeeDapperService>();
    }

    public static void ConfigureControllers(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        });
    }
}