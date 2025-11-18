using Platform.Domain;

namespace Platform.AcceptanceTests.Domain;

internal sealed class ServiceLocatorForAcceptanceTesting : ServiceLocatorBase
{
    protected override ConfigurationProviderBase CreateConfigurationProviderCore()
    {
        return new ConfigurationProvider();
    }
}
