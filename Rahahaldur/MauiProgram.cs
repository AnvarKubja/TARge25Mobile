using Plugin.Maui.Audio;
using RahaHaldur.Services;
using RahaHaldur.ViewModels;
using RahaHaldur.Views;

namespace RahaHaldur;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();
        builder.AddAudio(); // Plugin.Maui.Audio

        // Teenused (üks koopia kogu rakenduse jaoks)
        builder.Services.AddSingleton<SettingsService>();
        builder.Services.AddSingleton(LocalizationService.Instance);
        builder.Services.AddSingleton<ThemeService>();
        builder.Services.AddSingleton<AudioService>();
        builder.Services.AddSingleton<DatabaseService>();
        builder.Services.AddSingleton<NavigationService>();

        // ViewModelid
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<SortViewModel>();
        builder.Services.AddSingleton<SettingsViewModel>();
        builder.Services.AddTransient<EditViewModel>(); // uus iga kord, kui leht avatakse

        // Vaated
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<SortPage>();
        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<EditPage>();

        return builder.Build();
    }
}
