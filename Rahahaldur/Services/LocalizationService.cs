using System.ComponentModel;
using System.Globalization;
using System.Resources;

namespace RahaHaldur.Services;

// Võtab tekstid .resx failidest. Kui keel muutub, saavad kõik
// XAML-i sidumised (bindings) teate ja uuendavad end ise.
public class LocalizationService : INotifyPropertyChanged
{
    public static LocalizationService Instance { get; } = new();

    private readonly ResourceManager _resources =
        new("RahaHaldur.Resources.Localization.AppResources", typeof(LocalizationService).Assembly);

    private CultureInfo _culture = new("et");

    public event PropertyChangedEventHandler? PropertyChanged;
    public event Action? LanguageChanged;

    // Kasutus: loc["Save"]
    public string this[string key] => _resources.GetString(key, _culture) ?? key;

    public void SetLanguage(string code)
    {
        _culture = new CultureInfo(code);
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null)); // uuenda kõik tekstid
        LanguageChanged?.Invoke();
    }
}
