using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EducaERP.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static WebApplication ConfigureRequestPipeline(this WebApplication app)
    {
        // Configuração do ambiente de desenvolvimento
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.ConfigureSwagger();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.ConfigureHealthChecks();
        app.ConfigureErrorHandling();

        return app;
    }

    private static void ConfigureSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "EducaERP API V1");
            c.RoutePrefix = "swagger";
            c.DisplayRequestDuration();
            c.EnableDeepLinking();
        });
    }

    private static void ConfigureHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.MapHealthChecksUI(options =>
        {
            options.UIPath = "/health-ui";
        });
    }

    private static void ConfigureErrorHandling(this WebApplication app)
    {
        app.UseExceptionHandler("/error");

        app.Map("/error", (HttpContext context) =>
        {
            var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
            return Results.Problem(
                title: "Ocorreu um erro inesperado",
                detail: exception?.Message,
                statusCode: StatusCodes.Status500InternalServerError);
        });
    }
}