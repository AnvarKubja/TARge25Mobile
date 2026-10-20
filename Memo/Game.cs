using Memo;

namespace Memo;

// Mängu loogika: kaartide segamine, paaride kontroll, võit
public class Game
{
    private static readonly string[] Symbols = { "🍎", "🍌", "🍇", "🍓", "🍒", "🍉", "🥝", "🍋" };

    private Card? _first;       // esimene avatud kaart
    private bool _busy;         // true, kui kaks kaarti on avatud ja ootame
    private int _matchedPairs;

    public Player Player { get; }
    public List<Card> Cards { get; private set; } = new();
    public DateTime StartTime { get; private set; }

    // Sündmused, mida leht kuulab
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

        // Iga sümbol kaks korda, siis segame
        Cards = Symbols.Concat(Symbols).Select(s => new Card(s)).ToList();
        var random = new Random();
        Cards = Cards.OrderBy(c => random.Next()).ToList();
    }

    public TimeSpan Elapsed => DateTime.Now - StartTime;

    // Kasutaja puudutas kaarti
    public async Task SelectAsync(Card card)
    {
        if (_busy || card.IsFlipped || card.IsMatched)
            return;

        card.IsFlipped = true;
        CardFlipped?.Invoke(card);

        if (_first == null)
        {
            _first = card;   // esimene kaart valitud
            return;
        }

        // teine kaart valitud
        Player.Moves++;
        Card second = _first;
        _first = null;

        if (second.Symbol == card.Symbol)
        {
            second.IsMatched = true;
            card.IsMatched = true;
            _matchedPairs++;
            PairMatched?.Invoke(second, card);

            if (_matchedPairs == Symbols.Length)
                GameFinished?.Invoke();
        }
        else
        {
            _busy = true;
            await Task.Delay(800);   // anname aega kaardid ära näha
            second.IsFlipped = false;
            card.IsFlipped = false;
            PairMismatched?.Invoke(second, card);
            _busy = false;
        }
    }
}
