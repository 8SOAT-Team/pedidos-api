namespace Pedidos.Api.Configurations;

public static class OpenApiConfiguration
{
    public static IServiceCollection ConfigureOpenApi(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }
    
    public static IApplicationBuilder ConfigureUseSwagger(this IApplicationBuilder app, string apiName, string routePrefix = "docs")
    {
        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", apiName);
            options.RoutePrefix = string.Empty;
        });
        
        return app;
    }
}