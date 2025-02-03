using Microsoft.OpenApi.Models;
using Pedidos.Api.Endpoints;

namespace Pedidos.Api.Configurations;

public static class OpenApiConfiguration
{
    public static IServiceCollection ConfigureOpenApi(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = Constants.ApiName, Version = "v1" });

            options.OperationFilter<RemoveVersionParameter>();
            options.DocumentFilter<SetVersionInPath>();
        });

        return services;
    }

    public static IApplicationBuilder ConfigureUseSwagger(this IApplicationBuilder app, string apiName,
        string routePrefix = "docs")
    {
        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", apiName);
            options.RoutePrefix = string.Empty;
            options.DisplayOperationId();
            options.DisplayRequestDuration();
        });

        return app;
    }
}