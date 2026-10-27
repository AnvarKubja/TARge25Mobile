using System.ComponentModel;
using System.Globalization;
using System.Resources;

namespace RahaHaldur.Services;

// Võtab tekstid .resx failidest. Kui keel muutub, saavad kõik
// XAML-i sidumised (bindings) teate ja uuendavad end ise.
public class LocalizationService : INotifyPropertyChanged
{
    // Terves rakenduses on ainult see üks koopia
    public static LocalizationService Instance { get; } = new LocalizationService();

    private readonly ResourceManager _resources =
        new ResourceManager("RahaHaldur.Resources.Localization.AppResources", typeof(LocalizationService).Assembly);

    private CultureInfo _culture = new CultureInfo("et");

    public event PropertyChangedEventHandler PropertyChanged;
    public event Action LanguageChanged;

    // Kasutus: loc["Save"] annab "Salvesta" või "Save"
    public string this[string key]
    {
        get
        {
            string text = _resources.GetString(key, _culture);
            if (text == null)
                return key;
            return text;
        }
    }

    public void SetLanguage(string code)
    {
        _culture = new CultureInfo(code);

        // Anna kõigile tekstidele teada, et nad uuendaksid end
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        LanguageChanged?.Invoke();
    }
}
