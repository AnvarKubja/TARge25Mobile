
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memo;

// Mängu loogika: kaartide segamine, paaride kontroll ja võit
public class Game
{
    private static readonly string[] Symbols =
    {
        "#FF0000", // Punane
        "#0000FF", // Sinine
        "#00AA00", // Roheline
        "#FFFF00", // Kollane
        "#FF8000", // Oranž
        "#800080", // Lilla
        "#FF69B4", // Roosa
        "#00FFFF"  // Tsüaan
    };

    private Card? _first;
    private bool _busy;
    private int _matchedPairs;

    public Player Player { get; }
    public List<Card> Cards { get; private set; } = new();
    public DateTime StartTime { get; private set; }

    public event Action<Card>? CardFlipped;
    public event Action<Card, Card>? PairMatched;
    public event Action<Card, Card>? PairMismatched;
    public event Action? GameFinished;

    public Game(Player player)
    {
        Player = player;
    }

    public void Start()
    {
        Player.Moves = 0;
        _matchedPairs = 0;
        _first = null;
        _busy = false;
        StartTime = DateTime.Now;

        // Iga värv esineb kaks korda
        Cards = Symbols
            .Concat(Symbols)
            .Select(color => new Card(color))
            .ToList();

        // Segame kaardid
        var random = new Random();
        Cards = Cards.OrderBy(c => random.Next()).ToList();

        // Lähtestame kõik kaardid
        foreach (var card in Cards)
        {
            card.IsFlipped = false;
            card.IsMatched = false;
            card.Button.Text = "?";
            card.Button.BackgroundColor = Colors.Gray;
        }
    }

    public TimeSpan Elapsed => DateTime.Now - StartTime;

    // Kasutaja vajutas kaarti
    public async Task SelectAsync(Card card)
    {
        if (_busy || card.IsFlipped || card.IsMatched)
            return;

        card.IsFlipped = true;

        // Kuvame ainult värvi, mitte värvikoodi
        card.Button.Text = "";
        card.Button.BackgroundColor = Color.FromArgb(card.Symbol);

        CardFlipped?.Invoke(card);

        if (_first == null)
        {
            _first = card;
            return;
        }

        // Teine kaart valitud
        Player.Moves++;

        Card second = _first;
        _first = null;

        if (second.Symbol == card.Symbol)
        {
            // Paar leitud
            second.IsMatched = true;
            card.IsMatched = true;

            _matchedPairs++;

            PairMatched?.Invoke(second, card);

            // Kõik paarid leitud
            if (_matchedPairs == Symbols.Length)
                GameFinished?.Invoke();
        }
        else
        {
            // Paar ei sobi
            _busy = true;

            await Task.Delay(800);

            second.IsFlipped = false;
            card.IsFlipped = false;

            // Peidame värvid ja taastame küsimärgid
            second.Button.Text = "?";
            card.Button.Text = "?";

            second.Button.BackgroundColor = Colors.Gray;
            card.Button.BackgroundColor = Colors.Gray;

            PairMismatched?.Invoke(second, card);

            _busy = false;
        }
    }
}
