namespace Memo;

// Üks mängukaart
public class Card
{
    public string Symbol { get; }
    public bool IsFlipped { get; set; }
    public bool IsMatched { get; set; }
    public Button Button { get; }   // nupp, mis kaardi ekraanil kuvab

    public Card(string symbol)
    {
        Symbol = symbol;
        IsFlipped = false;
        IsMatched = false;
        Button = new Button { Text = "?", FontSize = 32 };
        Button.SetDynamicResource(Button.BackgroundColorProperty, "CardColor");
        Button.SetDynamicResource(Button.TextColorProperty, "TextColor");
        Button.SetDynamicResource(Button.FontFamilyProperty, "FontName");
    }
}
