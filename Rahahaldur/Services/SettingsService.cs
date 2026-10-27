namespace RahaHaldur.Services;

// Hoiab kasutaja eelistusi. Preferences salvestab need telefoni,
// seega on need olemas ka pärast rakenduse uuesti käivitamist.
public class SettingsService
{
    public string Language
    {
        get => Preferences.Get("language", "et");
        set => Preferences.Set("language", value);
    }

    public string Theme
    {
        get => Preferences.Get("theme", "Light");
        set => Preferences.Set("theme", value);
    }

    public bool SoundOn
    {
        get => Preferences.Get("sound_on", true);
        set => Preferences.Set("sound_on", value);
    }
}
