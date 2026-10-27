namespace RahaHaldur.Services;

// Vahetab heleda ja tumeda teema vahel ning jätab valiku meelde.
public class ThemeService
{
    private readonly SettingsService _settings;

    public ThemeService(SettingsService settings)
    {
        _settings = settings;
    }

    // Rakenduse käivitamisel: võta salvestatud teema
    public void ApplySaved()
    {
        Apply(_settings.Theme);
    }

    // theme on "Dark" või "Light"
    public void Apply(string theme)
    {
        _settings.Theme = theme;

        if (theme == "Dark")
            Application.Current.UserAppTheme = AppTheme.Dark;
        else
            Application.Current.UserAppTheme = AppTheme.Light;
    }
}
