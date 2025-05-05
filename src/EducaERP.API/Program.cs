using EducaERP.API.Extensions;
using EducaERP.Application.Extensions;
using EducaERP.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configura��o hier�rquica das camadas
builder.Services
    .AddApiServices()              // Configura��es da API
    .AddApplicationServices()      // Servi�os da camada de aplica��o
    .AddInfrastructureServices(builder.Configuration)  // Infraestrutura
    .AddSecurityServices(builder.Configuration)        // Autentica��o/JWT
    .AddDocumentationServices()    // Swagger/OpenAPI
    .AddHealthCheckServices(builder.Configuration)     // Health Checks
    .AddPerformanceServices();     // Rate limiting, compression

var app = builder.Build();

// Configura��o do pipeline de requisi��es
app.ConfigureRequestPipeline();

app.Run();