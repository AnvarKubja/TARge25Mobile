using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace Memo;

public partial class MainPage : ContentPage
{
    private Game? _game;
    private Theme _theme = Theme.All[0];
    private IDispatcherTimer? _timer;

    public MainPage()
    {
        InitializeComponent();

        // Täida teemade valik ja rakenda esimene teema
        ThemePicker.ItemsSource = Theme.All.Select(t => t.Name).ToList();
        ThemePicker.SelectedIndex = 0;

        // Taimer uuendab aega iga sekund
        _timer = Dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromSeconds(1);
        _timer.Tick += (s, e) => UpdateInfo();

        StartNewGame();
    }

    private void OnThemeChanged(object? sender, EventArgs e)
    {
        if (ThemePicker.SelectedIndex < 0) return;
        _theme = Theme.All[ThemePicker.SelectedIndex];
        _theme.Apply(this);
    }

    private void OnNewGameClicked(object? sender, EventArgs e)
    {
        StartNewGame();
    }

    private void StartNewGame()
    {
        string name = string.IsNullOrWhiteSpace(NameEntry.Text) ? "Mängija" : NameEntry.Text;
        _game = new Game(new Player(name));
        _game.CardFlipped += OnCardFlipped;
        _game.PairMatched += OnPairMatched;
        _game.PairMismatched += OnPairMismatched;
        _game.GameFinished += OnGameFinished;
        _game.Start();

        BuildBoard();
        UpdateInfo();
        _timer?.Start();
    }

    // Loob 4x4 ruudustiku kaartidest
    private void BuildBoard()
    {
        BoardGrid.Children.Clear();
        BoardGrid.RowDefinitions.Clear();
        BoardGrid.ColumnDefinitions.Clear();

        for (int i = 0; i < 4; i++)
        {
            BoardGrid.RowDefinitions.Add(new RowDefinition(GridLength.Star));
            BoardGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
        }

        for (int i = 0; i < _game!.Cards.Count; i++)
        {
            Card card = _game.Cards[i];
            card.Button.Clicked += async (s, e) => await _game.SelectAsync(card);
            BoardGrid.Add(card.Button, i % 4, i / 4);
        }
    }

    private void UpdateInfo()
    {
        if (_game == null) return;
        int best = _game.Player.BestMoves;
        string bestText = best == 0 ? "-" : best.ToString();
        InfoLabel.Text = $"{_game.Player.Name} | Käigud: {_game.Player.Moves} | Aeg: {_game.Elapsed:mm\\:ss} | Rekord: {bestText}";
    }

    // ---- Sündmused ja animatsioonid ----

    private async void OnCardFlipped(Card card)
    {
        await FlipAnimation(card.Button, card.Symbol);
        UpdateInfo();
    }

    private async void OnPairMatched(Card a, Card b)
    {
        await Task.WhenAll(
            a.Button.FadeTo(0.3, 400),
            b.Button.FadeTo(0.3, 400));
    }

    private async void OnPairMismatched(Card a, Card b)
    {
        await Task.WhenAll(
            FlipAnimation(a.Button, "?"),
            FlipAnimation(b.Button, "?"));
    }

    // Kaardi pööramine: pöörame 90 kraadi, vahetame teksti, pöörame tagasi
    private async Task FlipAnimation(Button button, string newText)
    {
        await button.RotateYTo(90, 120);
        button.Text = newText;
        await button.RotateYTo(0, 120);
    }

    private async void OnGameFinished()
    {
        _timer?.Stop();
        UpdateInfo();

        bool isRecord = _game!.Player.SaveIfBest();
        string message = $"Käike: {_game.Player.Moves}\nAeg: {_game.Elapsed:mm\\:ss}";
        if (isRecord)
            message += "\nUus rekord!";

        await Task.Delay(500);
        await DisplayAlert("Võit!", message, "OK");
    }
}
