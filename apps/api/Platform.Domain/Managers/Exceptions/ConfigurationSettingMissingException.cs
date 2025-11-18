using System.Diagnostics.CodeAnalysis;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public sealed class ConfigurationSettingMissingException : TechnicalBaseException
{
    public override string Reason => "Configuration Setting Missing";

    public ConfigurationSettingMissingException()
    {
    }

    public ConfigurationSettingMissingException(string message) : base(message)
    {
    }

    public ConfigurationSettingMissingException(string message, Exception inner) : base(message, inner)
    {
    }
}