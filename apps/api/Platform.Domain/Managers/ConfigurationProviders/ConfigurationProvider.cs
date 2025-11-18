using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;

namespace Platform.Domain;

internal sealed class ConfigurationProvider : ConfigurationProviderBase
{
    private readonly IConfigurationRoot _configurationRoot;

    public ConfigurationProvider()
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        LoadEnvironmentSpecificAppSettings(configurationBuilder);
        
        // Add User Secrets in Development environment (Aspire-compatible)
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (environment == "Development")
        {
            // User Secrets are loaded by assembly - this enables Aspire compatibility
            configurationBuilder.AddUserSecrets<ConfigurationProvider>(optional: true);
        }
        
        // Add environment variables (production and Aspire-compatible)
        configurationBuilder.AddEnvironmentVariables();

        _configurationRoot = configurationBuilder.Build();
    }

    protected override string RetrieveConfigurationSettingValue(string key)
    {
        return _configurationRoot[key]!;
    }

    internal ConfigurationProvider(IConfigurationRoot configurationRoot) => _configurationRoot = configurationRoot;


    private static void LoadEnvironmentSpecificAppSettings(IConfigurationBuilder configurationBuilder)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (!string.IsNullOrWhiteSpace(environment))
        {
            var envFile = $"appsettings.{environment}.json";
            if (File.Exists(envFile))
            {
                configurationBuilder.AddJsonFile(envFile);
            }
        }
    }
}
