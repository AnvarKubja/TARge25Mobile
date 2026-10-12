namespace MauiCarouselApp;

public class LanguageItem
{
    public string Name { get; set; }
    public string DescriptionEn { get; set; }
    public string DescriptionEt { get; set; }
    public string ImageUrl { get; set; }
    public int YearCreated { get; set; }
    public string HelloWorldCode { get; set; }

    // Dünaamiline omadus, mis tagastab kirjelduse vastavalt valitud kultuurile
    public string LocalizedDescription =>
        System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "et"
            ? DescriptionEt
            : DescriptionEn;
}
