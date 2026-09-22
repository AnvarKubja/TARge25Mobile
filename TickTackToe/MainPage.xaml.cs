using Microsoft.Maui.Storage;

namespace TickTackToe
{
    public partial class MainPage : ContentPage
    {
        private GameEngine _game = new GameEngine();
        private Button[,] _buttons = new Button[3, 3];

        // Statistika muutujad
        private int _xWins = Preferences.Get("X_Wins", 0);
        private int _oWins = Preferences.Get("O_Wins", 0);
        private int _draws = Preferences.Get("Draws", 0);

        public MainPage()
        {
            InitializeComponent();
            InitializeGrid();
            UpdateStatsDisplay();
        }

        private void InitializeGrid()
        {
            // Ruudustiku definitsioonid
            for (int i = 0; i < 3; i++)
            {
                GameGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            }

            // Nuppude loomine ruudustikku
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    var button = new Button
                    {
                        Text = "",
                        FontSize = 85,
                        FontAttributes = FontAttributes.Bold,
                        BackgroundColor = Colors.White,
                        TextColor = Colors.Black,
                        CornerRadius = 0
                    };

                    // Salvestame rea ja veeru nupu külge, et hiljem tuvastada asukoht
                    int row = r;
                    int col = c;
                    button.Clicked += (s, e) => OnCellClicked(row, col);

                    _buttons[r, c] = button;
                    GameGrid.Add(button, col, r);
                }
            }
        }

        private async void OnCellClicked(int row, int col)
        {
            if (!_game.MakeMove(row, col)) return;

            // Uuenda vajutatud nuppu visuaalselt
            _buttons[row, col].Text = _game.CurrentPlayer.ToString();
            _buttons[row, col].TextColor = _game.CurrentPlayer == Player.X ? Colors.Blue : Colors.Red;

            // Kontrolli võitu või viiki
            if (_game.CheckWin(row, col))
            {
                UpdateScore(_game.CurrentPlayer);
                bool playAgain = await DisplayAlert("Mäng läbi!", $"{_game.CurrentPlayer} võitis! Kas soovid veel mängida?", "Jah", "Ei");
                if (playAgain) ResetBoard();
                return;
            }
            else if (_game.CheckDraw())
            {
                UpdateScore(Player.None);
                bool playAgain = await DisplayAlert("Viik!", "Mäng jäi viiki. Kas soovid veel mängida?", "Jah", "Ei");
                if (playAgain) ResetBoard();
                return;
            }

            // Vaheta mängijat ja uuenda staatust
            _game.SwitchPlayer();
            StatusLabel.Text = $"Mängija {_game.CurrentPlayer} käik";
        }

        private void OnNewGameClicked(object sender, EventArgs e)
        {
            ResetBoard();
        }

        private async void OnWhoStartsClicked(object sender, EventArgs e)
        {
            if (_game.MoveCount > 0)
            {
                await DisplayAlert("Hoiatus", "Mäng on juba alanud. Alustajat saab valida ainult uue mängu eel.", "OK");
                return;
            }

            string action = await DisplayActionSheet("Vali alustaja:", "Tühista", null, "Mängija X", "Mängija O", "Juhuslik");

            if (action == "Mängija X") _game.SetStartingPlayer(Player.X);
            else if (action == "Mängija O") _game.SetStartingPlayer(Player.O);
            else if (action == "Juhuslik") _game.ChooseRandomStartingPlayer();

            StatusLabel.Text = $"Mängija {_game.CurrentPlayer} käik";
        }

        private void ResetBoard()
        {
            _game.ResetGame();
            StatusLabel.Text = $"Mängija {_game.CurrentPlayer} käik";

            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    _buttons[r, c].Text = "";
                    _buttons[r, c].BackgroundColor = Colors.White;
                }
            }
        }

        private void UpdateScore(Player winner)
        {
            if (winner == Player.X) _xWins++;
            else if (winner == Player.O) _oWins++;
            else _draws++;

            Preferences.Set("X_Wins", _xWins);
            Preferences.Set("O_Wins", _oWins);
            Preferences.Set("Draws", _draws);

            UpdateStatsDisplay();
        }

        private void UpdateStatsDisplay()
        {
            StatsLabel.Text = $"Võidud - X: {_xWins} | O: {_oWins} | Viigid: {_draws}";
        }

        private async void OnRulesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RulesPage());
        }
    }
}
