using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EducaERP.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddRepositories();

        return services;
    }
}