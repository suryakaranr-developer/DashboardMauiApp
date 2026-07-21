namespace DashboardMauiApp.Configuration;

/// <summary>
/// Centralised, flavour-aware app configuration.
/// The active flavour is selected at BUILD time via the MSBuild
/// "Configuration" property (Dev / UAT / Stage / Prod), which defines
/// one of the DEV / UAT / STAGE / PROD compiler constants (see the .csproj).
/// </summary>
public static class AppConfig
{
#if DEV
    public const string EnvironmentName = "Development";
    public const string ApiBaseUrl = "https://dev-api.yourcompany.com/api/";
    public const string BannerColorHex = "#2E7D32";   // green
    public const bool EnableLogging = true;
#elif UAT
    public const string EnvironmentName = "UAT";
    public const string ApiBaseUrl = "https://uat-api.yourcompany.com/api/";
    public const string BannerColorHex = "#F9A825";   // amber
    public const bool EnableLogging = true;
#elif STAGE
    public const string EnvironmentName = "Staging";
    public const string ApiBaseUrl = "https://stage-api.yourcompany.com/api/";
    public const string BannerColorHex = "#1565C0";   // blue
    public const bool EnableLogging = true;
#elif PROD
    public const string EnvironmentName = "Production";
    public const string ApiBaseUrl = "https://api.yourcompany.com/api/";
    public const string BannerColorHex = "#C62828";
    public const bool EnableLogging = false;
#else
    public const string EnvironmentName = "Development";
    public const string ApiBaseUrl = "https://dev-api.yourcompany.com/api/";
    public const string BannerColorHex = "#2E7D32";
    public const bool EnableLogging = true;
#endif

    /// <summary>True only for the Prod/Release flavour.</summary>
    public static bool IsProduction => EnvironmentName == "Production";
}
