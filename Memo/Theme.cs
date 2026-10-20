namespace Memo;

// Teema: hoiab värve ja fonti ning rakendab need lehele
public class Theme
{
    public string Name { get; }
    public Color BackgroundColor { get; }
    public Color TextColor { get; }
    public Color CardColor { get; }
    public string FontName { get; }

    public Theme(string name, Color background, Color text, Color card, string fontName)
    {
        Name = name;
        BackgroundColor = background;
        TextColor = text;
        CardColor = card;
        FontName = fontName;
    }

    // Kirjutab teema väärtused lehe ressurssidesse (DynamicResource uuendab välimuse kohe)
    public void Apply(ContentPage page)
    {
        page.BackgroundColor = BackgroundColor;
        page.Resources["TextColor"] = TextColor;
        page.Resources["CardColor"] = CardColor;
        page.Resources["FontName"] = FontName;
    }

    // Kõik saadaval olevad teemad
    public static List<Theme> All { get; } = new()
    {
        new Theme("Hele", Color.FromArgb("#FFFFFF"), Color.FromArgb("#222222"), Color.FromArgb("#BBDEFB"), "OpenSansRegular"),
        new Theme("Tume", Color.FromArgb("#1E1E1E"), Color.FromArgb("#FFFFFF"), Color.FromArgb("#455A64"), "OpenSansSemibold"),
        new Theme("Värviline", Color.FromArgb("#FF80AB"), Color.FromArgb("#FFF176"), Color.FromArgb("#7E57C2"), "")
    };
}
