
namespace Memo;

// Üks mängukaart
public class Card
{
    public string Symbol { get; }
    public bool IsFlipped { get; set; }
    public bool IsMatched { get; set; }
    public Button Button { get; }

    public Card(string color)
    {
        Symbol = color;
        IsFlipped = false;
        IsMatched = false;

        Button = new Button
        {
            Text = "?",
            FontSize = 32,
            BackgroundColor = Colors.Gray,
            TextColor = Colors.White
        };
    }
}
