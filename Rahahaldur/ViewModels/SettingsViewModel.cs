using RahaHaldur.Services;

namespace RahaHaldur.ViewModels;

// Seadete leht: keel, teema, heli. Iga muudatus rakendub kohe ja salvestatakse.
public class SettingsViewModel : BaseViewModel
{
    private readonly SettingsService _settings;
    private readonly ThemeService _theme;
    private readonly LocalizationService _loc;
    private readonly AudioService _audio;

    private int _languageIndex;
    private bool _isDark;
    private bool _soundOn;

    public List<string> Languages { get; } = new() { "Eesti", "English" };

    public int LanguageIndex
    {
        get => _languageIndex;
        set
        {
            if (value < 0) // Picker võib hetkeks anda -1
                return;

            if (SetProperty(ref _languageIndex, value))
            {
                var code = value == 1 ? "en" : "et";
                _settings.Language = code;
                _loc.SetLanguage(code); // keel muutub kohe, restarti pole vaja
            }
        }
    }

    public bool IsDark
    {
        get => _isDark;
        set
        {
            if (SetProperty(ref _isDark, value))
                _theme.Apply(value ? "Dark" : "Light");
        }
    }

    public bool SoundOn
    {
        get => _soundOn;
        set
        {
            if (SetProperty(ref _soundOn, value))
            {
                _settings.SoundOn = value;
                if (value)
                    _ = _audio.StartMusicAsync();
                else
                    _audio.StopMusic();
            }
        }
    }

    public SettingsViewModel(SettingsService settings, ThemeService theme, LocalizationService loc, AudioService audio)
    {
        _settings = settings;
        _theme = theme;
        _loc = loc;
        _audio = audio;

        // Taasta salvestatud väärtused
        _languageIndex = settings.Language == "en" ? 1 : 0;
        _isDark = settings.Theme == "Dark";
        _soundOn = settings.SoundOn;
    }
}
