using System.Globalization;
using MauiCarouselApp.Resources.Languages;

namespace MauiCarouselApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainPageViewModel();
    }

    // Interaktiivsus: Kuvab DisplayAlert detailse infoga
    private async void OnCardTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is LanguageItem selectedLanguage)
        {
            string message = $"{AppResources.CreatedIn}: {selectedLanguage.YearCreated}\n\nCode Example:\n{selectedLanguage.HelloWorldCode}";

            await DisplayAlert(
                selectedLanguage.Name,
                message,
                AppResources.CloseButton);
        }
    }

    // Keele vahetus Eesti keelele
    private void ChangeLanguageToEt(object sender, EventArgs e)
    {
        UpdateLanguage("et");
    }

    // Keele vahetus Inglise keelele
    private void ChangeLanguageToEn(object sender, EventArgs e)
    {
        UpdateLanguage("en");
    }

    private void UpdateLanguage(string cultureCode)
    {
        var culture = new CultureInfo(cultureCode);
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;

        // Värskendab BindingContexti, et muutused rakenduksid koheselt ekraanile
        var currentViewModel = BindingContext;
        BindingContext = null;
        BindingContext = currentViewModel;
    }
}
