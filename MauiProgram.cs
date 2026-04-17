using Microsoft.Extensions.Logging;
using The_Hunt_Khai_Tan_Sum.Services;
using The_Hunt_Khai_Tan_Sum.ViewModels;
using The_Hunt_Khai_Tan_Sum.Views;
using CommunityToolkit.Maui;

namespace The_Hunt_Khai_Tan_Sum;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Services
        builder.Services.AddSingleton<ISettingsService, SettingsService>();

        // ViewModels
        builder.Services.AddTransient<HomeViewModel>();
        builder.Services.AddTransient<ManifestoViewModel>();
        builder.Services.AddTransient<SettingsViewModel>();
        builder.Services.AddTransient<HistoryViewModel>();
        builder.Services.AddTransient<GraveyardViewModel>();

        // Pages
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<ManifestoPage>();
        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<HistoryPage>();
        builder.Services.AddTransient<GraveyardPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}