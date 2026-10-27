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

    public List<string> Languages { get; } = new List<string> { "Eesti", "English" };

    // 0 = eesti, 1 = inglise
    public int LanguageIndex
    {
        get => _languageIndex;
        set
        {
            if (value < 0 || value == _languageIndex)
                return;

            _languageIndex = value;
            OnPropertyChanged(nameof(LanguageIndex));

            string code = "et";
            if (value == 1)
                code = "en";

            _settings.Language = code;   // salvesta
            _loc.SetLanguage(code);      // muuda keel kohe
        }
    }

    public bool IsDark
    {
        get => _isDark;
        set
        {
            if (value == _isDark)
                return;

            _isDark = value;
            OnPropertyChanged(nameof(IsDark));

            if (value)
                _theme.Apply("Dark");
            else
                _theme.Apply("Light");
        }
    }

    public bool SoundOn
    {
        get => _soundOn;
        set
        {
            if (value == _soundOn)
                return;

            _soundOn = value;
            OnPropertyChanged(nameof(SoundOn));

            _settings.SoundOn = value;
            if (value)
                _audio.StartMusicAsync();
            else
                _audio.StopMusic();
        }
    }

    public SettingsViewModel(SettingsService settings, ThemeService theme, LocalizationService loc, AudioService audio)
    {
        _settings = settings;
        _theme = theme;
        _loc = loc;
        _audio = audio;

        // Taasta salvestatud väärtused
        if (settings.Language == "en")
            _languageIndex = 1;
        else
            _languageIndex = 0;

        _isDark = settings.Theme == "Dark";
        _soundOn = settings.SoundOn;
    }
}
