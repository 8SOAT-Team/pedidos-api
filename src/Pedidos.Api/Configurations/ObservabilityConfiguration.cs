using Serilog;

namespace Pedidos.Api.Configurations;

public static class ObservabilityConfiguration
{
    public static IServiceCollection ConfigureObservability(this IServiceCollection services)
    {
        services.AddLogging(logBuilder =>
        {
            logBuilder.AddSerilog(new LoggerConfiguration()
                .WriteTo
                .Async(config => config.Console())
                .CreateLogger());
        });

        services.AddHealthChecks();
        return services;
    }

    public static IApplicationBuilder ConfigureMapHealthChecks(this IApplicationBuilder app)
    {
        app.UseHealthChecks("/health");
        return app;
    }
}