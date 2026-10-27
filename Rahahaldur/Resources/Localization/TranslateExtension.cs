using Microsoft.Maui.Controls.Xaml;
using RahaHaldur.Services;

namespace RahaHaldur.Resources.Localization;

// Lubab XAML-is kirjutada: Text="{loc:Translate Save}"
// Tulemus on sidumine (binding), seega tekst uueneb keele vahetamisel ise.
[ContentProperty(nameof(Key))]
public class TranslateExtension : IMarkupExtension<BindingBase>
{
    public string Key { get; set; } = "";

    public BindingBase ProvideValue(IServiceProvider serviceProvider)
    {
        return new Binding($"[{Key}]", BindingMode.OneWay, source: LocalizationService.Instance);
    }

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
    {
        return ProvideValue(serviceProvider);
    }
}
