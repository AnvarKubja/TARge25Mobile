namespace RahaHaldur.Services;

// Vahetab heleda ja tumeda teema vahel ning jätab valiku meelde.
public class ThemeService
{
    private readonly SettingsService _settings;

    public ThemeService(SettingsService settings)
    {
        _settings = settings;
    }

    public bool IsDark => _settings.Theme == "Dark";

    // Rakenduse käivitamisel: võta salvestatud teema
    public void ApplySaved()
    {
        Apply(_settings.Theme);
    }

    public void Apply(string theme)
    {
        _settings.Theme = theme;
        Application.Current!.UserAppTheme = theme == "Dark" ? AppTheme.Dark : AppTheme.Light;
    }
}
