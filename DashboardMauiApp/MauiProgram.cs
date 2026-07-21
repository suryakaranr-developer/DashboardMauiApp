using Microsoft.Extensions.Logging;
using DashboardMauiApp.Configuration;

namespace DashboardMauiApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG || DEV || UAT || STAGE
        builder.Logging.AddDebug();
#endif

        // Example: register an HttpClient pre-configured with the
        // flavour-specific base address from AppConfig.
        builder.Services.AddSingleton(sp => new HttpClient
        {
            BaseAddress = new Uri(AppConfig.ApiBaseUrl)
        });

        return builder.Build();
    }
}
