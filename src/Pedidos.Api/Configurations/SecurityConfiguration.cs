namespace Pedidos.Api.Configurations;

public static class SecurityConfiguration
{
    public static IServiceCollection ConfigureSecurity(this IServiceCollection services)
    {
        services.AddCors();
        return services;
    }

    public static IApplicationBuilder ConfigureUseSecurity(this IApplicationBuilder app)
    {
        app.UseCors(builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });

        return app;
    }
}