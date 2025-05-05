using EducaERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EducaERP.Infrastructure.Extensions;

public static class DbContextExtensions
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EducaERPDbContext>(options =>
        {
            if (configuration.GetValue<bool>("DatabaseOptions:UseInMemoryDatabase"))
            {
                options.UseInMemoryDatabase("EducaERPInMemoryDb");
            }
            else
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    npgsqlOptions => npgsqlOptions.MigrationsAssembly(typeof(EducaERPDbContext).Assembly.FullName));
            }

            options.EnableSensitiveDataLogging(
                configuration.GetValue<bool>("DatabaseOptions:EnableSensitiveDataLogging"));

            options.EnableDetailedErrors(
                configuration.GetValue<bool>("DatabaseOptions:EnableDetailedErrors"));
        });

        return services;
    }
}