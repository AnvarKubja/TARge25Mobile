using RahaHaldur.Services;

namespace RahaHaldur;

public partial class App : Application
{
    private readonly AudioService _audio;

    public App(SettingsService settings, LocalizationService loc, ThemeService theme, AudioService audio)
    {
        InitializeComponent();
        _audio = audio;

        // Taasta salvestatud seaded (keel ja teema)
        loc.SetLanguage(settings.Language);
        theme.ApplySaved();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(new AppShell());

        // Taustamuusika algab, kui aken on loodud (kui heli on sisse lülitatud)
        window.Created += (s, e) => _ = _audio.StartMusicAsync();

        return window;
    }
}
