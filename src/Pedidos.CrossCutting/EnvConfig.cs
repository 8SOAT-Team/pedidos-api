using System.Diagnostics.CodeAnalysis;

namespace Pedidos.CrossCutting;

[ExcludeFromCodeCoverage]
public static class EnvConfig
{
    public static string EnvironmentName => EnvConfigValueGetter.MustGetString("ASPNETCORE_ENVIRONMENT");
    public static bool IsTestEnv => EnvironmentName.Equals("test", StringComparison.InvariantCultureIgnoreCase);
    public static string DatabaseConnection => EnvConfigValueGetter.MustGetString("DB_CONNECTION_STRING");
    public static string DistributedCacheUrl => EnvConfigValueGetter.MustGetString("DISTRIBUTED_CACHE_URL");
    public static bool RunMigrationsOnStart => EnvConfigValueGetter.GetBool("RUN_MIGRATIONS_ON_START");

    private static class EnvConfigValueGetter
    {
        public static string MustGetString(string key)
        {
            return Environment.GetEnvironmentVariable(key) ?? throw new ArgumentNullException(nameof(key));
        }

        public static string GetString(string key)
        {
            return Environment.GetEnvironmentVariable(key) ?? string.Empty;
        }

        public static Uri MustGetUri(string key)
        {
            var uri = MustGetString(key);
            return new Uri(uri, UriKind.Absolute);
        }

        public static bool GetBool(string key)
        {
            return bool.TryParse(GetString(key), out var value) && value;
        }
    }
}