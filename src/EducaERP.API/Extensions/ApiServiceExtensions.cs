using EducaERP.Application.Extensions;
using EducaERP.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Threading.RateLimiting;

namespace EducaERP.API.Extensions;

public static class ApiServiceExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = false;
        });

        return services;
    }

    public static IServiceCollection AddSecurityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthenticationServices(configuration);
        services.AddAuthorization();

        return services;
    }

    public static IServiceCollection AddDocumentationServices(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "EducaERP API",
                Version = "v1",
                Description = "API do sistema EducaERP",
                Contact = new OpenApiContact
                {
                    Name = "Suporte EducaERP",
                    Email = "mauridf@gmail.com"
                }
            });

            // Configuração de segurança JWT
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });

            // Configura para agrupar por tags
            c.TagActionsBy(api => new[] { api.GroupName });
            c.DocInclusionPredicate((name, api) => true);

            // Adiciona documentação XML se existir
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                c.IncludeXmlComments(xmlPath);
            }
        });

        return services;
    }

    public static IServiceCollection AddHealthCheckServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddDbContextCheck<EducaERPDbContext>()
            .AddNpgSql(configuration.GetConnectionString("DefaultConnection"));

        services.AddHealthChecksUI(setup =>
        {
            setup.SetEvaluationTimeInSeconds(60);
            setup.MaximumHistoryEntriesPerEndpoint(50);
            setup.AddHealthCheckEndpoint("EducaERP API", "/health");
        }).AddInMemoryStorage();

        return services;
    }

    public static IServiceCollection AddPerformanceServices(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: context.Request.Headers.Host.ToString(),
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = 100,
                        Window = TimeSpan.FromMinutes(1)
                    }));
        });

        services.AddResponseCompression(options =>
        {
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
            options.EnableForHttps = true;
        });

        return services;
    }
}