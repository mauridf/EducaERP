using EducaERP.API.Extensions;
using EducaERP.Application.Extensions;
using EducaERP.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configuração hierárquica das camadas
builder.Services
    .AddApiServices()              // Configurações da API
    .AddApplicationServices()      // Serviços da camada de aplicação
    .AddInfrastructureServices(builder.Configuration)  // Infraestrutura
    .AddSecurityServices(builder.Configuration)        // Autenticação/JWT
    .AddDocumentationServices()    // Swagger/OpenAPI
    .AddHealthCheckServices(builder.Configuration)     // Health Checks
    .AddPerformanceServices();     // Rate limiting, compression

var app = builder.Build();

// Configuração do pipeline de requisições
app.ConfigureRequestPipeline();

app.Run();