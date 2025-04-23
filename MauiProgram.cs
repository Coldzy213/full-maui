using Microsoft.Extensions.Logging;
using WeatherApp.ViewModels; // Add this
using WeatherApp.Services;   // Add this
// using MauiWeatherApp.Views; // MainPage is likely in the root namespace

namespace WeatherApp;

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

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Register HttpClient as Singleton for reuse
        // Consider configuring HttpClient base address or other settings here if needed
        builder.Services.AddSingleton<HttpClient>();

        // Register Services (Singleton is usually fine for stateless services)
        builder.Services.AddSingleton<WeatherService>();

        // Register ViewModels (Transient or Singleton depending on needs)
        // Singleton means the same instance is reused for MainPage
        // Transient means a new instance every time it's requested
        builder.Services.AddSingleton<WeatherViewModel>();

        // Register Pages/Views (Transient usually preferred for pages)
        builder.Services.AddSingleton<MainPage>(); // Use Singleton if ViewModel is Singleton and injected


        return builder.Build();
    }
}